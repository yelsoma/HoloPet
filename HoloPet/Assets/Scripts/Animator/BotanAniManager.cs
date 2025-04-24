using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotanAniManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private HoloMemStateMachine stateMachine;

    [Header("states")]
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
        stateMounting.OnExitMounting += StateMounting_OnExitMounting;
        stateMachine.stateFollowTarget.OnEnterState += StateFollowTarget_OnEnterState;
        stateMachine.stateHappyChat.OnEnterState += StateHappyChat_OnEnterState;
    }

    private void StateMounting_OnExitMounting(object sender, System.EventArgs e)
    {
        animator.Play("BotanHaveHand", layer: 2);
    }

    private void StateFollowTarget_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanFaceNormal", layer :1) ;
        animator.Play("BotanRun" ,layer: 0);       
    }

    private void StateHappyChat_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanFaceHappy", layer: 1);      
    }

    private void StateMounting_OnCartJump(object sender, System.EventArgs e)
    {
        animator.Play("BotanFaceShock", layer: 1);
    }

    private void StateMounting_OnCartNormal(object sender, System.EventArgs e)
    {
        animator.Play("BotanFaceNone", layer: 1);
        animator.Play("BotanMount", layer: 0);
        animator.Play("BotanNoHand", layer: 2);
    }

    private void StateMounting_OnCartDashMaxSpeed(object sender, System.EventArgs e)
    {
        animator.Play("BotanFaceShock", layer: 1);
    }

    private void StateMounting_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanFaceNone", layer: 1);
        animator.Play("BotanMount", layer: 0);
        animator.Play("BotanHalfHand", layer: 2);
    }

    private void StateWander_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanFaceNormal", layer: 1);
        animator.Play("BotanWalk", layer: 0);
    }

    private void StateKnockUp_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanFaceHit", layer: 1);
        animator.Play("BotanFall", layer: 0);
    }

    private void StateGrab_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanFaceTired", layer: 1);
        animator.Play("BotanGrab", layer: 0);
    }

    private void StateFall_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanFaceShock", layer: 1);
        animator.Play("BotanFall", layer: 0);
    }

    private void StateIdle_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanFaceNormal", layer: 1);
        animator.Play("BotanIdle", layer: 0);
    }
}
