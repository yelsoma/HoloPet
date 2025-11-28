using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyChat_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private InteractAbilityMod interactAbilityMod;
    [SerializeField] private float jumpUpPower;
    [SerializeField] private int jumpCount;
    private float jumpCountLeft;
    private Coroutine jumpCoroutine;
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

        IInteractAbilityMod iInteractAbilityMod = GetComponentInParent<IInteractAbilityMod>();
        if (iInteractAbilityMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no iInteractAbilityMod found in parent.");
        }
        else
        {
            interactAbilityMod = iInteractAbilityMod.InteractAbilityMod;
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        
        interactAbilityMod.InteractAbilityMg.GetTargetInteractableMg().OnExitInteracted += InteractTarget_OnExitInteract;
        if (interactAbilityMod.InteractAbilityMg.GetTargetInteractableMg() != null)
        {          
            if (interactAbilityMod.InteractAbilityMg.GetIsTargetRight())
            {
                basicMod.FaceDirectionMg.SetFaceRight();
            }
            else
            {
                basicMod.FaceDirectionMg.SetFaceLeft();
            }
            //start jump
            jumpCoroutine = StartCoroutine(CoStartJump());
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
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
        StopCoroutine(jumpCoroutine);
        interactAbilityMod.InteractAbilityMg.GetTargetInteractableMg().OnExitInteracted -= InteractTarget_OnExitInteract;
        interactAbilityMod.InteractAbilityMg.ExitInteractingEvent();      
    }
    #endregion

    private void InteractTarget_OnExitInteract(object sender, System.EventArgs e)
    {
        stateMachine.ChangeState(basicMod.StateInAir);
    }

    private IEnumerator CoStartJump()
    {
        jumpCountLeft = jumpCount;
        basicMod.PhysicsMg.SetJump(jumpUpPower);
        basicMod.PhysicsMg.ResetFall();
        while (jumpCountLeft > 0)
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
                    jumpCountLeft--;
                    basicMod.PhysicsMg.SetJump(jumpUpPower);
                    basicMod.PhysicsMg.ResetFall();
                }
            }
            yield return null;
        }
    }
}
