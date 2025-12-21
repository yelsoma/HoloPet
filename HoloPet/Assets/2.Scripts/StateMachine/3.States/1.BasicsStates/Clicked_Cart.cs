using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicked_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountableMod mountableMod;
    private DriveMod driveMod;

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

        IDriveMod iDriveSM = GetComponentInParent<IDriveMod>();
        if (iDriveSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no cartSM found in parent.");
        }
        else
        {
            driveMod = iDriveSM.DriveMod;
        }
    }

    public override void Enter()
    {
        if (mountableMod.MountableMg.GetIsMounted())
        {
            stateMachine.ChangeState(driveMod.StateDirveJump);
        }
        else
        {
            stateMachine.ChangeState(driveMod.StateClickedNor);
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
