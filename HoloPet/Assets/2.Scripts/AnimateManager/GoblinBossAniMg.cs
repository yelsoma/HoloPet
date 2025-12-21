using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoblinBossAniMg : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private BasicMod basicMod;
    private MountingAbilityMod mountingAbilityMod;
    [SerializeField] private StateBase wander;
    private BattleMod battleMod;
    private AttackableMod attackableMod;
    private void Awake()
    {
        basicMod = GetComponent<IBasicMod>().BasicMod;
        basicMod.StateIdle.OnEnterState += Idle_OnEnterState;
        basicMod.StateInAir.OnEnterState += InAir_OnEnterState;
        basicMod.StateGrabbed.OnEnterState += Grabbed_OnEnterState;
        basicMod.StateClicked.OnEnterState += Clicked_OnEnterState;        
        wander.OnEnterState += Wander_OnEnterState;

        mountingAbilityMod = GetComponent<IMountingAbilityMod>().MountingAbilityMod;
        mountingAbilityMod.StateMounting.OnEnterState += Mounting_OnEnterState;
                
        battleMod = GetComponent<IBattleMod>().BattleMod;
        battleMod.BattleSearch.OnEnterState += BattleSearch_OnEnterState;
        battleMod.BattleSearch.OnTriggerAni1 += BattleSearch_StartSearch;
        battleMod.BattleFall.OnEnterState += BattleFall_OnEnterState;
        battleMod.BattleItemAttack.OnEnterState += BattleItemAttack_OnEnterState;
        battleMod.BattleItemAttack.OnTriggerAni1 += BattleItemAttack_FaceBackToNormal;
        battleMod.BattleBasicAttack.OnEnterState += BattleBasicAttack_OnEnterState;
        battleMod.BattleBasicAttack.OnTriggerAni1 += BattleBasicAttack_FaceBackToNormal;
        battleMod.BattleWin.OnEnterState += BattleWin_OnEnterState;

        attackableMod = GetComponent<IAttackableMod>().AttackableMod;
        attackableMod.StateKnockBack.OnEnterState += AttackedKnockBack_OnEnterState;
        attackableMod.StateKnockBack.OnTriggerAni1 += AttackedKnockBack_OnKnockUpFall;
        attackableMod.StateHpZero.OnEnterState += StateHpZero_OnEnterState;
    }

  
    //attacked
    private void AttackedKnockBack_OnEnterState(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
    private void AttackedKnockBack_OnKnockUpFall(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
    private void StateHpZero_OnEnterState(object sender, EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Fall.ToString(), layer: 0);
        animator.Play(AniEnum.Humanoid.Face.FaceShock.ToString(), layer: 1);
    }

    // basic
    private void Idle_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
        FaceNormal();
    }
    private void InAir_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Fall.ToString(), layer: 0);
        animator.Play(AniEnum.Humanoid.Face.FaceShock.ToString(), layer: 1);
    }
    private void Grabbed_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Grab.ToString(), layer: 0);
        animator.Play(AniEnum.Humanoid.Face.FaceShock.ToString(), layer: 1);
    }
    private void Clicked_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Fall.ToString(), layer: 0);
        animator.Play(AniEnum.Humanoid.Face.FaceHit.ToString(), layer: 1);
    }
    private void Wander_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Walk.ToString(), layer: 0);
        FaceNormal();
    }

    //mounting
    private void Mounting_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Mount.ToString(), layer: 0);
        FaceNormal();
    }

    //battle
    private void BattleFall_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Fall.ToString(), layer: 0);
        animator.Play(AniEnum.Humanoid.Face.FaceShock.ToString(), layer: 1);
    }
    private void BattleSearch_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
        FaceNormal();
    }
    private void BattleSearch_StartSearch(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Run.ToString(), layer: 0);
        FaceNormal();
    }
    private void BattleItemAttack_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
        animator.Play(AniEnum.Humanoid.Face.FaceHit.ToString(), layer: 1);
    }
    private void BattleBasicAttack_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Punch.ToString(), layer: 0);
        animator.Play(AniEnum.Humanoid.Face.FaceHit.ToString(), layer: 1);
    }
    private void BattleBasicAttack_FaceBackToNormal(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
        FaceNormal();
    }
    private void BattleItemAttack_FaceBackToNormal(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Main.Idle.ToString(), layer: 0);
        FaceNormal();
    }
    private void BattleWin_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Humanoid.Face.FaceHappy.ToString(), layer: 1);
        animator.Play(AniEnum.Humanoid.Main.Jump.ToString(), layer: 0);
    }

    //animations
    private void FaceNormal()
    {
        float startPoint = UnityEngine.Random.value;
        animator.Play( AniEnum.Humanoid.Face.FaceNormal.ToString(), 1,startPoint);
    } 
}
