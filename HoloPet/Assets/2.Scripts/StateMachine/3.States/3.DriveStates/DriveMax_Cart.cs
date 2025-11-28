using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DriveMax_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountableMod mountableMod;
    private AttackAbilityMod attackAbilityMod;
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

        IAttackAbilityMod iAttackAbilityMod = GetComponentInParent<IAttackAbilityMod>();
        if (iAttackAbilityMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no attackAbilityMod found in parent.");
        }
        else
        {
            attackAbilityMod = iAttackAbilityMod.AttackAbilityMod;
        }
    }

    public override void Enter()
    {
        speedNow = speedMax;  
        isMounted = mountableMod.MountableMg.GetIsMounted();
        mountableMod.MountableMg.OnChangeMounted += MountableMg_OnChangeMounted;
    }

    public override void StateUpdate()
    {
        bool isFaceRigh = basicMod.FaceDirectionMg.GetIsFaceRight();
        Vector2 hitDirection;
        if (isFaceRigh)
        {
            hitDirection = Vector2.right;
            basicMod.PhysicsMg.MoveRight(speedNow);
            if (basicMod.BoundaryMg.CheckIsRightBounderyAndResetPos())
            {
                basicMod.FaceDirectionMg.SetFaceLeft();
            }
        }
        else
        {
            hitDirection = Vector2.left;
            basicMod.PhysicsMg.MoveLeft(speedNow);
            if (basicMod.BoundaryMg.CheckIsLeftBounderyAndResetPos())
            {
                basicMod.FaceDirectionMg.SetFaceRight();
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
                stateMachine.ChangeState(basicMod.StateIdle);
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

    private void SetHitAttackableKnockBack(Vector2 hitDirection)
    {
        bool isKnockRight;
        if (hitDirection == Vector2.right)
        {
            isKnockRight = true;
        }
        else
        {
            isKnockRight = false;
        }

        if (attackAbilityMod.AttackAbilityMg.TrySetAttackableAll(hitDirection, HitDistance))
        {
            CartFxTest.HitExplode();
            if (attackAbilityMod.AttackAbilityMg.GetTarget().GetIsKnockable())
            {
                attackAbilityMod.AttackAbilityMg.GetTarget().SetAttackKnockBack(knockBackPower,isKnockRight);
            }            
        }        
    }
    private void SetAttackablePanic(Vector2 hitDirection)
    {
        //if (attackAbilitySM.AttackAbilityMg.TrySetClosestAttackable(stateMachine.transform.position, hitDirection, panicDistance))
        //{
        //    CartFxTest.HitExplode();
        //    attackAbilitySM.AttackAbilityMg.GetTarget().SetAttacker(attackAbilitySM.AttackAbilityMg);
        //    attackAbilitySM.AttackAbilityMg.GetTarget().AttackPanic();
        //}
    }

    private void MountableMg_OnChangeMounted(object sender, EventArgs e)
    {
        isMounted = mountableMod.MountableMg.GetIsMounted();
    }
}
