#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
// Core
public partial class StateMachineMaker : MonoBehaviour
{
    [SerializeField] private StateMachineBase targetSM;
    [SerializeField] private GameObject statesFolder;
    [SerializeField] private GameObject managerFolder;
    [SerializeField] private bool RGB = false;

    private int rainbowIndex = 0;
    private int mgCount;
    private int mgAssigned;
    private int stateCount;
    private int stateAssigned;

    private static readonly string[] RainbowColors = new[]
    {
        "#FF4C4C", "#FF884C", "#FFE14C", "#4CFF4C",
        "#4CFFFF", "#4C4CFF", "#B44CFF", "#FF4CA3"
    };


    [ContextMenu("Magic Button (¡ä^o^)>-¡¸~)))")]
    private void AutoFill()
    {
        ClearConsole();
        mgCount = 0;
        mgAssigned = 0;
        stateCount = 0;
        stateAssigned = 0;
        rainbowIndex = 0;

        StateMachineBase targetStateMachineBase = GetComponent<StateMachineBase>();
        if (targetSM != targetStateMachineBase)
            targetSM = targetStateMachineBase;

        Type targetType = targetSM.GetType();
        Debug.Log("<color=White>(¡ä ¢X ¡¾¢X)> <( ^£s^ ¡ä)</color>");
        Debug.Log($"<color=yellow>Setting {targetType.Name} For {targetSM.transform.name}</color>");

        CreateFolderIfMissing();
        Debug.Log($"<color=White> ------------- </color>");

        Debug.Log("<color=yellow>Setting Managers</color>");
        Debug.Log($"<color=White> ( </color>");

        // Manager Assignment
        AssignManagers(targetType);
        Debug.Log($"<color=White> ) </color>");
        Debug.Log($"<color=yellow> Managers ({mgAssigned}/{mgCount})</color>");
        Debug.Log($"<color=White> ------------- </color>");

        //states
        Debug.Log("<color=yellow>Setting States</color>");
        Debug.Log($"<color=White> ( </color>");

        AssignStates(targetType);

        FieldInfo[] stateFields = targetType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        foreach (var field in stateFields)
        {
            if (field.FieldType == typeof(StateBase))
            {
                stateCount++;
                object value = field.GetValue(targetSM);
                if ((UnityEngine.Object)value == null)
                {
                    Log($"{field.Name} is not assigned", warn: true);
                }
                else
                {
                    stateAssigned++;
                }                   
            }
        }
        Debug.Log($"<color=White> ) </color>");
        Debug.Log($"<color=yellow> States ({stateAssigned}/{stateCount})</color>");
    }

}
#endif
