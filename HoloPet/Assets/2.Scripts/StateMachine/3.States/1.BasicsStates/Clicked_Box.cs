using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicked_Box : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountableManager mountableMg;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");

        IBasicMod iBasicMod = stateMachine.transform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            basicMod = iBasicMod.BasicMod;

        IMountableMod iMountableMod = stateMachine.transform.GetComponent<IMountableMod>();
        if (iMountableMod == null)
            Debug.LogError($"{transform.root.name} ¡X no iMountableMod found in parent.");
        else
            mountableMg = iMountableMod.MountableMod.MountableMg;
    }

    public override void Enter()
    {
        if (GameController.Instance.IsBattleActive)
        {
            stateMachine.ChangeState(basicMod.StateIdle);
            return;
        }
        StateMachineBase[] stateMachines = stateMachine.GetComponentsInChildren<StateMachineBase>();
        GameController.Instance.Inventory.SetUIActive(true);
        stateMachine.ChangeState(basicMod.StateIdle);
        if (mountableMg.GetIsMounted())
        {
            foreach(StateMachineBase sm in stateMachines)
            {
                if (sm == stateMachine)
                    continue;
                BasicMod basicMod = sm.GetComponent<IBasicMod>().BasicMod;
                if (basicMod.ObjectDefinition.ObjectCategory == ObjectCategoryEnum.Item_Temp)
                {
                    sm.ChangeState(basicMod.StateDestroy);
                    continue;
                }                 
                GameController.Instance.Inventory.AddItemToInventoryList(basicMod.ObjectDefinition);
                sm.ChangeState(basicMod.StateDestroy);
            }
        }
    }

    public override void StateUpdate()
    {

    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
