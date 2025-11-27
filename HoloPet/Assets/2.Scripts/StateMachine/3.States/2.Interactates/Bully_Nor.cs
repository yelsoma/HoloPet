using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bully_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private IInteractAbilitySM interactAbilitySM;
    private InteractAbilityManager myInteractAbilityMg;
    private InteractableManager interactTargetMg;
    private float punchCountDownNow;
    private bool punched;
    private float fallSpeedNow;
    private float fallSpeedIncrease;
    private bool exitToIdle;
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

        interactAbilitySM = GetComponentInParent<IInteractAbilitySM>();
        if (interactAbilitySM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IInteractAbilitySM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        myInteractAbilityMg = interactAbilitySM.InteractAbilityMg;
        interactTargetMg = myInteractAbilityMg.GetTargetInteractableMg();
        if (interactTargetMg != null)
        {
            if (myInteractAbilityMg.GetIsTargetRight())
            {
                basicMod.FaceDirectionMg.SetFaceRight();
            }
            else
            {
                basicMod.FaceDirectionMg.SetFaceLeft();
            }
            interactTargetMg.OnExitInteracted += HoloMem_Bully_OnExitInteracted;
            punchCountDownNow = 0.15f;
            punched = false;
            fallSpeedNow = 0;
            fallSpeedIncrease = 6.5f;
            exitToIdle = false;
        }
        else
        {
            // exit to idle
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
        punchCountDownNow -= 1*Time.deltaTime;
        if (punchCountDownNow <= 0 && punched ==false)
        {           
            myInteractAbilityMg.TriggerInteractingEvent();
            punched = true;
        }
        if (fallSpeedNow <= 9f)
        {
            fallSpeedNow = fallSpeedNow + fallSpeedIncrease * Time.deltaTime;
        }
        basicMod.PhysicsMg.MoveDown(fallSpeedNow);
        if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            if(punchCountDownNow <= -0.5)
            {
                stateMachine.ChangeState(basicMod.StateIdle);
            }
        }
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
        interactTargetMg.OnExitInteracted -= HoloMem_Bully_OnExitInteracted;
        myInteractAbilityMg.ExitInteractingEvent();
        interactTargetMg = null;
    }
    #endregion

    private void HoloMem_Bully_OnExitInteracted(object sender, System.EventArgs e)
    {
        if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicMod.StateIdle);
        }
        stateMachine.ChangeState(basicMod.StateInAir);
    }
}
