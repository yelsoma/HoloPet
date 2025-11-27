using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullied_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
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

        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        else
        {
            basicMod = ibasicMod.BasicMod;
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
                basicMod.FaceDirectionMg.SetFaceRight();
            }
            else
            {
                basicMod.FaceDirectionMg.SetFaceLeft();
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
            if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                stateMachine.ChangeState(basicMod.StateIdle);
                return;
            }
            else
            {
                stateMachine.ChangeState(basicMod.StateInAir);
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
            if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                stateMachine.ChangeState(basicMod.StateIdle) ;
            }
            stateMachine.ChangeState(basicMod.StateInAir);
        }       
    }

    private void TriggerInteracting(object sender, System.EventArgs e)
    {
        isKnockUp = true;
        basicMod.PhysicsMg.SetJump(knockUpPower);
        basicMod.PhysicsMg.ResetFall();
        ChooseKnockUpSide();
    }

    private void KnockBack()
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
            basicMod.PhysicsMg.KeepFall();
            if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                isPanicRun = true;
                StartPanicRun();
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
    private void ChooseKnockUpSide()
    {
        if (myInteractableMg.GetIsInteracterRight())
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
    private void StartPanicRun()
    {
        panicTimeNow = panicTime;
        if (myInteractableMg.GetIsInteracterRight())
        {
            panicRight = false;
            basicMod.FaceDirectionMg.SetFaceLeft();
        }
        else
        {
            panicRight = true;
            basicMod.FaceDirectionMg.SetFaceRight();
        }
    }
    private void PanicRun()
    {
        if(panicTimeNow >= 0)
        {
            panicTimeNow = panicTimeNow - Time.deltaTime;
            if (panicRight)
            {
                basicMod.PhysicsMg.MoveRightMultiply(panicSpeed);
                if (basicMod.BoundaryMg.CheckIsRightBounderyAndResetPos())
                {
                    basicMod.FaceDirectionMg.SetFaceLeft();
                    panicRight = false;
                }
            }
            else
            {
                basicMod.PhysicsMg.MoveLeftMultiply(panicSpeed);
                if (basicMod.BoundaryMg.CheckIsLeftBounderyAndResetPos())
                {
                    basicMod.FaceDirectionMg.SetFaceRight();
                    panicRight = true;
                }               
            }
        }
        else
        {
            stateMachine.ChangeState(basicMod.StateIdle);
        }
    }
}
