using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotanAniMg : MonoBehaviour
{

    [SerializeField] private Animator animator;
    private BasicMod basicMod;
    private MountingAbilityMod mountingAbilityMod;
    private InteractAbilityMod interactAbilityMod;
    private AttackableMod attackableMod;
    [SerializeField] private StateBase wander;
    [SerializeField] private StateBase happyChat;
    [SerializeField] private StateBase happyChatted;
    [SerializeField] private StateBase bully;
    [SerializeField] private StateBase bullied;
    private BattleMod battleMod;

    private void Awake()
    {
        basicMod = GetComponent<IBasicMod>().BasicMod;
        basicMod.StateIdle.OnEnterState += Idle_OnEnterState;
        basicMod.StateInAir.OnEnterState += InAir_OnEnterState;
        basicMod.StateGrabbed.OnEnterState += Grabbed_OnEnterState;
        basicMod.StateClicked.OnTriggerAni1 += Clicked_OnKnockUpFall;
        basicMod.StateClicked.OnEnterState += Clicked_OnEnterState;
        attackableMod = GetComponent<IAttackableMod>().AttackableMod;
        attackableMod.StateKnockBack.OnEnterState += AttackedKnockBack_OnEnterState;
        attackableMod.StateKnockBack.OnTriggerAni1 += AttackedKnockBack_OnKnockUpFall;
        attackableMod.StateHpZero.OnEnterState += StateHpZero_OnEnterState;
        wander.OnEnterState += Wander_OnEnterState;
        mountingAbilityMod = GetComponent<IMountingAbilityMod>().MountingAbilityMod;
        mountingAbilityMod.StateMounting.OnEnterState += Mounting_OnEnterState;
        //botan cart mount
        mountingAbilityMod.StateMounting.OnTriggerAni1 += StateMounting_CartIsDashing;
        mountingAbilityMod.StateMounting.OnTriggerAni2 += StateMounting_CartIsMaxSpeed;
        interactAbilityMod = GetComponent<IInteractAbilityMod>().InteractAbilityMod;
        interactAbilityMod.StateInteractFollowX.OnEnterState += FollowInteractX_OnEnterState;
        interactAbilityMod.StateInteractFollowY.OnEnterState += InteractFollowY_OnEnterState;
        interactAbilityMod.StateInteractFailed.OnEnterState += StateInteractFailed_OnEnterState;
        happyChat.OnEnterState += HappyChat_OnEnterState;
        happyChatted.OnEnterState += HappyChatted_OnEnterState;
        happyChatted.OnTriggerAni1 += HappyChatted_OnHappyJump;
        bully.OnEnterState += Bully_OnEnterState;
        bullied.OnEnterState += Bullied_OnEnterState;
        bullied.OnTriggerAni1 += Bullied_OnHit;
        bullied.OnTriggerAni2 += Bullied_OnPanic;

        battleMod = GetComponent<IBattleMod>().BattleMod;
        battleMod.BattleSearch.OnEnterState += BattleSearch_OnEnterState;
        battleMod.BattleSearch.OnTriggerAni1 += BattleSearch_StartSearch;
        battleMod.BattleFall.OnEnterState += BattleFall_OnEnterState;
        battleMod.BattleItemAttack.OnEnterState += BattleItemAttack_OnEnterState;
        battleMod.BattleItemAttack.OnTriggerAni1 += BattleItemAttack_FaceBackToNormal;
        battleMod.BattleBasicAttack.OnEnterState += BattleBasicAttack_OnEnterState;
        battleMod.BattleBasicAttack.OnTriggerAni1 += BattleBasicAttack_FaceBackToNormal;
        battleMod.BattleWin.OnEnterState += BattleWin_OnEnterState;
    }  

    private void StateInteractFailed_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
    }

    private void StateMounting_CartIsMaxSpeed(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceExciting.ToString(), layer: 1);
    }

    private void StateMounting_CartIsDashing(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceRoar.ToString(), layer: 1);
    }

    private void StateHpZero_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Fall.ToString(), layer: 0);
        animator.Play(AniEnum.Humanoid.Face.FaceTired.ToString(), layer: 1);
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
        animator.Play(AniEnum.Humanoid.Face.FaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Mount.ToString(), layer: 0);
    }

    //battle  
    private void BattleFall_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceShock.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Fall.ToString(), layer: 0);
    }   
    private void BattleSearch_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceNormal.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
    }
    private void BattleSearch_StartSearch(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceExciting.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Run.ToString(), layer: 0);
    }
    private void BattleBasicAttack_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceRoar.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Punch.ToString(), layer: 0);
    }  
    private void BattleItemAttack_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceRoar.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
    }
    private void BattleBasicAttack_FaceBackToNormal(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
        animator.Play(AniEnum.Humanoid.Face.FaceNormal.ToString(), layer: 1);
    }
    private void BattleItemAttack_FaceBackToNormal(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
        animator.Play(AniEnum.Humanoid.Face.FaceNormal.ToString(), layer: 1);
    }
    private void BattleWin_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceHappy.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Jump.ToString(), layer: 0);
    }
}

