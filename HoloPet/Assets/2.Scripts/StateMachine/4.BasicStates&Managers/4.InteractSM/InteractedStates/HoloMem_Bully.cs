using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMem_Bully : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractAbilitySM interactAbilitySM;
    private InteractAbilityManager myInteractAbilityMg;
    private InteractableManager interactTargetMg;
    [SerializeField] private float punchCountDown;
    private float punchCountDownNow;

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
        myInteractAbilityMg = interactAbilitySM.InteractAbilityMg;
        interactTargetMg = myInteractAbilityMg.GetTargetIInteractable();
        if (interactTargetMg != null)
        {
            if (myInteractAbilityMg.GetIsTargetRight())
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
        interactTargetMg.OnExitInteracted += HoloMem_Bully_OnExitInteracted;
        punchCountDownNow = punchCountDown;
    }
    public override void StateUpdate()
    {
        punchCountDownNow -= 1;
        if (punchCountDownNow <= 0)
        {
            myInteractAbilityMg.TriggerInteractingEvent();
            stateMachine.ChangeState(basicSM.StateIdle);
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
        stateMachine.ChangeState(basicSM.StateIdle);
    }
}
