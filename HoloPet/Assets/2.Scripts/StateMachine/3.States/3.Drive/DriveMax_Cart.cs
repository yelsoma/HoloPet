using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DriveMax_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IMountableSM mountableSM;
    private IAttackAbilitySM attackAbilitySM;
    private IDriveSM driveSM;
    [SerializeField] private float speedMax;
    [SerializeField] private float speedPlus;
    private float speedNow;
    [SerializeField] private float knockUpDistance;
    [SerializeField] private float knockBackPower;
    private bool isMounted;

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

        driveSM = GetComponentInParent<IDriveSM>();
        if (driveSM == null)
        {
            Debug.LogError($"{transform} ¡X no cartSM found in parent.");
        }

        attackAbilitySM = GetComponentInParent<IAttackAbilitySM>();
        if (attackAbilitySM == null)
        {
            Debug.LogError($"{transform} ¡X no attackAbilitySM found in parent.");
        }
    }

    public override void Enter()
    {
        speedNow = speedMax;  
        isMounted = mountableSM.MountableMg.GetIsMounted();
        mountableSM.MountableMg.OnChangeMounted += MountableMg_OnChangeMounted;
    }

    public override void StateUpdate()
    {
        if (isMounted)
        {
            if (speedNow < speedMax)
            {
                speedNow += speedPlus * Time.deltaTime;
            }
            else
            {
                speedNow = speedMax;
            }
        }
        else
        {   
            //breaking
            if (speedNow >= 0f)
            {
                speedNow -= speedPlus *2f* Time.deltaTime;
            }
            else
            {
                stateMachine.ChangeState(basicSM.StateIdle);
            }
        }

        if (basicSM.FaceDirectionMg.GetIsFaceRight())
        {
            basicSM.MovementMg.MoveRight(speedNow);
            if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                basicSM.FaceDirectionMg.SetFaceLeft();
            }
            if(speedNow >= speedMax)
            {
                SetAttackablesKnockRight(true);
            }           
        }
        else
        {
            basicSM.MovementMg.MoveLeft(speedNow);
            if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
            {
                basicSM.FaceDirectionMg.SetFaceRight();
            }
            if (speedNow >= speedMax)
            {
                SetAttackablesKnockRight(false);
            }
        }             
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        mountableSM.MountableMg.OnChangeMounted -= MountableMg_OnChangeMounted;
    }

    private void SetAttackablesKnockRight(bool isAttackRight)
    {
        if (basicSM.RaycastMg.TrySetRaycast(knockUpDistance, Vector2.left))
        {
            if (attackAbilitySM.AttackAbilityMg.TrySetAttackables(basicSM.RaycastMg.GetRaycastHits()))
            {
                attackAbilitySM.AttackAbilityMg.SetAttackablesKnockBackRight(isAttackRight, knockBackPower);
            }
        }
    }

    private void MountableMg_OnChangeMounted(object sender, EventArgs e)
    {
        isMounted = mountableSM.MountableMg.GetIsMounted();
    }
}
