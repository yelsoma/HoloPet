using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullied_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractableSM interactableSM;
    private InteractableManager myInteractableMg;
    private InteractAbilityManager interacterMg;
    private bool isKnockUp;
    private bool isPanicRun;
    private bool exitToIdle;

    //ani
    private bool isHitTriggered;
    private bool isPanicTriggered;

    //KnockBack
    private float knockUpPower = 6f;
    private float knockBackPower = 2.5f;
    private bool knockUpRight;

    //PanicRun
    [SerializeField] private float panicTime;
    private float panicSpeed = 2f;
    private float panicTimeNow;
    private bool panicRight;
    #region AutoSetRef
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

        interactableSM = GetComponentInParent<IInteractableSM>();
        if (interactableSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IInteractableSM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        myInteractableMg = interactableSM.InteractableMg;
        interacterMg = interactableSM.InteractableMg.GetInteracterManager();
        if (interacterMg != null)
        {
            if (myInteractableMg.GetIsInteracterRight())
            {
                basicSM.FaceDirectionMg.SetFaceRight();
            }
            else
            {
                basicSM.FaceDirectionMg.SetFaceLeft();
            }
            interacterMg.OnTriggerInteracting += TriggerInteracting;
            interacterMg.OnExitInteracting += ExitInteracting;
            isKnockUp = false;
            isPanicRun = false;
            isHitTriggered = false;
            isPanicTriggered = false;
            exitToIdle = false; 
        }
        else
        {
            // exit to idle or fall
            exitToIdle = true;           
        }       
}
    public override void StateUpdate()
    {
        if (exitToIdle)
        {
            if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                stateMachine.ChangeState(basicSM.StateIdle);
                return;
            }
            else
            {
                stateMachine.ChangeState(basicSM.StateInAir);
                return;
            }
        }
        if (isKnockUp && !isPanicRun)
        {
            if(isHitTriggered == false)
            {
                TriggerAni1(); // hit ani
                isHitTriggered = true;
            }
            KnockBack();
        }
        if (isPanicRun)
        {
            if (isPanicTriggered == false)
            {
                TriggerAni2(); // run ani
                isPanicTriggered = true;
            }
            PanicRun();
        }
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {        
        interacterMg.OnTriggerInteracting -= TriggerInteracting;
        interacterMg.OnExitInteracting -= ExitInteracting;
        myInteractableMg.ExitInteractedEvent();
        interacterMg = null;
    }
    #endregion
    private void ExitInteracting(object sender, System.EventArgs e)
    {
        if (isKnockUp == false)
        {
            if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                stateMachine.ChangeState(basicSM.StateIdle) ;
            }
            stateMachine.ChangeState(basicSM.StateInAir);
        }       
    }

    private void TriggerInteracting(object sender, System.EventArgs e)
    {
        isKnockUp = true;
        basicSM.PhysicsMg.SetJump(knockUpPower);
        basicSM.PhysicsMg.ResetFall();
        ChooseKnockUpSide();
    }

    private void KnockBack()
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
            basicSM.PhysicsMg.KeepFall();
            if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                isPanicRun = true;
                StartPanicRun();
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
    private void ChooseKnockUpSide()
    {
        if (myInteractableMg.GetIsInteracterRight())
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
    private void StartPanicRun()
    {
        panicTimeNow = panicTime;
        if (myInteractableMg.GetIsInteracterRight())
        {
            panicRight = false;
            basicSM.FaceDirectionMg.SetFaceLeft();
        }
        else
        {
            panicRight = true;
            basicSM.FaceDirectionMg.SetFaceRight();
        }
    }
    private void PanicRun()
    {
        if(panicTimeNow >= 0)
        {
            panicTimeNow = panicTimeNow - Time.deltaTime;
            if (panicRight)
            {
                basicSM.PhysicsMg.MoveRightMultiply(panicSpeed);
                if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
                {
                    basicSM.FaceDirectionMg.SetFaceLeft();
                    panicRight = false;
                }
            }
            else
            {
                basicSM.PhysicsMg.MoveLeftMultiply(panicSpeed);
                if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
                {
                    basicSM.FaceDirectionMg.SetFaceRight();
                    panicRight = true;
                }               
            }
        }
        else
        {
            stateMachine.ChangeState(basicSM.StateIdle);
        }
    }
}
