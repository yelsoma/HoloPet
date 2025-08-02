using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractThink_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractAbilitySM interactAbilitySM;
    [SerializeField] private float interactDistance;
    private InteractAbilityManager myInteractMg;
    private InteractableManager targetInteractMg;
    private bool targetIsFarX;
    private bool targetIsFarY;
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
        if (!basicSM.RaycastMg.TrySetRaycastBothSide(10))
        {
            //no Raycas hit
            stateMachine.ChangeState(basicSM.StateIdle);
            return;
        }
        if (!interactAbilitySM.InteractAbilityMg.TrySetTargetWihtRaycastHits(basicSM.RaycastMg.GetRaycastHits()))
        {
            //no Interactable
            stateMachine.ChangeState(basicSM.StateIdle);
            return;
        }
        if (!interactAbilitySM.InteractAbilityMg.TryMatchOptionsChooseWithBothChance())
        {
            //no Interact option match
            stateMachine.ChangeState(basicSM.StateIdle);
            return;
        }
        myInteractMg = interactAbilitySM.InteractAbilityMg;
        targetInteractMg = myInteractMg.GetTargetIInteractable();
        targetIsFarX = myInteractMg.GetIsTargetFarX(interactDistance);
        targetIsFarY = myInteractMg.GetIsTargetFarY(interactDistance);
    }
    public override void StateUpdate()
    {
        if (targetIsFarX == false && targetIsFarY == true)
        {
            stateMachine.ChangeState(interactAbilitySM.StateInteractFollowY);
        }
        else
        {
            stateMachine.ChangeState(interactAbilitySM.StateInteractFollowX);
        }
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
    }
    #endregion
}
