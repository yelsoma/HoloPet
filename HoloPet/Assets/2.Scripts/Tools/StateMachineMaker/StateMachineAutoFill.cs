using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

public class StateMachineAutoFill : MonoBehaviour
{
    [SerializeField] private StateMachineBase targetSM;
    [SerializeField] private GameObject statesFolder;
    [SerializeField] private GameObject managerFolder;

    int mgCount;
    int mgAssigned;
    [ContextMenu("Auto Fill Everything")]
    private void AutoFill()
    {
        ClearConsole();
        mgCount = 0;
        mgAssigned = 0;

        StateMachineBase targetStateMachineBase = GetComponent<StateMachineBase>();
        if (targetSM != targetStateMachineBase)
        {
            targetSM = targetStateMachineBase;
        }

        Type targetType = targetSM.GetType();

        Debug.Log($"<color=#D080FF>Setting {targetType.Name} For {targetSM.transform.name}</color>");

        CreateFolderIfMissing();

        Debug.Log("<color=yellow>Setting Managers</color>");

        #region BoundaryManager
        FieldInfo boundaryMgField = targetType.GetField("boundaryMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (boundaryMgField != null)
        {
            GenerateManagerAndAssign<BoundaryManager>("BoundaryMg", boundaryMgField);
        }
        #endregion

        #region FaceDirectionManager
        FieldInfo faceDirectionMgField = targetType.GetField("faceDirectionMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (faceDirectionMgField != null)
        {
            GenerateManagerAndAssign<FaceDirectionManager>("FaceDirectionMg", faceDirectionMgField);
        }
        #endregion

        #region MovementManager
        FieldInfo movementMgField = targetType.GetField("movementMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (movementMgField != null)
        {
            GenerateManagerAndAssign<MovementManager>("MovementMg", movementMgField);
        }
        #endregion

        #region RaycastManager
        FieldInfo raycastMgField = targetType.GetField("raycastMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (raycastMgField != null)
        {
            GenerateManagerAndAssign<RaycastManager>("RaycastMg", raycastMgField);
        }
        #endregion

        #region BaseDataManager
        FieldInfo baseDataMgField = targetType.GetField("baseDataMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (baseDataMgField != null)
        {
            GenerateManagerAndAssign<BaseDataManager>("BaseDataMg", baseDataMgField);
        }
        #endregion

        #region ClickableManager
        FieldInfo clickableMgField = targetType.GetField("clickableMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (clickableMgField != null)
        {
            GenerateManagerAndAssign<ClickableManager>("ClickableMg", clickableMgField);
        }
        #endregion

        #region ILayerManager
        FieldInfo layerMgField = targetType.GetField("layerMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (layerMgField != null)
        {
            OnlyManagerAssign<ILayerManager>(layerMgField);
        }
        #endregion

        #region RandomMoveManager
        FieldInfo randomMoveMgField = targetType.GetField("randomMoveMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (randomMoveMgField != null)
        {
            GenerateManagerAndAssign<RandomMoveManager>("RandomMoveMg", randomMoveMgField);
        }
        #endregion

        #region MountableManager
        FieldInfo mountableMgField = targetType.GetField("mountableMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (mountableMgField != null)
        {
            GenerateManagerAndAssign<MountableManager>("MountableMg", mountableMgField);
        }
        #endregion

        #region MountingAbilityManager
        FieldInfo mountingAbilityMgField = targetType.GetField("mountingAbilityMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (mountingAbilityMgField != null)
        {
            GenerateManagerAndAssign<MountingAbilityManager>("MountingAbilityMg", mountingAbilityMgField);
        }
        #endregion

        #region InteractableManager
        FieldInfo interactableMgField = targetType.GetField("interactableMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (interactableMgField != null)
        {
            GenerateManagerAndAssign<InteractableManager>("InteractableMg", interactableMgField);
        }
        #endregion

        #region InteractAbilityManager
        FieldInfo interactAbilityMgField = targetType.GetField("interactAbilityMg", BindingFlags.NonPublic | BindingFlags.Instance);
        if (interactAbilityMgField != null)
        {
            GenerateManagerAndAssign<InteractAbilityManager>("InteractAbilityMg", interactAbilityMgField);
        }
        #endregion

        Debug.Log($"<color=yellow>Managers set ({mgAssigned}/{mgCount})</color>");
       
    }

    [ContextMenu("States Check")]
    private void StatesCheck()
    {
        #region StatesCheck
        Debug.Log("<color=yellow>States</color>");
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
                    Debug.LogWarning($"<color=#FF8080>{field.Name} is not assigned</color>");
                }
                else
                {
                    stateAssigned++;
                }
            }
        }
        Debug.Log($"<color=yellow>States set ({stateAssigned}/{stateCount})</color>");
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
            Debug.Log($"Created child object: States");
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
            Debug.Log($"Created child object: Managers");
        }
        else
        {
            managerFolder = mgTf.gameObject;
        }
    }

    private void GenerateManagerAndAssign<T>(string prefabName,FieldInfo field) where T : Component
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
                Debug.Log($"[{typeof(T).Name}] prefab instantiated.");
            }
            else
            {
                Debug.LogWarning($"<color=#FF8080>Missing {typeof(T).Name} and no prefab found.</color>");
            }
        }

        foundComponents = targetSM.GetComponentsInChildren<T>(true);

        if (foundComponents.Length > 1)
        {
            Debug.LogWarning($"<color=#FF8080>Multiple {typeof(T).Name} found.</color>");
        }

        if (foundComponents.Length == 1)
        {
            T found = foundComponents[0];
            object currentValue = field.GetValue(targetSM);
            if (currentValue != (object)found)
            {
                field.SetValue(targetSM, found);
                Debug.Log($"{typeof(T).Name} auto-filled with {found.name}");
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
            Debug.LogWarning($"<color=#FF8080>Missing {typeof(T).Name}.</color>");
        }
        else if (foundComponents.Length > 1)
        {
            Debug.LogWarning($"<color=#FF8080>Multiple {typeof(T).Name} found.</color>");
        }
        else
        {
            T found = foundComponents[0];
            object currentValue = fieldToAssign.GetValue(targetSM);
            if (currentValue != (object)found)
            {
                fieldToAssign.SetValue(targetSM, found);
                Debug.Log($"{typeof(T).Name} auto-filled with {((Component)(object)found).name}");
            }
            mgAssigned++;
        }
    }

    private GameObject FindPrefabByName(string prefabName)
    {
        string[] guids = AssetDatabase.FindAssets($"{prefabName} t:Prefab");

        if (guids.Length == 0)
        {
            Debug.LogWarning($"<color=#FF8080>No prefab found with name containing '{prefabName}'.</color>");
            return null;
        }
        else if (guids.Length > 1)
        {
            Debug.LogWarning($"<color=#FF8080>Multiple prefabs found whose names contain '{prefabName}':</color>");
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Debug.Log($"<color=#FF8080> ¡÷ {path}</color>");
            }
            return null;
        }
        else
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null)
            {
                Debug.LogWarning($"<color=#FF8080>Asset at path {path} could not be loaded as a prefab.</color>");
                return null;
            }
            if (prefab.name != prefabName)
            {
                Debug.LogWarning($"<color=#FF8080>Similar prefab found, but name does not exactly match '{prefabName}'. Found prefab name: '{prefab.name}' at path: {path}</color>");
                return null;
            }
            return prefab;
        }
    }
}
