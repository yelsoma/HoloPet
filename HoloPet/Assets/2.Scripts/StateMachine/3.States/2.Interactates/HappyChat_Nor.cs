using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyChat_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractAbilitySM interactAbilitySM;
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

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
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
        
        interactAbilitySM.InteractAbilityMg.GetTargetInteractableMg().OnExitInteracted += InteractTarget_OnExitInteract;
        if (interactAbilitySM.InteractAbilityMg.GetTargetInteractableMg() != null)
        {          
            if (interactAbilitySM.InteractAbilityMg.GetIsTargetRight())
            {
                basicSM.FaceDirectionMg.SetFaceRight();
            }
            else
            {
                basicSM.FaceDirectionMg.SetFaceLeft();
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
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
        StopCoroutine(jumpCoroutine);
        interactAbilitySM.InteractAbilityMg.GetTargetInteractableMg().OnExitInteracted -= InteractTarget_OnExitInteract;
        interactAbilitySM.InteractAbilityMg.ExitInteractingEvent();      
    }
    #endregion

    private void InteractTarget_OnExitInteract(object sender, System.EventArgs e)
    {
        stateMachine.ChangeState(basicSM.StateInAir);
    }

    private IEnumerator CoStartJump()
    {
        jumpCountLeft = jumpCount;
        basicSM.PhysicsMg.SetJump(jumpUpPower);
        basicSM.PhysicsMg.ResetFall();
        while (jumpCountLeft > 0)
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
                    jumpCountLeft--;
                    basicSM.PhysicsMg.SetJump(jumpUpPower);
                    basicSM.PhysicsMg.ResetFall();
                }
            }
            yield return null;
        }
    }
}
