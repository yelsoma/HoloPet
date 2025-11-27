using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractFailed_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private IInteractAbilitySM interactAbilitySM;
    private ICreatureSM creatureSM;
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

        interactAbilitySM = GetComponentInParent<IInteractAbilitySM>();
        if (interactAbilitySM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IInteractAbilitySM found in parent.");
        }

        creatureSM = GetComponentInParent<ICreatureSM>();
        if (creatureSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no creatureSM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        sadTimeNow = failTime;
        interactAbilitySM.TextLogMg.PopUpSadEmoji(sadBubbleTime);
        if (interactAbilitySM.InteractAbilityMg.GetIsTargetRight())
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
            stateMachine.ChangeState(creatureSM.StateWander);
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
