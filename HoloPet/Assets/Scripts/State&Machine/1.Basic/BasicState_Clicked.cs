using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicState_Clicked : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;

    public event EventHandler OnKnockUpFall;

    private float knockUpPower;
    private float knockUpPowerNow;
    private float knockUpFaceDir;
    private float knockBackPower;
    private float fallSpeedNow;

    [SerializeField] private float knockUpDecrese;
    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;

    private bool knockUpRight;

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
    }

    public override void Enter()
    {
        knockUpPower = UnityEngine.Random.Range(6f, 7f);
        knockBackPower = UnityEngine.Random.Range(0.7f, 2f);
        knockUpFaceDir = UnityEngine.Random.Range(0f, 1f);

        fallSpeedNow = 0f;
        knockUpPowerNow = knockUpPower;

        if (knockUpFaceDir <= 0.5f)
        {
            knockUpRight = false;
            basicSM.FaceDirection.SetFaceRight();
        }
        else
        {
            knockUpRight = true;
            basicSM.FaceDirection.SetFaceLeft();
        }
    }

    public override void StateUpdate()
    {
        if (knockUpPowerNow >= 0f)
        {
            knockUpPowerNow -= knockUpDecrese * Time.deltaTime;

            if (basicSM.BoundaryManager.CheckIsTopBounderyAndResetPos())
            {
                knockUpPowerNow = -1f;
            }

            basicSM.Movement.MoveUp(knockUpPowerNow);
        }
        else
        {
            OnKnockUpFall?.Invoke(this, EventArgs.Empty);

            if (fallSpeedNow <= fallSpeedMax)
            {
                fallSpeedNow += fallSpeedIncreese * Time.deltaTime;
            }

            basicSM.Movement.MoveDown(fallSpeedNow);

            if (basicSM.BoundaryManager.CheckIsBotBounderyAndResetPos())
            {
                stateMachine.ChangeState(basicSM.StateIdle);
            }
        }

        if (knockUpRight)
        {
            if (basicSM.BoundaryManager.CheckIsRightBounderyAndResetPos())
            {
                knockUpRight = false;
            }

            basicSM.Movement.MoveRight(knockBackPower);
        }
        else
        {
            if (basicSM.BoundaryManager.CheckIsLeftBounderyAndResetPos())
            {
                knockUpRight = true;
            }

            basicSM.Movement.MoveLeft(knockBackPower);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
