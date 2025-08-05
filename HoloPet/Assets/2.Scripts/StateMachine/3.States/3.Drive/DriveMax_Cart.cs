using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveMax_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IMountableSM mountableSM;
    private CartSM cartSM;
    [SerializeField] private float speedMax;
    [SerializeField] private float speedPlus;
    private float speedNow;

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

        mountableSM = GetComponentInParent<IMountableSM>();
        if (mountableSM == null)
        {
            Debug.LogError($"{transform} ¡X no mountableSM found in parent.");
        }

        cartSM = GetComponentInParent<CartSM>();
        if (cartSM == null)
        {
            Debug.LogError($"{transform} ¡X no cartSM found in parent.");
        }
    }

    public override void Enter()
    {
        speedNow = speedMax;
    }

    public override void StateUpdate()
    {
        if (!mountableSM.MountableMg.GetIsMounted())
        {
            if (speedNow >= 0f)
            {
                speedNow -= speedPlus * Time.deltaTime;
            }
            else
            {
                stateMachine.ChangeState(cartSM.StateIdle);
            }
        }

        if (basicSM.FaceDirectionMg.GetIsFaceRight())
        {
            basicSM.MovementMg.MoveRight(speedNow);
            if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                basicSM.FaceDirectionMg.SetFaceLeft();
            }
        }
        else
        {
            basicSM.MovementMg.MoveLeft(speedNow);
            if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
            {
                basicSM.FaceDirectionMg.SetFaceRight();
            }
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
