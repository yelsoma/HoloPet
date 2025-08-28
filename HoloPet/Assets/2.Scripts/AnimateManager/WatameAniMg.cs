using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatameAniMg : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private IBasicSM basicSM;
    private IMountingAbilitySM mountingAbilitySM;
    private IInteractAbilitySM interactAbilitySM;
    private IAttackableSM attackableSM;
    private ICreatureSM creatureSM;
    [SerializeField] private StateBase happyChat;
    [SerializeField] private StateBase happyChatted;
    [SerializeField] private StateBase bully;
    [SerializeField] private StateBase bullied;

    [SerializeField] private StateBase vidioWalk;
    [SerializeField] private StateBase vidioHit;
    [SerializeField] private StateBase vidioPanic;

    private void Awake()
    {
        basicSM = GetComponent<IBasicSM>();
        basicSM.StateIdle.OnEnterState += Idle_OnEnterState;
        basicSM.StateInAir.OnEnterState += InAir_OnEnterState;
        basicSM.StateGrabbed.OnEnterState += Grabbed_OnEnterState;
        basicSM.StateClicked.OnTriggerAni1 += Clicked_OnKnockUpFall;
        basicSM.StateClicked.OnEnterState += Clicked_OnEnterState;
        attackableSM = GetComponent<IAttackableSM>();
        attackableSM.StateKnockBack.OnEnterState += AttackedKnockBack_OnEnterState;
        attackableSM.StateKnockBack.OnTriggerAni1 += AttackedKnockBack_OnKnockUpFall;
        attackableSM.StateHpZero.OnEnterState += StateHpZero_OnEnterState;
        attackableSM.StateHpZero.OnExitState += StateHpZero_OnExitState;
        creatureSM = GetComponent<ICreatureSM>();
        creatureSM.StateWander.OnEnterState += Wander_OnEnterState;
        mountingAbilitySM = GetComponent<IMountingAbilitySM>();
        mountingAbilitySM.StateMounting.OnEnterState += Mounting_OnEnterState;
        mountingAbilitySM.StateMounting.OnExitState += Mounting_OnExitState;
        interactAbilitySM = GetComponent<IInteractAbilitySM>();
        interactAbilitySM.StateInteractFollowX.OnEnterState += FollowInteractX_OnEnterState;
        interactAbilitySM.StateInteractFollowY.OnEnterState += InteractFollowY_OnEnterState;
        interactAbilitySM.StateInteractFailed.OnEnterState += StateInteractFailed_OnEnterState;
        happyChat.OnEnterState += HappyChat_OnEnterState;
        happyChatted.OnEnterState += HappyChatted_OnEnterState;
        happyChatted.OnTriggerAni1 += HappyChatted_OnHappyJump;
        bully.OnEnterState += Bully_OnEnterState;
        bullied.OnEnterState += Bullied_OnEnterState;
        bullied.OnTriggerAni1 += Bullied_OnHit;
        bullied.OnTriggerAni2 += Bullied_OnPanic;
    }

    private void StateInteractFailed_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
    }

    private void StateHpZero_OnExitState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Hand.HaveHand.ToString(), layer: 2);
    }

    private void StateHpZero_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Dead.ToString(), layer: 0);
        animator.Play(AniEnum.Humanoid.Face.FaceDead.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Hand.NoHand.ToString(), layer: 2);
        animator.Play(AniEnum.Humanoid.Fx.DeadFlash.ToString(), layer: 3);
    }

    private void AttackedKnockBack_OnKnockUpFall(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceShock.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Fall.ToString(), layer: 0);
    }

    private void AttackedKnockBack_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceHit.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Fall.ToString(), layer: 0);
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
        animator.Play(AniEnum.Humanoid.Face.FaceCalm.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
    }

    private void HappyChat_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceHappy.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
    }

    private void HappyChatted_OnHappyJump(object sender, System.EventArgs e)
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
    private void Clicked_OnKnockUpFall(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceShock.ToString(), layer: 1);
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
