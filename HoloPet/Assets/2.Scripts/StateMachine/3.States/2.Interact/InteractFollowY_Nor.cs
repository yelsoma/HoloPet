using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractFollowY_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractAbilitySM interactAbilitySM;
    [SerializeField] private float interactDistance;
    private InteractAbilityManager myInteractMg;
    private InteractableManager targetInteractMg;
    private bool targetIsRight;
    private bool targetIsFarX;
    private bool targetIsFarY;

    // jump
    [SerializeField] private float jumpUpPower;
    [SerializeField] private float jumpUpDecrese;
    private float jumpUpPowerNow;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    private float fallSpeedNow;
    private bool keepJump;

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
        myInteractMg = interactAbilitySM.InteractAbilityMg;
        targetInteractMg = myInteractMg.GetTargetIInteractable();
        keepJump = true;
        jumpUpPowerNow = jumpUpPower;
        fallSpeedNow = 0f;
    }
    public override void StateUpdate()
    {
        // if target is no longer Interactable  , exit to idle
        if (!targetInteractMg.GetIsInteractable())
        {
            keepJump = false;
        }
        targetIsRight = myInteractMg.GetIsTargetRight();
        targetIsFarX = myInteractMg.GetIsTargetFarX(interactDistance);
        targetIsFarY = myInteractMg.GetIsTargetFarY(interactDistance);
        if (targetIsFarX)
        {
            keepJump = false;
        }
        else
        {
            if (targetIsFarY)
            {
                keepJump = true;
                if (targetIsRight)
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
                GoToChoosenInteract();
            }
        }
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
                if (keepJump)
                {
                    jumpUpPowerNow = jumpUpPower;
                    fallSpeedNow = 0f;
                }
                else
                {
                    stateMachine.ChangeState(interactAbilitySM.StateInteractFollowX);
                }
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

    private void GoToChoosenInteract()
    {
        targetInteractMg.SetInteracter(myInteractMg);
        targetInteractMg.GoToChoosenInteracedState();
        stateMachine.ChangeState(myInteractMg.GetBothInteractOption().GetInteracterOption().GetOptionState);
    }
}
