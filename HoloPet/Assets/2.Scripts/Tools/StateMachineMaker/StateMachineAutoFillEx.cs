﻿using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class StateMachineAutoFillEx : MonoBehaviour
{
    [SerializeField] private StateMachineBase targetSM;
    [SerializeField] private GameObject statesFolder;
    [SerializeField] private GameObject managerFolder;
    [SerializeField] private bool RGB = false;

    private int mgCount;
    private int mgAssigned;
    private int rainbowIndex = 0;

    private static readonly string[] RainbowColors = new[]
    {
        "#FF4C4C", // Red
        "#FF884C", // Orange
        "#FFE14C", // Yellow
        "#4CFF4C", // Green
        "#4CFFFF", // Cyan
        "#4C4CFF", // Blue
        "#B44CFF", // Purple
        "#FF4CA3"  // Pink
    };

    [ContextMenu("Auto Fill Everything")]
    private void AutoFill()
    {
        ClearConsole();
        mgCount = 0;
        mgAssigned = 0;
        rainbowIndex = 0;

        StateMachineBase targetStateMachineBase = GetComponent<StateMachineBase>();
        if (targetSM != targetStateMachineBase)
        {
            targetSM = targetStateMachineBase;
        }

        Type targetType = targetSM.GetType();
        Debug.Log($"<color=#D080FF>Setting {targetType.Name} For {targetSM.transform.name}</color>");

        CreateFolderIfMissing();

        Debug.Log("<color=yellow>Setting Managers</color>");

        #region Managers
        TrySetManagerField<BoundaryManager>("BoundaryMg", targetType, "boundaryMg");
        TrySetManagerField<FaceDirectionManager>("FaceDirectionMg", targetType, "faceDirectionMg");
        TrySetManagerField<MovementManager>("MovementMg", targetType, "movementMg");
        TrySetManagerField<RaycastManager>("RaycastMg", targetType, "raycastMg");
        TrySetManagerField<BaseDataManager>("BaseDataMg", targetType, "baseDataMg");
        TrySetManagerField<ClickableManager>("ClickableMg", targetType, "clickableMg");
        TrySetManagerField<RandomMoveManager>("RandomMoveMg", targetType, "randomMoveMg");
        TrySetManagerField<MountableManager>("MountableMg", targetType, "mountableMg");
        TrySetManagerField<MountingAbilityManager>("MountingAbilityMg", targetType, "mountingAbilityMg");
        TrySetManagerField<InteractableManager>("InteractableMg", targetType, "interactableMg");
        TrySetManagerField<InteractAbilityManager>("InteractAbilityMg", targetType, "interactAbilityMg");

        FieldInfo layerMgField = targetType.GetField("layerMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (layerMgField != null)
        {
            OnlyManagerAssign<ILayerManager>(layerMgField);
        }
        #endregion

        Debug.Log($"<color=yellow> ({mgAssigned}/{mgCount})</color>");
    }

    [ContextMenu("States Check")]
    private void StatesCheck()
    {
        ClearConsole();

        #region StatesCheck
        Debug.Log("<color=yellow>States Check</color>");
        Type targetType = targetSM.GetType();
        FieldInfo[] fields = targetType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

        int stateCount = 0;
        int stateAssigned = 0;

        foreach (var field in fields)
        {
            if (field.FieldType == typeof(StateBase))
            {
                stateCount++;
                object value = field.GetValue(targetSM);
                if (value == null)
                {
                    Log($"{field.Name} is not assigned", warn: true);
                }
                else
                {
                    stateAssigned++;
                }
            }
        }
        Debug.Log($"<color=yellow> ({stateAssigned}/{stateCount})</color>");
        #endregion
    }
    [ContextMenu("Basic States Fill")]
    private void BasicStatesFill()
    {
        StateMachineBase targetStateMachineBase = GetComponent<StateMachineBase>();
        if (targetSM != targetStateMachineBase)
        {
            targetSM = targetStateMachineBase;
        }

        Type targetType = targetSM.GetType();
        ClearConsole();

        #region Managers
        TryFindStateField("StateIdle", targetType, "stateIdle");
        #endregion
    }

    private void ClearConsole()
    {
        var logEntries = Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        var clearMethod = logEntries?.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
        clearMethod?.Invoke(null, null);
    }

    private void CreateFolderIfMissing()
    {
        Transform statesTf = transform.Find("States");
        if (statesTf == null)
        {
            GameObject child = new GameObject("States");
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            statesFolder = child;
            Log("Created child object: States");
        }
        else
        {
            statesFolder = statesTf.gameObject;
        }

        Transform mgTf = transform.Find("Managers");
        if (mgTf == null)
        {
            GameObject child = new GameObject("Managers");
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            managerFolder = child;
            Log("Created child object: Managers");
        }
        else
        {
            managerFolder = mgTf.gameObject;
        }
    }

    private bool TrySetManagerField<T>(string prefabName, Type type, string fieldName) where T : Component
    {
        FieldInfo field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (field != null)
        {
            GenerateManagerAndAssign<T>(prefabName, field);
            return true;
        }
        return false;
    }

    private bool TryFindStateField(string prefabName, Type type, string fieldName) 
    {
        FieldInfo field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (field != null)
        {
            GenerateStateAndAssign(prefabName, field);
            return true;
        }
        return false;
    }


    private void GenerateStateAndAssign(string prefabName, FieldInfo field)
    {
        GameObject prefab = FindPrefabByName(prefabName);
        if (prefab != null)
        {
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            instance.transform.SetParent(statesFolder.transform, false);
            Log($"{prefabName} prefab instantiated.");
            StateBase found = instance.GetComponent<StateBase>();
            object currentValue = field.GetValue(targetSM);
            if (found!= null && (object)found != currentValue)
            {
                field.SetValue(targetSM, found);
                Log($"{found} auto-filled with {found.name}");
            }
            
        }
        else
        {
            Log($"Missing {prefabName} and no prefab found.", warn: true);
        }
    }

    //checks muitiple component
    private void GenerateManagerAndAssign<T>(string prefabName, FieldInfo field) where T : Component
    {
        mgCount++;
        T[] foundComponents = targetSM.GetComponentsInChildren<T>(true);

        if (foundComponents.Length == 0)
        {
            GameObject prefab = FindPrefabByName(prefabName);
            if (prefab != null)
            {
                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                instance.transform.SetParent(managerFolder.transform, false);
                Log($"[{typeof(T).Name}] prefab instantiated.");
            }
            else
            {
                Log($"Missing {typeof(T).Name} and no prefab found.", warn: true);
            }
        }

        foundComponents = targetSM.GetComponentsInChildren<T>(true);

        if (foundComponents.Length > 1)
        {
            Log($"Multiple {typeof(T).Name} found.", warn: true);
        }

        if (foundComponents.Length == 1)
        {
            T found = foundComponents[0];
            object currentValue = field.GetValue(targetSM);
            if (currentValue != (object)found)
            {
                field.SetValue(targetSM, found);
                Log($"{typeof(T).Name} auto-filled with {found.name}");
            }
            mgAssigned++;
        }
    }

    private void OnlyManagerAssign<T>(FieldInfo fieldToAssign)
    {
        mgCount++;
        T[] foundComponents = targetSM.GetComponentsInChildren<T>(true);

        if (foundComponents.Length == 0)
        {
            Log($"Missing {typeof(T).Name}.", warn: true);
        }
        else if (foundComponents.Length > 1)
        {
            Log($"Multiple {typeof(T).Name} found.", warn: true);
        }
        else
        {
            T found = foundComponents[0];
            object currentValue = fieldToAssign.GetValue(targetSM);
            if (currentValue != (object)found)
            {
                fieldToAssign.SetValue(targetSM, found);
                Log($"{typeof(T).Name} auto-filled with {((Component)(object)found).name}");
            }
            mgAssigned++;
        }
    }

    private GameObject FindPrefabByName(string prefabName)
    {
        string[] guids = AssetDatabase.FindAssets($"{prefabName} t:Prefab");

        if (guids.Length == 0)
        {
            Log($"No prefab found with name containing '{prefabName}'.", warn: true);
            return null;
        }
        else if (guids.Length > 1)
        {
            Log($"Multiple prefabs found whose names contain '{prefabName}':", warn: true);
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Log($" → {path}", warn: true);
            }
            return null;
        }
        else
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null)
            {
                Log($"Asset at path {path} could not be loaded as a prefab.", warn: true);
                return null;
            }
            if (prefab.name != prefabName)
            {
                Log($"Similar prefab found, but name does not exactly match '{prefabName}'. Found: '{prefab.name}' at path: {path}", warn: true);
                return null;
            }
            return prefab;
        }
    }

    private void Log(string message, bool warn = false)
    {
        if (RGB)
        {
            string color = RainbowColors[rainbowIndex % RainbowColors.Length];
            rainbowIndex++;
            string formatted = $"<color={color}>{message}</color>";
            if (warn)
                Debug.LogWarning(formatted);
            else
                Debug.Log(formatted);
        }
        else
        {
            if (warn)               
                Debug.LogWarning($"<color=#FF8080>{message}</color>");
            else
                Debug.Log(message);
        }
    }
}
