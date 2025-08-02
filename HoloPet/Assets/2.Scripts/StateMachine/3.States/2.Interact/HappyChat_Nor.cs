using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyChat_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractAbilitySM interactAbilitySM;
    [SerializeField] private float jumpUpPower;
    [SerializeField] private float jumpUpDecrese;
    [SerializeField] private int jumpCount;
    private float jumpUpPowerNow;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    private float fallSpeedNow;
    private float jumpCountLeft;
    private Coroutine jumpCoroutine;
    #region AutoSetRef
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform} ¡X no basicSM found in parent.");
        }

        interactAbilitySM = GetComponentInParent<IInteractAbilitySM>();
        if (interactAbilitySM == null)
        {
            Debug.LogError($"{transform} ¡X no IInteractAbilitySM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        
        interactAbilitySM.InteractAbilityMg.GetTargetIInteractable().OnExitInteracted += InteractTarget_OnExitInteract;
        if (interactAbilitySM.InteractAbilityMg.GetTargetIInteractable() != null)
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
        }
        else
        {
            // exit to idle
            stateMachine.ChangeState(basicSM.StateIdle);
            return;
        }
    }
    public override void StateUpdate()
    {
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
        StopCoroutine(jumpCoroutine);
        interactAbilitySM.InteractAbilityMg.GetTargetIInteractable().OnExitInteracted -= InteractTarget_OnExitInteract;
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
        jumpUpPowerNow = jumpUpPower;
        fallSpeedNow = 0f;
        while (jumpCountLeft > 0)
        {
            if (jumpUpPowerNow > 0)
            {
                basicSM.MovementMg.MoveUp(jumpUpPowerNow);
                jumpUpPowerNow -= jumpUpDecrese * Time.deltaTime;
            }
            else
            {
                basicSM.MovementMg.MoveDown(fallSpeedNow);
                if (fallSpeedNow <= fallSpeedMax)
                {
                    fallSpeedNow += fallSpeedIncreese * Time.deltaTime;
                }
                if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
                {
                    jumpCountLeft--;
                    jumpUpPowerNow = jumpUpPower;
                    fallSpeedNow = 0f;
                }
            }
            yield return null;
        }
    }
}
