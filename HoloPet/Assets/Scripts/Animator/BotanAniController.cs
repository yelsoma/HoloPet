using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotanAniController : MonoBehaviour
{
    [Header("Need Refernce")]
    [SerializeField] private Animator animator;
    [SerializeField] private HoloMemStateMachine stateMachine;
    [Header("Need Refernce")]
    [SerializeField] private HoloMemState_Idle stateIdle;
    [SerializeField] private HoloMemState_Fall stateFall;
    [SerializeField] private HoloMemState_Grab stateGrab;
    [SerializeField] private HoloMemState_KnockUp stateKnockUp;
    [SerializeField] private HoloMemState_Wander stateWander;
    [SerializeField] private HoloMemState_Mounting stateMounting;
    [SerializeField] private HoloMemState_FollowTarget stateFollowTarget;
    [SerializeField] private HoloMemState_ChooseRandom stateChooseRandom;
    [SerializeField] private HoloMemState_Spawn stateSpawn;
    [SerializeField] private HoloMemState_HappyChat stateHappyChat;
    private void Awake()
    {
        stateIdle.OnEnterState += StateIdle_OnEnterState;
        stateFall.OnEnterState += StateFall_OnEnterState;
        stateGrab.OnEnterState += StateGrab_OnEnterState;
        stateKnockUp.OnEnterState += StateKnockUp_OnEnterState;
        stateWander.OnEnterState += StateWander_OnEnterState;
        stateMounting.OnEnterState += StateMounting_OnEnterState;
        stateMounting.OnCartDashMaxSpeed += StateMounting_OnCartDashMaxSpeed;
        stateMounting.OnCartNormal += StateMounting_OnCartNormal;
        stateMounting.OnCartJump += StateMounting_OnCartJump;
        stateFollowTarget.OnEnterState += StateFollowTarget_OnEnterState;
        stateHappyChat.OnEnterState += StateHappyChat_OnEnterState;
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
