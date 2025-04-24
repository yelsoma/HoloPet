using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotanAniController : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private HoloMemStateMachine stateMachine;

    [SerializeField] private HoloMemState_Mounting stateMounting;
    private void Awake()
    {
        stateMachine.stateIdle.OnEnterState += StateIdle_OnEnterState;
        stateMachine.stateFall.OnEnterState += StateFall_OnEnterState;
        stateMachine.stateGrab.OnEnterState += StateGrab_OnEnterState;
        stateMachine.stateKnockUp.OnEnterState += StateKnockUp_OnEnterState;
        stateMachine.stateWander.OnEnterState += StateWander_OnEnterState;
        stateMachine.stateMounting.OnEnterState += StateMounting_OnEnterState;
        stateMounting.OnCartDashMaxSpeed += StateMounting_OnCartDashMaxSpeed;
        stateMounting.OnCartNormal += StateMounting_OnCartNormal;
        stateMounting.OnCartJump += StateMounting_OnCartJump;
        stateMachine.stateFollowTarget.OnEnterState += StateFollowTarget_OnEnterState;
        stateMachine.stateHappyChat.OnEnterState += StateHappyChat_OnEnterState;
    }

    private void StateFollowTarget_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanFindTarget");
    }

    private void StateHappyChat_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanHappyJump");
    }

    private void StateMounting_OnCartJump(object sender, System.EventArgs e)
    {
        animator.Play("BotanFallNew");
    }

    private void StateMounting_OnCartNormal(object sender, System.EventArgs e)
    {
        animator.Play("BotanMounting");
    }

    private void StateMounting_OnCartDashMaxSpeed(object sender, System.EventArgs e)
    {
        animator.Play("BotanCartDashMaxSpeed");
    }

    private void StateMounting_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanMounting");
    }

    private void StateWander_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanWanderNew");
    }

    private void StateKnockUp_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanKnockUpNew" ,0,0);
    }

    private void StateGrab_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanGrabNew");
    }

    private void StateFall_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanFallNew");
    }

    private void StateIdle_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanIdleNew");
    }
}
