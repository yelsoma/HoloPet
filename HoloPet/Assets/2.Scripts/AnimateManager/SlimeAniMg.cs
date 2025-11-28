using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAniMg : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private BasicMod basicMod;
    private AttackAbilityMod attackAbilityMod;
    [SerializeField] private BasicAttack_Slime slimeAttack;
    [SerializeField] private Search_JumpEnemy searchJump;

    private void Awake()
    {
        basicMod = GetComponent<IBasicMod>().BasicMod;
        basicMod.StateIdle.OnEnterState += StateIdle_OnEnterState;
        basicMod.StateInAir.OnEnterState += StateInAir_OnEnterState;
        basicMod.StateClicked.OnEnterState += StateClicked_OnEnterState;
        attackAbilityMod = GetComponent<IAttackAbilityMod>().AttackAbilityMod;
        attackAbilityMod.StateSearch.OnEnterState += StateSearch_OnEnterState;
        attackAbilityMod.StateBasicAttack.OnEnterState += StateBasicAttack_OnEnterState;
        attackAbilityMod.StateBasicAttack.OnTriggerAni1 += StateBasicAttack_OnTriggerAni1;
        searchJump.OnTriggerAni1 += SearchJump_OnTriggerAni1;// startJump ani
        searchJump.OnTriggerAni2 += SearchJump_OnTriggerAni2;// seach no one idle ani
    }

    private void SearchJump_OnTriggerAni2(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Common.Idle.ToString());
    }

    private void SearchJump_OnTriggerAni1(object sender, System.EventArgs e)
    {
        float normalizedTime;

        int roll = Random.Range(0, 4); // 0,1,2,3
        if (roll < 2) // 0 or 1 ¡÷ 50%
            normalizedTime = 0.6f; // frame 6
        else if (roll == 2) // 25%
            normalizedTime = 0f;   // frame 0
        else // roll == 3 ¡÷ 25%
            normalizedTime = 0.3f; // frame 3

        animator.Play(AniEnum.Common.Jump.ToString(), 0, normalizedTime);
        animator.Update(0f); // snap to chosen frame immediately
    }

    private void StateBasicAttack_OnTriggerAni1(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Common.Hit.ToString());
    }

    private void StateBasicAttack_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Common.BasicAttack.ToString());
    }

    private void StateSearch_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Common.Jump.ToString());
    }

    private void StateClicked_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Common.Fall.ToString());
    }

    private void StateInAir_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Common.Fall.ToString());
    }

    private void StateIdle_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(AniEnum.Common.Idle.ToString());
    }

    public void StartAttackJump()
    {
        slimeAttack.StartAttackJump();
    }

    public void StartSearchJump()
    {
        searchJump.SearchJumpStart();
    }
}
