using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
// States
public partial class StateMachineMaker : MonoBehaviour
{
    private void AssignStates(Type targetType)
    {
        TryFindStateField("Idle_Nor", targetType, "stateIdle");
        TryFindStateField("InAir_Nor", targetType, "stateInAir");
        TryFindStateField("Grabbed_Nor", targetType, "stateGrabbed");
        TryFindStateField("Clicked_Nor", targetType, "stateClicked");
        TryFindStateField("InAir_Nor", targetType, "stateReleased");
        TryFindStateField("Spawn_Nor", targetType, "stateSpawn");
        TryFindStateField("Wander_Nor", targetType, "stateWander");
        TryFindStateField("Mounting_Nor", targetType, "stateMounting");
        TryFindStateField("FollowTarget_Nor", targetType, "stateFollowTarget");
    }
    private bool TryFindStateField(string prefabName, Type type, string fieldName)
    {
        FieldInfo field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (field != null)
        {
            object value = field.GetValue(targetSM);
            if ((UnityEngine.Object)value == null)
            {
                GenerateStateAndAssign(prefabName, field);
            }
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
            instance.transform.SetParent(statesFolder.transform, false); // <- Corrected
            Log($"{prefabName} prefab instantiated.");

            StateBase found = instance.GetComponent<StateBase>();
            object currentValue = field.GetValue(targetSM);
            if (found != null && !Equals(found, currentValue))
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
}
