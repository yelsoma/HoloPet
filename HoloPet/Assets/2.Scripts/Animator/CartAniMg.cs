using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAniMg : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private IBasicSM basicSM;
    private IDriveSM driveSM;
    [SerializeField] private DriveJump_Cart driveJump;
    [SerializeField] private DriveMax_Cart driveMax;
    [SerializeField] private Drive_Cart drive;
    [SerializeField] private Grabbed_Cart grabbed;
    private IAttackableSM attackableSM;
    private void Awake()
    {
        basicSM = GetComponent<IBasicSM>();
        basicSM.StateIdle.OnEnterState += Idle_OnEnterState;
        grabbed.OnEnterState += Grabbed_OnEnterState;
        grabbed.OnMountedGrabbed += Grabbed_OnMountedGrabbed;
        basicSM.StateInAir.OnEnterState += InAir_OnEnterState;
        basicSM.StateReleased.OnEnterState += Released_OnEnterState;
        driveSM = GetComponent<IDriveSM>();
        driveSM.StateClickedNor.OnEnterState += ClickedNor_OnEnterState;
        driveJump.OnEnterState += DirveJump_OnEnterState;
        driveJump.OnMountLeft += DriveJump_OnMountLeft;
        driveMax.OnEnterState += DirveMax_OnEnterState;
        driveMax.OnMountLeft += DriveMax_OnMountLeft;
        drive.OnEnterState += Drive_OnEnterState;
        drive.OnMountLeft += Drive_OnMountLeft;
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
