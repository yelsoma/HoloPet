using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Released_CheckMount : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IMountingAbilitySM mountingAbilitySM;
    [SerializeField] private float checkDistanceDown; 

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

        mountingAbilitySM = GetComponentInParent<IMountingAbilitySM>();
        if(mountingAbilitySM == null)
        {
            Debug.LogError($"{transform} ¡X no mountingAbilitySM found in parent.");
        }
    }

    public override void Enter()
    {        
    }

    public override void StateUpdate()
    {
        if (mountingAbilitySM.MountingAbilityMg.TrySetMountWithRaycast(Vector2.down, checkDistanceDown))
        {     
            stateMachine.ChangeState(mountingAbilitySM.StateMounting);
            return;
        }
        if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicSM.StateIdle);
        }
        else
        {
            stateMachine.ChangeState(basicSM.StateInAir);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
