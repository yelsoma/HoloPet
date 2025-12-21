using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAniMg : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private BasicMod basicMod;
    private ItemMod itemMod;
    private void Awake()
    {
        basicMod= GetComponent<IBasicMod>().BasicMod;
        basicMod.StateIdle.OnEnterState += Idle_OnEnterState;
        basicMod.StateInAir.OnEnterState += InAir_OnEnterState;
        basicMod.StateGrabbed.OnEnterState += Grabbed_OnEnterState;
        basicMod.StateClicked.OnEnterState += Clicked_OnEnterState;
        itemMod = GetComponent<IItemMod>().ItemMod;
        itemMod.StateItemUse.OnEnterState += MeleeAtk_OnEnterState;
        itemMod.StateHold.OnEnterState += Hold_OnEnterState;

    }

    private void Hold_OnEnterState(object sender, System.EventArgs e)
    {
        Hold();
    }
 
    private void MeleeAtk_OnEnterState(object sender, System.EventArgs e)
    {
        Attack();
    }

    //basic
    private void Idle_OnEnterState(object sender, System.EventArgs e)
    {
        Idle();
    }
    private void InAir_OnEnterState(object sender, System.EventArgs e)
    {
        Fall();
    }
    private void Grabbed_OnEnterState(object sender, System.EventArgs e)
    {
        Fall();
    }
    private void Clicked_OnEnterState(object sender, System.EventArgs e)
    {
        Fall();
    }

    // animations
    private void Attack()
    {
        animator.Play("Attack", 0, 0);
    }
    private void Idle()
    {
        animator.Play("Idle");
    }
    private void Hold()
    {
        animator.Play("Hold");
    }
    private void Fall()
    {
        animator.Play("Fall");
    }
}
