using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMem_Bully : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractAbilitySM interactAbilitySM;

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
        //interactAbilitySM.InteractAbilityMg.GetTargetIInteractable().OnExitInteracted += InteractTarget_OnExitInteract;
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
    }
    #endregion
}
