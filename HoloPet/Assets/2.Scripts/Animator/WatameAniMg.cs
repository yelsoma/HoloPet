using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatameAniMg : MonoBehaviour
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
        mountingAbilitySM.StateMounting.OnExitState += Mounting_OnExitState;
        happyChat.OnEnterState += HappyChat_OnEnterState;
        happyChatted.OnEnterState += HappyChatted_OnEnterState;
        findInteractTarget.OnEnterState += FindInteractTarget_OnEnterState;
    }

    private void FindInteractTarget_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceExciting.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameRun.ToString(), layer: 0);
    }

    private void HappyChatted_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceHappy.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameIdle.ToString(), layer: 0);
    }

    private void HappyChat_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceHappy.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameIdle.ToString(), layer: 0);
    }

    private void Idle_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameIdle.ToString(), layer: 0);
    }
    private void InAir_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceShock.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameFall.ToString(), layer: 0);
    }
    private void Grabbed_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceTired.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameGrab.ToString(), layer: 0);
    }
    private void Clicked_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceHit.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameFall.ToString(), layer: 0);
    }
    private void Wander_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameWalk.ToString(), layer: 0);
    }
    private void Mounting_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Watame.Face.WatameFaceCalm.ToString(), layer: 1);
        animator.Play(AniEnum.Watame.Main.WatameMount.ToString(), layer: 0);
        animator.Play(AniEnum.Basic.Hand.HalfHand.ToString(), layer: 2);
    }
    private void Mounting_OnExitState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Basic.Hand.HaveHand.ToString(), layer: 2);
    }
}
