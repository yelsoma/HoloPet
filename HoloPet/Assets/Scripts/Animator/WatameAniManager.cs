using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatameAniManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private HoloMemStateMachine stateMachine;

    [Header("states")]
    [SerializeField] private HoloMemState_Mounting stateMounting;
    [SerializeField] private HoloMemState_KnockUp stateKnockUp;
    [SerializeField] private HoloMemState_HappyChatInteracted stateHappyChatInteracted;
    [SerializeField] private HoloMemState_Bullied stateBullied;

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
        stateHappyChatInteracted.OnStartJump += StateInteracted_OnStartJump;
        stateMachine.stateHappyChatInteracted.OnEnterState += StateHappyChatInteracted_OnEnterState;
        stateBullied.OnHit += StateBullied_OnHit;
        stateBullied.OnFall += StateBullied_OnFall;
        stateBullied.OnPanic += StateBullied_OnPanic;
        stateMachine.stateBully.OnEnterState += StateBully_OnEnterState;
    }

    private void StateBully_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceRoar.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatamePunch.ToString(), layer: 0);
    }

    private void StateBullied_OnPanic(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceShock.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameRun.ToString(), layer: 0);
    }

    private void StateBullied_OnFall(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceShock.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameFall.ToString(), layer: 0);
    }

    private void StateBullied_OnHit(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceHit.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameFall.ToString(), layer: 0);
    }

    private void StateHappyChatInteracted_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceCalm.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameIdle.ToString(), layer: 0);
    }

    private void StateInteracted_OnStartJump(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceHappy.ToString(), layer: 1);
    }

    private void StateKnockUp_OnKnockUpFall(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceShock.ToString(), layer: 1);
    }

    private void StateMounting_OnExitMounting(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Basic.Hand.HaveHand.ToString(), layer: 2);
    }

    private void StateFollowTarget_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceNormal.ToString(), layer :1) ;
        animator.Play(AniEnum.Watame.Main.WatameRun.ToString() ,layer: 0);       
    }

    private void StateHappyChat_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Main.WatameIdle.ToString(), layer: 0);
        animator.Play(AniEnum.Watame.Face.WatameFaceHappy.ToString(), layer: 1);      
    }

    private void StateMounting_OnCartJump(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceShock.ToString(), layer: 1);
    }

    private void StateMounting_OnCartNormal(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceCalm.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameMount.ToString(), layer: 0);
        animator.Play(AniEnum.Basic.Hand.HalfHand.ToString(), layer: 2);
    }

    private void StateMounting_OnCartDashMaxSpeed(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceExciting.ToString(), layer: 1);
    }

    private void StateMounting_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceCalm.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameMount.ToString(), layer: 0);
        animator.Play(AniEnum.Basic.Hand.HalfHand.ToString(), layer: 2);
    }

    private void StateWander_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameWalk.ToString(), layer: 0);
    }

    private void StateKnockUp_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceHit.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameFall.ToString(), layer: 0);
    }

    private void StateGrab_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceTired.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameGrab.ToString(), layer: 0);
    }

    private void StateFall_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceShock.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameFall.ToString(), layer: 0);
    }

    private void StateIdle_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameIdle.ToString(), layer: 0);
    }
}
