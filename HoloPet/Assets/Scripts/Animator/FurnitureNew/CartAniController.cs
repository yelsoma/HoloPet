using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAniController : MonoBehaviour
{
    [Header("Need Refernce")]
    [SerializeField] private Animator animator;
    [SerializeField] private CartStateMachine stateMachine;
    [Header("Need Refernce")]
    [SerializeField] private CartState_Idle stateIdle;
    [SerializeField] private CartState_Fall stateFall;
    [SerializeField] private CartState_Grab stateGrab;
    [SerializeField] private CartState_KnockUp stateKnockUp;
    [SerializeField] private CartState_Dash stateDash;
    [SerializeField] private CartState_Mounted stateMounted;
    [SerializeField] private CartState_DashMaxSpeed stateDashMaxSpeed;
    [SerializeField] private CartState_Spawn stateSpawn;
    private void Awake()
    {
        stateIdle.OnEnterState += StateIdle_OnEnterState;
        stateFall.OnEnterState += StateFall_OnEnterState;
        stateGrab.OnEnterState += StateGrab_OnEnterState;
        stateKnockUp.OnEnterState += StateKnockUp_OnEnterState;
        stateDash.OnEnterState += StateDash_OnEnterState;
        stateDashMaxSpeed.OnEnterState += StateDashMaxSpeed_OnEnterState;
        stateMounted.OnEnterState += StateMounted_OnEnterState;
    }

    private void StateDashMaxSpeed_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("CartDashMaxSpeed");
    }

    private void StateMounted_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("CartMounted");
    }
    private void StateDash_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("CartDash");
    }

    private void StateKnockUp_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("CartIdle", 0, 0);
    }

    private void StateGrab_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("CartIdle");
    }

    private void StateFall_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("CartIdle");
    }

    private void StateIdle_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("CartIdle");
    }
}
