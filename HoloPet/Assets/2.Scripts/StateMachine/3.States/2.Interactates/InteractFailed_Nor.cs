using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractFailed_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private InteractAbilityMod interactAbilityMod;
    [SerializeField] private float failTime;
    private float sadTimeNow;
    [SerializeField] private float sadBubbleTime;

    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    private float fallSpeedNow;
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
        sadTimeNow = failTime;
        interactAbilityMod.TextLogMg.PopUpSadEmoji(sadBubbleTime);
        if (interactAbilityMod.InteractAbilityMg.GetIsTargetRight())
        {
            basicMod.FaceDirectionMg.SetFaceRight();
        }
        else
        {
            basicMod.FaceDirectionMg.SetFaceLeft();
        }
    }
    public override void StateUpdate()
    {
        if (!basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            basicMod.PhysicsMg.MoveDown(fallSpeedNow);
            if (fallSpeedNow <= fallSpeedMax)
            {
                fallSpeedNow += fallSpeedIncreese * Time.deltaTime;
            }
        }
        if (sadTimeNow >= 0)
        {
            sadTimeNow -= Time.deltaTime;
        }
        else
        {
            stateMachine.ChangeState(basicMod.StateIdle);
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
