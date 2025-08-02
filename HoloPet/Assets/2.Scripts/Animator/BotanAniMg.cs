using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AniEnum;

public class BotanAniMg : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private IBasicSM basicSM;
    private IMountingAbilitySM mountingAbilitySM;
    private IInteractAbilitySM interactAbilitySM;
    [Header("states")]
    [SerializeField] private Wander_Nor wander;
    [SerializeField] private HappyChat_Nor happyChat;
    [SerializeField] private HappyChatted_Nor happyChatted;
    [SerializeField] private Bully_Nor bully;
    [SerializeField] private Bullied_Nor bullied;

    private void Awake()
    {
        basicSM = GetComponent<IBasicSM>();
        mountingAbilitySM = GetComponent<IMountingAbilitySM>();
        interactAbilitySM = GetComponent<IInteractAbilitySM>();
        basicSM.StateIdle.OnEnterState += Idle_OnEnterState;
        basicSM.StateInAir.OnEnterState += InAir_OnEnterState;
        basicSM.StateGrabbed.OnEnterState += Grabbed_OnEnterState;
        basicSM.StateClicked.OnEnterState += Clicked_OnEnterState;
        wander.OnEnterState += Wander_OnEnterState;
        mountingAbilitySM.StateMounting.OnEnterState += Mounting_OnEnterState;
        mountingAbilitySM.StateMounting.OnExitState += Mounting_OnExitState;
        happyChat.OnEnterState += HappyChat_OnEnterState;
        happyChatted.OnEnterState += HappyChatted_OnEnterState;
        interactAbilitySM.StateInteractFollowX.OnEnterState += FollowInteractX_OnEnterState;
        interactAbilitySM.StateInteractFollowY.OnEnterState += InteractFollowY_OnEnterState;
        bully.OnEnterState += Bully_OnEnterState;
        bullied.OnEnterState += Bullied_OnEnterState;
        bullied.OnHit += Bullied_OnHit;
        bullied.OnPanic += Bullied_OnPanic;
    }

    private void Bullied_OnPanic(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceShock.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Run.ToString(), layer: 0);
    }

    private void Bullied_OnHit(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceShock.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Fall.ToString(), layer: 0);
    }

    private void Bullied_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
    }

    private void Bully_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceRoar.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Punch.ToString(), layer: 0);
    }

    private void FollowInteractX_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceExciting.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Run.ToString(), layer: 0);
    }

    private void InteractFollowY_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceExciting.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
    }

    private void HappyChatted_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceHappy.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
    }

    private void HappyChat_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceHappy.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
    }

    private void Idle_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
    }
    private void InAir_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceShock.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Fall.ToString(), layer: 0);
    }
    private void Grabbed_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceTired.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Grab.ToString(), layer: 0);
    }
    private void Clicked_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceHit.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Fall.ToString(), layer: 0);
    }
    private void Wander_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Walk.ToString(), layer: 0);
    }
    private void Mounting_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceCalm.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Mount.ToString(), layer: 0);
        animator.Play(AniEnum.Humanoid.Hand.HalfHand.ToString(), layer: 2);
    }
    private void Mounting_OnExitState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Hand.HaveHand.ToString(), layer: 2);
    }
}

