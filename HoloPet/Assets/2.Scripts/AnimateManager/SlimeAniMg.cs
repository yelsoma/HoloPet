using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAniMg : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private IBasicSM basicSM;
    [SerializeField] private StateBase findAttackable;

    private void Awake()
    {
        findAttackable.OnEnterState += FindAttackable_OnEnterState;
        findAttackable.OnTriggerAni1 += FindAttackable_OnTriggerAni1;
        findAttackable.OnTriggerAni2 += FindAttackable_OnTriggerAni2;
    }

    private void FindAttackable_OnTriggerAni2(object sender, System.EventArgs e)
    {
        animator.Play("Move");
    }

    private void FindAttackable_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("Move");
    }

    private void FindAttackable_OnTriggerAni1(object sender, System.EventArgs e)
    {
        animator.Play("Attack");
    }
}
