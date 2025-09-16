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
    [SerializeField] private float HitDistance;
    private bool isMounted;
    [SerializeField] private int knockBackDamage;
    public CartFxTest CartFxTest;
    [SerializeField] private float panicDistance;

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
        bool isFaceRigh = basicSM.FaceDirectionMg.GetIsFaceRight();
        Vector2 hitDirection;
        if (isFaceRigh)
        {
            hitDirection = Vector2.right;
            basicSM.MovementMg.MoveRight(speedNow);
            if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                basicSM.FaceDirectionMg.SetFaceLeft();
            }
        }
        else
        {
            hitDirection = Vector2.left;
            basicSM.MovementMg.MoveLeft(speedNow);
            if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
            {
                basicSM.FaceDirectionMg.SetFaceRight();
            }
        }

        if (isMounted)
        {
            if (speedNow < speedMax)
            {
                speedNow += speedPlus * Time.deltaTime;
            }
            else
            {
                SetHitAttackableKnockBack(hitDirection);
                SetAttackablePanic(hitDirection);
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

        
        CartFxTest.DriveParticalSmokeSpeed(speedNow);
    }

    public override void StateLateUpdate()
    {       
    }

    public override void Exit()
    {
        mountableSM.MountableMg.OnChangeMounted -= MountableMg_OnChangeMounted;
    }

    private void SetHitAttackableKnockBack(Vector2 hitDirection)
    {
        if (attackAbilitySM.AttackAbilityMg.TrySetAttackableAll(hitDirection, HitDistance))
        {
            CartFxTest.HitExplode();
            attackAbilitySM.AttackAbilityMg.GetTarget().SetAttacker(attackAbilitySM.AttackAbilityMg);
            attackAbilitySM.AttackAbilityMg.GetTarget().AttackKnockBack(knockBackDamage, knockBackPower);
        }        
    }
    private void SetAttackablePanic(Vector2 hitDirection)
    {
        if (attackAbilitySM.AttackAbilityMg.TrySetClosestAttackable(stateMachine.transform.position, hitDirection, panicDistance))
        {
            CartFxTest.HitExplode();
            attackAbilitySM.AttackAbilityMg.GetTarget().SetAttacker(attackAbilitySM.AttackAbilityMg);
            attackAbilitySM.AttackAbilityMg.GetTarget().AttackPanic();
        }
    }

    private void MountableMg_OnChangeMounted(object sender, EventArgs e)
    {
        isMounted = mountableSM.MountableMg.GetIsMounted();
    }
}
