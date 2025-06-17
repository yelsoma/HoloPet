using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAniManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CartStateMachine stateMachine;
    [SerializeField] private CartState_Grab stateGrab;
    [SerializeField] private CartState_Fall stateFall;

    private void Awake()
    {
        stateMachine.stateDash.OnEnterState += StateDash_OnEnterState;
        stateMachine.stateDashMaxSpeed.OnEnterState += StateDashMaxSpeed_OnEnterState;
        stateMachine.stateIdle.OnEnterState += StateIdle_OnEnterState;
        stateMachine.stateJump.OnEnterState += StateJump_OnEnterState;
        stateMachine.stateKnockUp.OnEnterState += StateKnockUp_OnEnterState;
        stateMachine.stateMounted.OnEnterState += StateMounted_OnEnterState;
        stateGrab.OnGrabMounted += StateGrab_OnGrabMounted;
        stateGrab.OnGrabNormal += StateGrab_OnGrabNormal;
        stateFall.OnFallMounted += StateFall_OnFallMounted;
        stateFall.OnFallNormal += StateFall_OnFallNormal;
    }

    private void StateFall_OnFallNormal(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Cart.Main.CartIdle.ToString());
    }

    private void StateFall_OnFallMounted(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Cart.Main.CartMounted.ToString());
    }

    private void StateGrab_OnGrabNormal(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Cart.Main.CartIdle.ToString());
    }

    private void StateGrab_OnGrabMounted(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Cart.Main.CartMounted.ToString());
    }

    private void StateMounted_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Cart.Main.CartMounted.ToString());
    }

    private void StateKnockUp_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Cart.Main.CartIdle.ToString());
    }

    private void StateJump_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Cart.Main.CartMounted.ToString());
    }

    private void StateIdle_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Cart.Main.CartIdle.ToString());
    }

    private void StateDashMaxSpeed_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Cart.Main.CartDashMaxSpeed.ToString());
    }

    private void StateDash_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Cart.Main.CartDash.ToString());
    }
}
