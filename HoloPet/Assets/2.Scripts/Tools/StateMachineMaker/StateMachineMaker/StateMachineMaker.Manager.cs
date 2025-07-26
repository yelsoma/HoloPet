using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
// Manager
public partial class StateMachineMaker : MonoBehaviour
{
    private void AssignManagers(Type targetType)
    {
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
            Log($"Multiple {typeof(T).Name} found.", warn: true);

        if (foundComponents.Length == 1)
        {
            T found = foundComponents[0];
            object currentValue = field.GetValue(targetSM);
            if (!Equals(currentValue, found))
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
            if (!Equals(currentValue, found))
            {
                fieldToAssign.SetValue(targetSM, found);
                Log($"{typeof(T).Name} auto-filled with {((Component)(object)found).name}");
            }
            mgAssigned++;
        }
    }
}
