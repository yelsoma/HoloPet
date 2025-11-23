using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockUp_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;

    private bool fallAniTriggered;
    private float knockUpPower;
    private float knockUpFaceDir;
    private float knockBackPower;
    private bool knockUpRight;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
    }

    public override void Enter()
    {
        knockUpPower = UnityEngine.Random.Range(6f, 7f);
        knockBackPower = UnityEngine.Random.Range(0.7f, 2f);
        knockUpFaceDir = UnityEngine.Random.Range(0f, 1f);
        basicSM.PhysicsMg.SetJump(knockUpPower);
        basicSM.PhysicsMg.ResetFall();
       
        fallAniTriggered = false;

        if (knockUpFaceDir <= 0.5f)
        {
            knockUpRight = false;
            basicSM.FaceDirectionMg.SetFaceRight();
        }
        else
        {
            knockUpRight = true;
            basicSM.FaceDirectionMg.SetFaceLeft();
        }
    }

    public override void StateUpdate()
    {
        if (basicSM.PhysicsMg.KeepJump())
        {           
            if (basicSM.BoundaryMg.CheckIsTopBounderyAndResetPos())
            {
                basicSM.PhysicsMg.SetJump(0);
            }
        }
        else
        {
            if (!fallAniTriggered)
            {               
                TriggerAni1(); //Trigger fall ani
                fallAniTriggered = true;
            }

            basicSM.PhysicsMg.KeepFall();

            if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                stateMachine.ChangeState(basicSM.StateIdle);
            }
        }

        if (knockUpRight)
        {
            if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                knockUpRight = false;
            }

            basicSM.PhysicsMg.MoveRight(knockBackPower);
        }
        else
        {
            if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
            {
                knockUpRight = true;
            }

            basicSM.PhysicsMg.MoveLeft(knockBackPower);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
