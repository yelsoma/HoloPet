using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Box : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountableManager mountableManager;

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
            mountableManager = iMountableMod.MountableMod.MountableMg;
    }

    public override void Enter()
    {
        mountableManager.OnChangeMounted += MountableManager_OnChangeMounted;
        if (!basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicMod.StateInAir);
        }
        if (mountableManager.GetIsMounted())
        {
            GameController.Instance.Inventory.SetUIActive(true);
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
        mountableManager.OnChangeMounted -= MountableManager_OnChangeMounted;
    }
    private void MountableManager_OnChangeMounted(object sender, System.EventArgs e)
    {
        GameController.Instance.Inventory.SetUIActive(true);
    }
}
