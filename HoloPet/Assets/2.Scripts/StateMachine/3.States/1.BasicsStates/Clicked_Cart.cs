using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicked_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountableMod mountableMod;
    private IDriveSM driveSM;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        else
        {
            basicMod = ibasicMod.BasicMod;
        }

        IMountableMod imountableMod = GetComponentInParent<IMountableMod>();
        if (imountableMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no mountableMod  found in parent.");
        }
        else
        {
            mountableMod = imountableMod.MountableMod;
        }

        driveSM = GetComponentInParent<IDriveSM>();
        if (driveSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IDriveSM found in parent.");
        }
    }

    public override void Enter()
    {
        if (mountableMod.MountableMg.GetIsMounted())
        {
            stateMachine.ChangeState(driveSM.StateDirveJump);
        }
        else
        {
            stateMachine.ChangeState(driveSM.StateClickedNor);
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
