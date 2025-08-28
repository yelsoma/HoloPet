using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractThink_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractAbilitySM interactAbilitySM;
    [SerializeField] private float serchDistance;
    [SerializeField] private float interactDistance;
    private InteractAbilityManager myInteractMg;
    private InteractableManager targetInteractMg;
    private bool targetIsFarX;
    private bool targetIsFarY;
    [SerializeField] private float waitTime;
    private float waitTimeNow;
    [SerializeField] private float bubbleTime;
    private bool exitToIdle;
    private bool haveATarget;
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
        haveATarget = interactAbilitySM.InteractAbilityMg.GetIsTargetLocked();
        if (!haveATarget)
        {
            if (!interactAbilitySM.InteractAbilityMg.TrySetTargetBothSide(serchDistance))
            {
                //no Interactable
                exitToIdle = true;
                return;
            }
            interactAbilitySM.InteractAbilityMg.SetTargetLocked(true);
        }       
        myInteractMg = interactAbilitySM.InteractAbilityMg;
        targetInteractMg = myInteractMg.GetTargetInteractableMg();
        targetIsFarX = myInteractMg.GetIsTargetFarX(interactDistance);
        targetIsFarY = myInteractMg.GetIsTargetFarY(interactDistance);
        IBasicSM targetBasicSM = targetInteractMg.GetTargetIBasicSM();
        interactAbilitySM.TextLogMg.PopUpTargetIcon(targetBasicSM.BaseDataMg.GetIconSprite(), bubbleTime);
        waitTimeNow = waitTime;
        exitToIdle = false;
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
        if (waitTimeNow >= 0)
        {
            waitTimeNow -= Time.deltaTime;
        }
        else
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
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
    }
    #endregion
}
