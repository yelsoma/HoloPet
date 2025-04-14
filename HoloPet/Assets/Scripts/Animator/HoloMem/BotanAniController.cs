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
    [SerializeField] private HoloMemState_FindInteractTarget stateFindInteractTarget;
    [SerializeField] private HoloMemState_ChooseRandom stateChooseRandom;
    [SerializeField] private HoloMemState_Interact stateInteract;
    [SerializeField] private HoloMemState_Spawn stateSpawn;
    private void Awake()
    {
        stateIdle.OnEnterState += StateIdle_OnEnterState;
        stateFall.OnEnterState += StateFall_OnEnterState;
        stateGrab.OnEnterState += StateGrab_OnEnterState;
        stateKnockUp.OnEnterState += StateKnockUp_OnEnterState;
        stateWander.OnEnterState += StateWander_OnEnterState;
        stateFindInteractTarget.OnEnterState += StateFindInteractTarget_OnEnterState;
        stateInteract.OnEnterState += StateInteract_OnEnterState;
        stateMounting.OnEnterState += StateMounting_OnEnterState;
    }

    private void StateMounting_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanMounting");
    }

    private void StateInteract_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanHappyJump");
    }

    private void StateFindInteractTarget_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanFindTarget");
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
