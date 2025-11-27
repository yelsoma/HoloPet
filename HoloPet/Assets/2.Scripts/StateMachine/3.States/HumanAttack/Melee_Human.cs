using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Human : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private IAttackAbilitySM attackAbilitySM;
    //private IHoloMemAttackAbilitySM humanAttackSM;

    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private float wait = 0.5f;

    private float waitTimer;
    private bool waiting;

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
        if (attackAbilitySM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IAttackAbilitySM found in parent.");
        }

        //humanAttackSM = GetComponentInParent<IHoloMemAttackAbilitySM>();
        //if (humanAttackSM == null)
        //{
        //    Debug.LogError($"{transform} ¡X no humanAttackSM found in parent.");
        //}
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        Debug.Log("here");
        waiting = false;
        waitTimer = 0f;
    }

    public override void StateUpdate()
    {
        // If waiting, just count down
        if (waiting)
        {
            waitTimer -= Time.deltaTime;
            //if (waitTimer <= 0f)
            //{
            //    stateMachine.ChangeState(humanAttackSM.StateSearch);
            //}
            return; // Skip attack logic while waiting
        }

        // Normal attack logic
        if (attackAbilitySM.AttackAbilityMg.TrySetClosestAttackableHorizontal(3f, targetLayerMask))
        {
            if (attackAbilitySM.AttackAbilityMg.GetIsTargetRight())
            {
                basicMod.FaceDirectionMg.SetFaceRight();
            }
            else
            {
                basicMod.FaceDirectionMg.SetFaceLeft();
            }

            if (attackAbilitySM.AttackAbilityMg.GetTargetDistance() <= 3f)
            {
                TriggerAni1();
                //attackAbilitySM.AttackAbilityMg.GetTarget().SetAttacker(attackAbilitySM.AttackAbilityMg);
                //attackAbilitySM.AttackAbilityMg.GetTarget().AttackKnockBack(0, 1);

                StartWait();
                return;
            }
        }

        // No target or too far ¡÷ still wait before returning
        StartWait();
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        waiting = false;
        waitTimer = 0f;
    }
    #endregion

    #region Helpers
    private void StartWait()
    {
        waiting = true;
        waitTimer = wait;
    }
    #endregion
}
