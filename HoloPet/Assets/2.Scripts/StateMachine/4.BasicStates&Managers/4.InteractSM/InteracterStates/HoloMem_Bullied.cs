using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMem_Bullied : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractableSM interactableSM;
    private InteractableManager myInteractableMg;
    private InteractAbilityManager interacterMg;
    private bool isHit =false;
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

        interactableSM = GetComponentInParent<IInteractableSM>();
        if (interactableSM == null)
        {
            Debug.LogError($"{transform} ¡X no IInteractableSM found in parent.");
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
        }
        else
        {
            // exit to idle
            stateMachine.ChangeState(basicSM.StateIdle);
            return;
        }
        interacterMg.OnTriggerInteracting += TriggerInteracting;
        interacterMg.OnExitInteracting += ExitInteracting;
    }
    public override void StateUpdate()
    {

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
        if (isHit == false)
        {
            stateMachine.ChangeState(basicSM.StateIdle);
        }
    }

    private void TriggerInteracting(object sender, System.EventArgs e)
    {
        isHit = true;
        stateMachine.ChangeState(basicSM.StateClicked);
    }
}
