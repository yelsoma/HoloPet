using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_DummyRegen : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private AttackableMod attackableMod;
    [SerializeField] private float waitTime;
    private float waitTimeNow;
    private float hpLastFrame;

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

        IAttackableMod iAttackableMod = GetComponentInParent<IAttackableMod>();
        if(iAttackableMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no iAttackableMod found in parent.");
        }
        else
        {
            attackableMod = iAttackableMod.AttackableMod;
        }
    }

    public override void Enter()
    {
        if (!basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicMod.StateInAir);
        }
        attackableMod.DefenceStatMg.HpModify(1);
        waitTimeNow = waitTime;
        hpLastFrame = attackableMod.DefenceStatMg.GetHPNow();
    }

    public override void StateUpdate()
    {       
        if (hpLastFrame == attackableMod.DefenceStatMg.GetHPNow())
        {            
            if (waitTimeNow > 0)
            {
                waitTimeNow -= Time.deltaTime;
                return;
            }
            else
            {
                attackableMod.DefenceStatMg.ResetHP();
                stateMachine.ChangeState(basicMod.StateIdle);
            }
        }
        else
        {
            waitTimeNow = waitTime;
            hpLastFrame = attackableMod.DefenceStatMg.GetHPNow();
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
