using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotanAniManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private HoloMemStateMachine stateMachine;

    [Header("states")]
    [SerializeField] private HoloMemState_Mounting stateMounting;
    [SerializeField] private HoloMemState_KnockUp stateKnockUp;

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
        stateKnockUp.OnKnockUpFall += StateKnockUp_OnKnockUpFall;
        stateMachine.stateHappyChatInteracted.OnEnterState += StateHappyChatInteracted_OnEnterState;
    }

    private void StateHappyChatInteracted_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceHappy.ToString(), layer: 1);
    }

    private void StateKnockUp_OnKnockUpFall(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceShock.ToString(), layer: 1);
    }

    private void StateMounting_OnExitMounting(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Basic.Hand.HaveHand.ToString(), layer: 2);
    }

    private void StateFollowTarget_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceNormal.ToString(), layer :1) ;
        animator.Play(AniEnum.Botan.Main.BotanRun.ToString() ,layer: 0);       
    }

    private void StateHappyChat_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Main.BotanRun.ToString(), layer: 0);
        animator.Play(AniEnum.Botan.Face.BotanFaceHappy.ToString(), layer: 1);      
    }

    private void StateMounting_OnCartJump(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceShock.ToString(), layer: 1);
    }

    private void StateMounting_OnCartNormal(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceCalm.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanMount.ToString(), layer: 0);
        animator.Play(AniEnum.Basic.Hand.HalfHand.ToString(), layer: 2);
    }

    private void StateMounting_OnCartDashMaxSpeed(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceExciting.ToString(), layer: 1);
    }

    private void StateMounting_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceCalm.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanMount.ToString(), layer: 0);
        animator.Play(AniEnum.Basic.Hand.HalfHand.ToString(), layer: 2);
    }

    private void StateWander_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanWalk.ToString(), layer: 0);
    }

    private void StateKnockUp_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceHit.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanFall.ToString(), layer: 0);
    }

    private void StateGrab_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceTired.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanGrab.ToString(), layer: 0);
    }

    private void StateFall_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceShock.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanFall.ToString(), layer: 0);
    }

    private void StateIdle_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanIdle.ToString(), layer: 0);
    }
}
