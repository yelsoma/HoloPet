using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FindAttackable_Enemy : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private IAttackAbilitySM attackAbilitySM;
    [SerializeField] private float searchDistance;
    LayerMask playerLayerMask;
    [SerializeField] private float AttackDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackPrepare;
    private float attackPrepareNow;
    [SerializeField] private float attackAfter;
    private float attackAfterNow;

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

        attackAbilitySM = GetComponentInParent<IAttackAbilitySM>();
        if(attackAbilitySM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IAttackAbilitySM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        playerLayerMask = LayerMask.GetMask("Player");
        attackPrepareNow = attackPrepare;
        attackAfterNow = attackAfter;
        attackAfterNow = 0;
    }
    public override void StateUpdate()
    {
        if (attackAbilitySM.AttackAbilityMg.TrySetClosestAttackableHorizontal(searchDistance, playerLayerMask))
        {
            if(attackAbilitySM.AttackAbilityMg.GetTargetDistance() <= AttackDistance)
            {
                if(attackAfterNow <= 0)
                {
                    TriggerAni1();
                    if (attackPrepareNow >= 0)
                    {
                        attackPrepareNow -= Time.deltaTime;
                    }
                    else
                    {
                        //attackAbilitySM.AttackAbilityMg.GetTarget().SetAttacker(attackAbilitySM.AttackAbilityMg);
                        //attackAbilitySM.AttackAbilityMg.GetTarget().AttackKnockBack(0, 1);
                        attackPrepareNow = attackPrepare;
                        attackAfterNow = attackAfter;
                    }
                }   
                attackAfterNow -= Time.deltaTime;
            }
            else
            {
                if (attackAfterNow >= 0)
                {
                    attackAfterNow -= Time.deltaTime;
                }
                else
                {
                    TriggerAni2();
                    if (attackAbilitySM.AttackAbilityMg.GetIsTargetRight())
                    {
                        basicMod.FaceDirectionMg.SetFaceRight();
                        basicMod.PhysicsMg.MoveRight(moveSpeed);
                    }
                    else
                    {
                        basicMod.FaceDirectionMg.SetFaceLeft();
                        basicMod.PhysicsMg.MoveLeft(moveSpeed);
                    }
                }              
            }
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
