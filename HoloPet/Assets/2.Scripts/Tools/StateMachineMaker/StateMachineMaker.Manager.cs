#if UNITY_EDITOR
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
        TrySetManagerField<PhysicsManager>("PhysicsMg", targetType, "physicsMg");
        TrySetManagerField<RaycastManager>("RaycastMg", targetType, "raycastMg");
        TrySetManagerField<ClickableManager>("ClickableMg", targetType, "clickableMg");
        TrySetManagerField<RandomMoveManager>("RandomMoveMg", targetType, "randomMoveMg");
        TrySetManagerField<MountableManager>("MountableMg", targetType, "mountableMg");
        TrySetManagerField<MountingAbilityManager>("MountingAbilityMg", targetType, "mountingAbilityMg");
        TrySetManagerField<InteractableManager>("InteractableMg", targetType, "interactableMg");
        TrySetManagerField<InteractAbilityManager>("InteractAbilityMg", targetType, "interactAbilityMg");
        TrySetManagerField<AttackableManager>("AttackableMg", targetType, "attackableMg");
        TrySetManagerField<AttackAbilityManager>("AttackAbilityMg", targetType, "attackAbilityMg");
        TrySetManagerField<TextLogManager>("TextLogMg", targetType, "textLogMg");
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
        bool instantiate = false;
        if (foundComponents.Length == 0)
        {
            GameObject prefab = FindPrefabByName(prefabName);
            if (prefab != null)
            {
                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                instance.transform.SetParent(managerFolder.transform, false);
                instantiate = true; 
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
                if (instantiate)
                {
                    Log($"{found.name} auto- spawn and filled ");
                }
                else
                {
                    Log($"{found.name} auto- filled ");
                }
            }
            mgAssigned++;
        }
    }

    private void AssignBodyPart(Type targetType)
    {
        mgCount++; // for layer manager
        ILayerManager layerManager = bodyPartsFolder.GetComponentInChildren<ILayerManager>();
       if(layerManager == null)
        {
            GameObject prefab = FindPrefabByName("BasicBody");
            if (prefab != null)
            {
                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                instance.transform.SetParent(bodyPartsFolder.transform, false);
                FieldInfo layerMgField = targetType.GetField("layerMg", BindingFlags.NonPublic | BindingFlags.Instance);
                object currentValue = layerMgField.GetValue(targetSM);
                ILayerManager instanceLayerManager = instance.transform.GetComponent<ILayerManager>();
                if (!Equals(currentValue,instanceLayerManager))
                {
                    layerMgField.SetValue(targetSM, instanceLayerManager);
                    Log($" BasicBody auto- spawn and filled in LayerMg");
                }
                mgAssigned++;
            }
            else
            {
                Log($"Missing BasicBody and no prefab found.", warn: true);
            }
        }
        else
        {
            mgAssigned++;
        }
    }
}
#endif