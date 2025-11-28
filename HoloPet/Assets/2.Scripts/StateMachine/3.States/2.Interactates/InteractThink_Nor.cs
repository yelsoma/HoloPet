using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractThink_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private InteractAbilityMod interactAbilityMod;
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
        haveATarget = interactAbilityMod.InteractAbilityMg.GetIsTargetLocked();
        if (!haveATarget)
        {
            if (!interactAbilityMod.InteractAbilityMg.TrySetTargetBothSide(serchDistance))
            {
                //no Interactable
                exitToIdle = true;
                return;
            }
            interactAbilityMod.InteractAbilityMg.SetTargetLocked(true);
        }       
        myInteractMg = interactAbilityMod.InteractAbilityMg;
        targetInteractMg = myInteractMg.GetTargetInteractableMg();
        targetIsFarX = myInteractMg.GetIsTargetFarX(interactDistance);
        targetIsFarY = myInteractMg.GetIsTargetFarY(interactDistance);
        BasicMod targetBasicSM = targetInteractMg.GetTargetBasicMod();
        //interactAbilityMod.TextLogMg.PopUpTargetIcon(targetBasicSM.BaseDataMg.GetIconSprite(), bubbleTime);
        waitTimeNow = waitTime;
        exitToIdle = false;
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
        if (waitTimeNow >= 0)
        {
            waitTimeNow -= Time.deltaTime;
        }
        else
        {
            if (targetIsFarX == false && targetIsFarY == true)
            {
                stateMachine.ChangeState(interactAbilityMod.StateInteractFollowY);
            }
            else
            {
                stateMachine.ChangeState(interactAbilityMod.StateInteractFollowX);
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
