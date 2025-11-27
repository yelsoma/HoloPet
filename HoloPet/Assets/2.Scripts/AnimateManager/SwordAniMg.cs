using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAniMg : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private StateBase swing;
    [SerializeField] private StateBase hold;
    [SerializeField] private BasicMod basicMod;
    private void Awake()
    {
        basicMod= GetComponent<IBasicMod>().BasicMod;
        basicMod.StateIdle.OnEnterState += Idle_OnEnterState;
        basicMod.StateInAir.OnEnterState += InAir_OnEnterState;
        basicMod.StateGrabbed.OnEnterState += Grabbed_OnEnterState;
        basicMod.StateClicked.OnEnterState += Clicked_OnEnterState;
        swing.OnTriggerAni1 += Swing_OnSwingAni;
        hold.OnTriggerAni1 += Hold_OnTriggerAni1;

    }

    private void Hold_OnTriggerAni1(object sender, System.EventArgs e)
    {
        SwordHold();
    }
 
    private void Swing_OnSwingAni(object sender, System.EventArgs e)
    {
        SwordSwing();
    }

    //basic

    private void Idle_OnEnterState(object sender, System.EventArgs e)
    {
        SwordIdle();
    }
    private void InAir_OnEnterState(object sender, System.EventArgs e)
    {
        SwordFall();
    }
    private void Grabbed_OnEnterState(object sender, System.EventArgs e)
    {
        SwordFall();
    }
    private void Clicked_OnEnterState(object sender, System.EventArgs e)
    {
        SwordFall();
    }

    // animations
    private void SwordSwing()
    {
        animator.Play("SwordSwing", 0, 0);
    }
    private void SwordFall()
    {
        animator.Play("SwordFall");
    }
    private void SwordIdle()
    {
        animator.Play("SwordIdle");
    }
    private void SwordHold()
    {
        animator.Play("SwordHold");
    }
}
