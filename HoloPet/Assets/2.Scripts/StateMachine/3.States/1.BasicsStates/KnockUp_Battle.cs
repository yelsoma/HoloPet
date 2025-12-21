using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockUp_Battle : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private BattleMod battleMod;
    private bool fallAniTriggered;
    private float knockUpPower;
    private float knockUpFaceDir;
    private float knockBackPower;
    private bool knockUpRight;

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

        IBattleMod iBattleMod = stateMachine.transform.GetComponent<IBattleMod>();
        if (iBattleMod == null)
            Debug.LogError($"{transform.root.name} ¡X no iBattleMod found in parent.");
        else
            battleMod = iBattleMod.BattleMod;
    }

    public override void Enter()
    {
        knockUpPower = UnityEngine.Random.Range(6f, 7f);
        knockBackPower = UnityEngine.Random.Range(0.7f, 2f);
        knockUpFaceDir = UnityEngine.Random.Range(0f, 1f);
        basicMod.PhysicsMg.SetJump(knockUpPower);
        basicMod.PhysicsMg.ResetFall();
       
        fallAniTriggered = false;

        if (knockUpFaceDir <= 0.5f)
        {
            knockUpRight = false;
            basicMod.FaceDirectionMg.SetFaceRight();
        }
        else
        {
            knockUpRight = true;
            basicMod.FaceDirectionMg.SetFaceLeft();
        }
    }

    public override void StateUpdate()
    {
        if (basicMod.PhysicsMg.KeepJump())
        {           
            if (basicMod.BoundaryMg.CheckIsTopBounderyAndResetPos())
            {
                basicMod.PhysicsMg.SetJump(0);
            }
        }
        else
        {
            if (!fallAniTriggered)
            {               
                TriggerAni1(); //Trigger fall ani
                fallAniTriggered = true;
            }

            basicMod.PhysicsMg.KeepFall();

            if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                if (battleMod.GetIsInbattle())
                {
                    stateMachine.ChangeState(battleMod.BattleSearch);
                    return;
                }
                stateMachine.ChangeState(basicMod.StateIdle);
                return;
            }
        }

        if (knockUpRight)
        {
            if (basicMod.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                knockUpRight = false;
            }

            basicMod.PhysicsMg.MoveRight(knockBackPower);
        }
        else
        {
            if (basicMod.BoundaryMg.CheckIsLeftBounderyAndResetPos())
            {
                knockUpRight = true;
            }

            basicMod.PhysicsMg.MoveLeft(knockBackPower);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
