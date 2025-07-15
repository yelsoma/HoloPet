using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using System.Drawing;


public class StateMachineAutoFill : MonoBehaviour
{
    [SerializeField] private StateMachineBase targetSM;

    private string BoundaryPrefabS = "BoundaryMg";
    private string FaceDirectionPrefabS = "FaceDirection";
    [SerializeField] private GameObject statesFolder;
    [SerializeField] private GameObject managerFolder;
    [ContextMenu("Generate Object Basic")]
    private void GenerateStatesAndManagers()
    {
        statesFolder= CreateChildIfMissing("States");
        managerFolder = CreateChildIfMissing("Managers");
    }   
    [ContextMenu("Auto Fill")]
    private void AutoFill()
    {
        int mgCount = 0;
        int mgAssigned = 0;

        ClearConsole();

        if (targetSM == null)
        {
            Debug.LogError("Target StateMachine is not assigned.");
            return;
        }

        Type targetType = targetSM.GetType();
        Debug.Log($"<color=#FF8080>Making {targetType.Name} ¡X {targetSM.transform.name}</color>");
        Debug.Log("¡X ¡X ¡X");
        Debug.Log("<color=yellow>Managers</color>");
        Debug.Log("¡X ¡X ¡X");

        #region BoundaryManager
        FieldInfo boundaryMgField = targetType.GetField("boundaryMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (boundaryMgField != null)
        {
            mgCount++;
            BoundaryManager[] boundaryManagers = targetSM.GetComponentsInChildren<BoundaryManager>(true);

            if (boundaryManagers.Length == 0)
            {
                GameObject prefab = FindPrefabByName(BoundaryPrefabS);
                if (prefab != null)
                {
                    GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                    instance.transform.SetParent(managerFolder.transform, false);
                    Debug.Log($"BoundaryManager prefab instantiated and assigned");
                }
                else
                {
                    Debug.LogWarning("Missing Basic BoundaryMg ¡X no prefab found to instantiate.");
                }
            }

            // Re-scan after possible prefab instantiation
            boundaryManagers = targetSM.GetComponentsInChildren<BoundaryManager>(true);

            if (boundaryManagers.Length > 1)
            {
                Debug.LogWarning("Multiple Basic BoundaryMg");
            }

            if (boundaryManagers.Length == 1)
            {
                BoundaryManager boundaryInChild = boundaryManagers[0];
                object currentValue = boundaryMgField.GetValue(targetSM);
                if (currentValue != (object)boundaryInChild)
                {
                    boundaryMgField.SetValue(targetSM, boundaryInChild);
                    Debug.Log($"BoundaryManager auto-filled with {boundaryInChild.name}");
                }
                mgAssigned++;
            }
        }
        #endregion

        #region FaceDirectionManager
        FieldInfo faceDirectionMgField = targetType.GetField("faceDirectionMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (faceDirectionMgField != null)
        {
            mgCount++;
            FaceDirectionManager[] foundComponents = targetSM.GetComponentsInChildren<FaceDirectionManager>(true);
            if (foundComponents.Length == 0)
            {
                Debug.LogWarning("Missing Basic FaceDirectionMg");
            }
            else if (foundComponents.Length > 1)
            {
                Debug.LogWarning("Multiple Basic FaceDirectionMg");
            }
            else
            {
                FaceDirectionManager found = foundComponents[0];
                object currentValue = faceDirectionMgField.GetValue(targetSM);
                if (currentValue != (object)found)
                {
                    faceDirectionMgField.SetValue(targetSM, found);
                    Debug.Log($"FaceDirectionManager auto-filled with {found.name}");
                }
                mgAssigned++;
            }
        }
        #endregion

        #region MovementManager
        FieldInfo movementMgField = targetType.GetField("movementMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (movementMgField != null)
        {
            mgCount++;
            MovementManager[] foundComponents = targetSM.GetComponentsInChildren<MovementManager>(true);
            if (foundComponents.Length == 0)
            {
                Debug.LogWarning("Missing Basic MovementMg");
            }
            else if (foundComponents.Length > 1)
            {
                Debug.LogWarning("Multiple Basic MovementMg");
            }
            else
            {
                MovementManager found = foundComponents[0];
                object currentValue = movementMgField.GetValue(targetSM);
                if (currentValue != (object)found)
                {
                    movementMgField.SetValue(targetSM, found);
                    Debug.Log($"MovementManager auto-filled with {found.name}");
                }
                mgAssigned++;
            }
        }
        #endregion

        #region RaycastManager
        FieldInfo raycastMgField = targetType.GetField("raycastMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (raycastMgField != null)
        {
            mgCount++;
            RaycastManager[] foundComponents = targetSM.GetComponentsInChildren<RaycastManager>(true);
            if (foundComponents.Length == 0)
            {
                Debug.LogWarning("Missing Basic RaycastMg");
            }
            else if (foundComponents.Length > 1)
            {
                Debug.LogWarning("Multiple Basic RaycastMg");
            }
            else
            {
                RaycastManager found = foundComponents[0];
                object currentValue = raycastMgField.GetValue(targetSM);
                if (currentValue != (object)found)
                {
                    raycastMgField.SetValue(targetSM, found);
                    Debug.Log($"RaycastManager auto-filled with {found.name}");
                }
                mgAssigned++;
            }
        }
        #endregion

        #region BaseDataManager
        FieldInfo baseDataMgField = targetType.GetField("baseDataMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (baseDataMgField != null)
        {
            mgCount++;
            BaseDataManager[] foundComponents = targetSM.GetComponentsInChildren<BaseDataManager>(true);
            if (foundComponents.Length == 0)
            {
                Debug.LogWarning("Missing Basic BaseDataMg");
            }
            else if (foundComponents.Length > 1)
            {
                Debug.LogWarning("Multiple Basic BaseDataMg");
            }
            else
            {
                BaseDataManager found = foundComponents[0];
                object currentValue = baseDataMgField.GetValue(targetSM);
                if (currentValue != (object)found)
                {
                    baseDataMgField.SetValue(targetSM, found);
                    Debug.Log($"BaseDataManager auto-filled with {found.name}");
                }
                mgAssigned++;
            }
        }
        #endregion

        #region ClickableManager
        FieldInfo clickableMgField = targetType.GetField("clickableMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (clickableMgField != null)
        {
            mgCount++;
            ClickableManager[] foundComponents = targetSM.GetComponentsInChildren<ClickableManager>(true);
            if (foundComponents.Length == 0)
            {
                Debug.LogWarning("Missing Basic ClickableMg");
            }
            else if (foundComponents.Length > 1)
            {
                Debug.LogWarning("Multiple Basic ClickableMg");
            }
            else
            {
                ClickableManager found = foundComponents[0];
                object currentValue = clickableMgField.GetValue(targetSM);
                if (currentValue != (object)found)
                {
                    clickableMgField.SetValue(targetSM, found);
                    Debug.Log($"ClickableManager auto-filled with {found.name}");
                }
                mgAssigned++;
            }
        }
        #endregion

        #region ILayerManager
        FieldInfo layerMgField = targetType.GetField("layerMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (layerMgField != null)
        {
            mgCount++;
            ILayerManager[] layerComponents = targetSM.GetComponentsInChildren<ILayerManager>(true);

            if (layerComponents.Length == 0)
            {
                Debug.LogWarning("Missing Basic LayerMg");
            }
            else if (layerComponents.Length > 1)
            {
                Debug.LogWarning("Multiple Basic LayerMg");
            }
            else
            {
                MonoBehaviour layerComponent = (MonoBehaviour)layerComponents[0];
                object currentValue = layerMgField.GetValue(targetSM);
                if (currentValue != (object)layerComponent)
                {
                    layerMgField.SetValue(targetSM, layerComponent);
                    Debug.Log($"ILayerManager auto-filled with {layerComponent.name}");
                }
                mgAssigned++;
            }
        }
        #endregion

        #region RandomMoveManager
        FieldInfo randomMoveMgField = targetType.GetField("randomMoveMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (randomMoveMgField != null)
        {
            mgCount++;
            RandomMoveManager[] foundComponents = targetSM.GetComponentsInChildren<RandomMoveManager>(true);
            if (foundComponents.Length == 0)
            {
                Debug.LogWarning("Missing RandomMove RandomMoveMg");
            }
            else if (foundComponents.Length > 1)
            {
                Debug.LogWarning("Multiple RandomMove RandomMoveMg");
            }
            else
            {
                RandomMoveManager found = foundComponents[0];
                object currentValue = randomMoveMgField.GetValue(targetSM);
                if (currentValue != (object)found)
                {
                    randomMoveMgField.SetValue(targetSM, found);
                    Debug.Log($"RandomMoveManager auto-filled with {found.name}");
                }
                mgAssigned++;
            }
        }
        #endregion

        #region MountableManager
        FieldInfo mountableMgField = targetType.GetField("mountableMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (mountableMgField != null)
        {
            mgCount++;
            MountableManager[] foundComponents = targetSM.GetComponentsInChildren<MountableManager>(true);
            if (foundComponents.Length == 0)
            {
                Debug.LogWarning("Missing Mounts MountableMg");
            }
            else if (foundComponents.Length > 1)
            {
                Debug.LogWarning("Multiple Mounts MountableMg");
            }
            else
            {
                MountableManager found = foundComponents[0];
                object currentValue = mountableMgField.GetValue(targetSM);
                if (currentValue != (object)found)
                {
                    mountableMgField.SetValue(targetSM, found);
                    Debug.Log($"MountableManager auto-filled with {found.name}");
                }
                mgAssigned++;
            }
        }
        #endregion

        #region MountingAbilityManager
        FieldInfo mountingAbilityMgField = targetType.GetField("mountingAbilityMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (mountingAbilityMgField != null)
        {
            mgCount++;
            MountingAbilityManager[] foundComponents = targetSM.GetComponentsInChildren<MountingAbilityManager>(true);
            if (foundComponents.Length == 0)
            {
                Debug.LogWarning("Missing Mounts MountingAbilityMg");
            }
            else if (foundComponents.Length > 1)
            {
                Debug.LogWarning("Multiple Mounts MountingAbilityMg");
            }
            else
            {
                MountingAbilityManager found = foundComponents[0];
                object currentValue = mountingAbilityMgField.GetValue(targetSM);
                if (currentValue != (object)found)
                {
                    mountingAbilityMgField.SetValue(targetSM, found);
                    Debug.Log($"MountingAbilityManager auto-filled with {found.name}");
                }
                mgAssigned++;
            }
        }
        #endregion

        #region InteractableManager
        FieldInfo interactableMgField = targetType.GetField("interactableMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (interactableMgField != null)
        {
            mgCount++;
            InteractableManager[] foundComponents = targetSM.GetComponentsInChildren<InteractableManager>(true);
            if (foundComponents.Length == 0)
            {
                Debug.LogWarning("Missing Interacts InteractableMg");
            }
            else if (foundComponents.Length > 1)
            {
                Debug.LogWarning("Multiple Interacts InteractableMg");
            }
            else
            {
                InteractableManager found = foundComponents[0];
                object currentValue = interactableMgField.GetValue(targetSM);
                if (currentValue != (object)found)
                {
                    interactableMgField.SetValue(targetSM, found);
                    Debug.Log($"InteractableManager auto-filled with {found.name}");
                }
                mgAssigned++;
            }
        }
        #endregion

        #region InteractAbilityManager
        FieldInfo interactAbilityMgField = targetType.GetField("interactAbilityMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (interactAbilityMgField != null)
        {
            mgCount++;
            InteractAbilityManager[] foundComponents = targetSM.GetComponentsInChildren<InteractAbilityManager>(true);
            if (foundComponents.Length == 0)
            {
                Debug.LogWarning("Missing Interacts InteractAbilityMg");
            }
            else if (foundComponents.Length > 1)
            {
                Debug.LogWarning("Multiple Interacts InteractAbilityMg");
            }
            else
            {
                InteractAbilityManager found = foundComponents[0];
                object currentValue = interactAbilityMgField.GetValue(targetSM);
                if (currentValue != (object)found)
                {
                    interactAbilityMgField.SetValue(targetSM, found);
                    Debug.Log($"InteractAbilityManager auto-filled with {found.name}");
                }
                mgAssigned++;
            }
        }
        #endregion

        Debug.Log("¡X ¡X ¡X");
        Debug.Log($"<color=yellow>Managers set ({mgAssigned}/{mgCount})</color>");        

        Debug.Log("¡X ¡X ¡X");
        Debug.Log("<color=#D080FF>States</color>");
        Debug.Log("¡X ¡X ¡X");

        FieldInfo[] fields = targetType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        int stateCount =0;
        int stateAssinged =0;
        foreach (var field in fields)
        {
            if (field.FieldType == typeof(StateBase))
            {
                stateCount++;
                object value = field.GetValue(targetSM);
                if (value == null)
                {                    
                    Debug.LogWarning($"{field.Name} is not assigned");
                }
                else
                {
                    stateAssinged++;
                }
            }
        }
        Debug.Log("¡X ¡X ¡X");
        Debug.Log($"<color=#D080FF>States set ({stateAssinged}/{stateCount})</color>");
    }

    private void ClearConsole()
    {
        var logEntries = Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        var clearMethod = logEntries?.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
        clearMethod?.Invoke(null, null);
    }

    private GameObject FindPrefabByName(string prefabName)
    {
        string[] guids = AssetDatabase.FindAssets($"{prefabName} t:Prefab");

        if (guids.Length == 0)
        {
            Debug.LogWarning($"No prefab found with name containing '{prefabName}'.");
            return null;
        }
        else if (guids.Length > 1)
        {
            Debug.LogWarning($"Multiple prefabs found whose names contain '{prefabName}':");
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Debug.Log($" ¡÷ {path}");
            }
            return null;
        }
        else
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null)
            {
                Debug.LogWarning($"Asset at path {path} could not be loaded as a prefab.");
                return null;
            }
            if (prefab.name != prefabName)
            {
                Debug.LogWarning($"Similar prefab found, but name does not exactly match '{prefabName}'. Found prefab name: '{prefab.name}' at path: {path}");
                return null;
            }
            return prefab;
        }
    }
    private GameObject CreateChildIfMissing(string childName)
    {
        Transform existing = transform.Find(childName);
        if (existing == null)
        {
            GameObject child = new GameObject(childName);
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            Debug.Log($"Created child object: {childName}");
            return child;
        }
        else
        {
            Debug.Log($"Child \"{childName}\" already exists, skipped.");
            return null;
        }
    }
}
