using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AniEnum;

public class BotanAniMg : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private IBasicSM basicSM;
    private IMountingAbilitySM mountingAbilitySM;
    [Header("states")]
    [SerializeField] private RandomMoveState_Wander wander;
    [SerializeField] private InteracterState_FindInteracts findInteractTarget;
    [SerializeField] private InteracterState_HappyChat happyChat;
    [SerializeField] private InteractedStates_HappyChatted happyChatted;

    private void Awake()
    {
        basicSM = GetComponent<IBasicSM>();
        mountingAbilitySM = GetComponent<IMountingAbilitySM>();

        basicSM.StateIdle.OnEnterState += Idle_OnEnterState;
        basicSM.StateInAir.OnEnterState += InAir_OnEnterState;
        basicSM.StateGrabbed.OnEnterState += Grabbed_OnEnterState;
        basicSM.StateClicked.OnEnterState += Clicked_OnEnterState;
        wander.OnEnterState += Wander_OnEnterState;
        mountingAbilitySM.StateMounting.OnEnterState += Mounting_OnEnterState;
        mountingAbilitySM.StateMounting.OnExitState += StateMounting_OnExitState;
        happyChat.OnEnterState += HappyChat_OnEnterState;
        happyChatted.OnEnterState += HappyChatted_OnEnterState;
        findInteractTarget.OnEnterState += FindInteractTarget_OnEnterState;
    }

    private void FindInteractTarget_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceExciting.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanRun.ToString(), layer: 0);
    }

    private void HappyChatted_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceHappy.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanIdle.ToString(), layer: 0);
    }

    private void HappyChat_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceHappy.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanIdle.ToString(), layer: 0);
    }

    private void Idle_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanIdle.ToString(), layer: 0);
    }
    private void InAir_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceShock.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanFall.ToString(), layer: 0);
    }
    private void Grabbed_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceTired.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanGrab.ToString(), layer: 0);
    }
    private void Clicked_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceHit.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanFall.ToString(), layer: 0);
    }
    private void Wander_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanWalk.ToString(), layer: 0);
    }
    private void Mounting_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Botan.Face.BotanFaceCalm.ToString(), layer: 1);
        animator.Play(AniEnum.Botan.Main.BotanMount.ToString(), layer: 0);
        animator.Play(AniEnum.Basic.Hand.HalfHand.ToString(), layer: 2);
    }
    private void StateMounting_OnExitState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Basic.Hand.HaveHand.ToString(), layer: 2);
    }
}
