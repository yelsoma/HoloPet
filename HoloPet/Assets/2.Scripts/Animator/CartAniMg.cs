using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAniMg : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private IBasicSM basicSM;
    private IDriveSM driveSM;
    private IAttackableSM attackableSM;
    private void Awake()
    {
        basicSM = GetComponent<IBasicSM>();
        basicSM.StateIdle.OnEnterState += Idle_OnEnterState;
        basicSM.StateGrabbed.OnEnterState += Grabbed_OnEnterState;
        basicSM.StateGrabbed.OnTriggerAni1 += Grabbed_OnMountedGrabbed;
        basicSM.StateInAir.OnEnterState += InAir_OnEnterState;
        basicSM.StateReleased.OnEnterState += Released_OnEnterState;
        driveSM = GetComponent<IDriveSM>();
        driveSM.StateClickedNor.OnEnterState += ClickedNor_OnEnterState;
        driveSM.StateDirveJump.OnEnterState += DirveJump_OnEnterState;
        driveSM.StateDirveJump.OnTriggerAni1 += DriveJump_OnMountLeft;
        driveSM.StateDirveMax.OnEnterState += DirveMax_OnEnterState;
        driveSM.StateDirveMax.OnTriggerAni1 += DriveMax_OnMountLeft;
        driveSM.StateDrive.OnEnterState += Drive_OnEnterState;
        driveSM.StateDrive.OnTriggerAni1 += Drive_OnMountLeft;
        attackableSM = GetComponent<IAttackableSM>();
        attackableSM.StateKnockBack.OnEnterState += StateKnockBack_OnEnterState;
    }

    private void StateKnockBack_OnEnterState(object sender, System.EventArgs e)
    {
        Idle();
    }

    private void Drive_OnMountLeft(object sender, System.EventArgs e)
    {
        Idle();
    }

    private void DriveMax_OnMountLeft(object sender, System.EventArgs e)
    {
        Idle();
    }

    private void DriveJump_OnMountLeft(object sender, System.EventArgs e)
    {
        Idle();
    }

    private void Grabbed_OnMountedGrabbed(object sender, System.EventArgs e)
    {
        Idle();
    }

    private void Drive_OnEnterState(object sender, System.EventArgs e)
    {
        Drive();
    }
    private void DirveMax_OnEnterState(object sender, System.EventArgs e)
    {
        DriveMax();
    }
    private void DirveJump_OnEnterState(object sender, System.EventArgs e)
    {
        Mounted();
    }
    private void ClickedNor_OnEnterState(object sender, System.EventArgs e)
    {
        Idle();
    }
    private void Released_OnEnterState(object sender, System.EventArgs e)
    {
        Idle();
    }
    private void InAir_OnEnterState(object sender, System.EventArgs e)
    {
        Idle();
    }
    private void Grabbed_OnEnterState(object sender, System.EventArgs e)
    {
        Idle();
    }
    private void Idle_OnEnterState(object sender, System.EventArgs e)
    {
        Idle();
    }

    private void Idle()
    {
        animator.Play(AniEnum.Cart.Main.Idle.ToString());
    }
    private void Drive()
    {
        animator.Play(AniEnum.Cart.Main.Dash.ToString());
    }
    private void DriveMax()
    {
        animator.Play(AniEnum.Cart.Main.DashMaxSpeed.ToString());
    }
    private void Mounted()
    {
        animator.Play(AniEnum.Cart.Main.Mounted.ToString());
    }
}
