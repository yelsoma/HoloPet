using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drive_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountableMod mountableMod;
    private IDriveSM driveSM;

    [SerializeField] private float speedMax;
    [SerializeField] private float speedPlus;
    private float speedNow;
    private bool isMounted;
    public CartFxTest CartFxTest;

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

        IMountableMod imountableMod = GetComponentInParent<IMountableMod>();
        if (imountableMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no mountableMod  found in parent.");
        }
        else
        {
            mountableMod = imountableMod.MountableMod;
        }

        driveSM = GetComponentInParent<IDriveSM>();
        if (driveSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no cartSM found in parent.");
        }
    }

    public override void Enter()
    {
        speedNow = 0f;
        isMounted = mountableMod.MountableMg.GetIsMounted();
        mountableMod.MountableMg.OnChangeMounted += MountableMg_OnChangeMounted;
        mountableMod.MountableMg.GetIsMountable();
    }

    public override void StateUpdate()
    {
        if (isMounted)
        {
            speedNow += speedPlus * Time.deltaTime;
        }
        else
        {
            speedNow -= speedPlus * 2f * Time.deltaTime;
        }

        if(speedNow <= 0)
        {
            stateMachine.ChangeState(basicMod.StateIdle);
        }

        if(speedNow >= speedMax)
        {
            stateMachine.ChangeState(driveSM.StateDirveMax);
        }

        if (basicMod.FaceDirectionMg.GetIsFaceRight())
        {
            basicMod.PhysicsMg.MoveRight(speedNow);
            if (basicMod.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                basicMod.FaceDirectionMg.SetFaceLeft();
            }
        }
        else
        {
            basicMod.PhysicsMg.MoveLeft(speedNow);
            if (basicMod.BoundaryMg.CheckIsLeftBounderyAndResetPos())
            {
                basicMod.FaceDirectionMg.SetFaceRight();
            }
        }
        CartFxTest.DriveParticalSmokeSpeed(speedNow);
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        mountableMod.MountableMg.OnChangeMounted -= MountableMg_OnChangeMounted;
    }

    private void MountableMg_OnChangeMounted(object sender, EventArgs e)
    {
        isMounted = mountableMod.MountableMg.GetIsMounted();
    }
}
