using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicForV : StateBase
{

    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IAttackableSM attackableSM;
    private bool runRight;
    [SerializeField]private float speed;
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

        attackableSM = GetComponentInParent<IAttackableSM>();
        if (attackableSM == null)
        {
            Debug.LogError($"{transform} ¡X no attackableSM found in parent.");
        }
    }

    public override void Enter()
    {
        if (attackableSM.AttackableMg.GetIsAttackerLeft())
        {
            runRight = true;
            basicSM.FaceDirectionMg.SetFaceRight();
        }
        else
        {
            runRight = false;
            basicSM.FaceDirectionMg.SetFaceLeft();
        }
    }

    public override void StateUpdate()
    {
        if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
        {
            runRight = true;
            basicSM.FaceDirectionMg.SetFaceRight();
        }
        if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
        {

            runRight = false;
            basicSM.FaceDirectionMg.SetFaceLeft();
        }
        if (runRight)
        {
            basicSM.MovementMg.MoveRight(speed);
        }
        else
        {
            basicSM.MovementMg.MoveLeft(speed);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
