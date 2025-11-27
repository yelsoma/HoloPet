using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Released_CheckMount : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountingAbilityMod mountingAbilityMod;
    [SerializeField] private float checkDistanceDown; 

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

        IMountingAbilityMod iMountingAbilityMod = GetComponentInParent<IMountingAbilityMod>();
        if(iMountingAbilityMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no imountingAbilityMod found in parent.");
        }
        else
        {
            mountingAbilityMod = iMountingAbilityMod.MountingAbilityMod;
        }
    }

    public override void Enter()
    {        
    }

    public override void StateUpdate()
    {
        if (mountingAbilityMod.MountingAbilityMg.TrySetMountWithRaycast(Vector2.down, checkDistanceDown))
        {     
            stateMachine.ChangeState(mountingAbilityMod.StateMounting);
            return;
        }
        if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicMod.StateIdle);
        }
        else
        {
            stateMachine.ChangeState(basicMod.StateInAir);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
