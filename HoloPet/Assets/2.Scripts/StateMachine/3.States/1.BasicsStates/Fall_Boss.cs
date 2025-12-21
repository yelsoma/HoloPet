using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Boss : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        basicMod = GetComponentInParent<IBasicMod>().BasicMod;
        if (basicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
    }

    public override void Enter()
    {
        basicMod.PhysicsMg.ResetFall();
        basicMod.PhysicsMg.SetJump(3);      
    }

    public override void StateUpdate()
    {
        //fall
        
        if (basicMod.PhysicsMg.KeepJump())
        {
            
        }
        else
        {
            basicMod.PhysicsMg.KeepFall(2.5f);
        }
        
        if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            List<StateMachineBase> playerSMs = GameController.Instance.StateMachineListMg.GetPlayerGangList();
            foreach(StateMachineBase playerSM in playerSMs)
            {
                IAttackableMod iAttackableMod = playerSM.GetComponent<IAttackableMod>();
                if(iAttackableMod != null && iAttackableMod.AttackableMod.AttackableMg.GetIsAttackable())
                {
                    iAttackableMod.AttackableMod.AttackableMg.SetAttackKnockBack(2.5f,6f, true);
                }
            }
            //exit to idle
            stateMachine.ChangeState(basicMod.StateIdle);
        }
    }

    public override void StateLateUpdate()
    {     
    }

    public override void Exit()
    {
    }
}
