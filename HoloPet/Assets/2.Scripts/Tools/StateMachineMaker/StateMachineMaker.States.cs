#if UNITY_EDITOR
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
        TryFindStateField("Fall_Nor", targetType, "stateInAir");
        TryFindStateField("Grabbed_Nor", targetType, "stateGrabbed");
        TryFindStateField("KnockUp_Nor", targetType, "stateClicked");
        TryFindStateField("Fall_Nor", targetType, "stateReleased");
        TryFindStateField("Spawn_Nor", targetType, "stateSpawn");
        TryFindStateField("Wander_Nor", targetType, "stateWander");
        TryFindStateField("Mounting_Nor", targetType, "stateMounting");
        TryFindStateField("InteractThink_Nor", targetType, "stateInteractThink");
        TryFindStateField("InteractFollowY_Nor", targetType, "stateInteractFollowY");
        TryFindStateField("InteractFollowX_Nor", targetType, "stateInteractFollowX");
        TryFindStateField("AttackedKnockback_Nor", targetType, "stateKnockBack");
        TryFindStateField("Destroy_Nor", targetType, "stateDestroy");
        TryFindStateField("HpZero_Nor", targetType, "stateHpZero");
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
        bool instantiate = false;
        GameObject prefab = FindPrefabByName(prefabName);
        if (prefab != null)
        {
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            instance.transform.SetParent(statesFolder.transform, false); // <- Corrected
            instantiate = true;

            StateBase found = instance.GetComponent<StateBase>();
            object currentValue = field.GetValue(targetSM);
            if (found != null && !Equals(found, currentValue))
            {
                field.SetValue(targetSM, found);
                if (instantiate)
                {
                    Log($"{prefabName} auto- spawn and filled");
                }
                else
                {
                    Log($"{prefabName} auto- filled");
                }             
            }
        }
        else
        {
            Log($"Missing {prefabName} and no prefab found.", warn: true);
        }
    }
}
#endif