using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IMountableSM mountableSM;
    private IDriveSM DriveSM;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform} ¡X no basicSM found in parent.");
        }

        mountableSM = GetComponentInParent<IMountableSM>();
        if(mountableSM == null)
        {
            Debug.LogError($"{transform} ¡X no mountableSM found in parent.");
        }

        DriveSM = GetComponentInParent<IDriveSM>();
        if (DriveSM == null)
        {
            Debug.LogError($"{transform} ¡X no IDriveSM found in parent.");
        }        
    }

    public override void Enter()
    {
        if (mountableSM.MountableMg.GetIsMounted())
        {
            stateMachine.ChangeState(DriveSM.StateDrive);
            return;
        }
        mountableSM.MountableMg.OnChangeMounted += MountableMg_OnChangeMounted;
    }    

    public override void StateUpdate()
    {
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        mountableSM.MountableMg.OnChangeMounted -= MountableMg_OnChangeMounted;
    }

    private void MountableMg_OnChangeMounted(object sender, System.EventArgs e)
    {
        if (mountableSM.MountableMg.GetIsMounted())
        {
            stateMachine.ChangeState(DriveSM.StateDrive);
        }        
    }
}
