using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpZero_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;

    private float knockUpPower;
    private float knockBackPower;
    private bool moveRight;

    [Header("Spin Settings")]
    private float spinSpeed = 180f; // degrees per second
    [SerializeField] private GameObject spinPoint;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");

        IBasicMod iBasicMod = stateMachine.transform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            basicMod = iBasicMod.BasicMod;
    }

    public override void Enter()
    {
        basicMod.ClickableMg.SetIsClickable(false);
        IInteractableMod iInteractableMod = stateMachine.GetComponent<IInteractableMod>();
        if (iInteractableMod != null)
        {
            iInteractableMod.InteractableMod.InteractableMg.SetIsInteractable(false);
        }
        IAttackableMod attackableMod = stateMachine.GetComponent<IAttackableMod>();
        if (attackableMod != null)
        {
            attackableMod.AttackableMod.AttackableMg.SetIsAttackable(false);
        }
        IMountableMod mountableMod = stateMachine.GetComponent<IMountableMod>();
        if (mountableMod != null)
        {
            mountableMod.MountableMod.MountableMg.SetIsMountableState(false);
        }
        IItemHolderMod itemHolderMod = stateMachine.GetComponent<IItemHolderMod>();
        if (itemHolderMod != null)
        {
            ItemHolderManager itemHolderMg = itemHolderMod.ItemHolderMod.ItemHolderMg;
            itemHolderMg.SetIsCanHoldState(false);
            if (itemHolderMg.GetIsHolding())
            {
                ItemManager itemHoldMg = itemHolderMg.GetItem();
                itemHoldMg.ExitHold();
                itemHoldMg.GetStateMachine().ChangeState(itemHoldMg.GetStateMachine().GetComponent<IBasicMod>().BasicMod.StateClicked);
            }
        }

        knockUpPower = UnityEngine.Random.Range(4.5f, 5.5f);
        knockBackPower = UnityEngine.Random.Range(0.5f, 1f);

        moveRight = UnityEngine.Random.value > 0.5f;

        basicMod.PhysicsMg.SetJump(knockUpPower);
        basicMod.PhysicsMg.ResetFall();

        if (moveRight)
            basicMod.FaceDirectionMg.SetFaceRight();
        else
            basicMod.FaceDirectionMg.SetFaceLeft();

        if (spinPoint == null)
        {
            spinPoint = stateMachine.gameObject;
        }
    }

    public override void StateUpdate()
    {       
        spinPoint.transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
        // vertical movement
        if (basicMod.PhysicsMg.KeepJump())
        {
            if (basicMod.BoundaryMg.CheckIsTopBounderyAndResetPos())
            {
                basicMod.PhysicsMg.SetJump(0);
            }
        }
        else
        {
            basicMod.PhysicsMg.KeepFall();

            // once it leaves the bottom of the screen, it rests forever
            if (basicMod.BoundaryMg.CheckIsOutOfBot())
            {
                stateMachine.ChangeState(basicMod.StateDestroy);
                return;
            }
        }

        // horizontal drift
        if (moveRight)
            basicMod.PhysicsMg.MoveRight(knockBackPower);
        else
            basicMod.PhysicsMg.MoveLeft(knockBackPower);
    }

    public override void Exit()
    {
    }
}
