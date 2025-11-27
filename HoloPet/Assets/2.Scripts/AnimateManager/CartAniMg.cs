using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAniMg : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private IDriveSM driveSM;
    private IAttackableSM attackableSM;
    private MountableMod mountableMod;
    private void Awake()
    {
        basicMod = GetComponent<IBasicMod>().BasicMod;
        basicMod.StateIdle.OnEnterState += Idle_OnEnterState;
        basicMod.StateGrabbed.OnEnterState += Grabbed_OnEnterState;
        basicMod.StateInAir.OnEnterState += StateInAir_OnEnterState;
        driveSM = GetComponent<IDriveSM>();
        driveSM.StateClickedNor.OnEnterState += ClickedNor_OnEnterState;
        driveSM.StateDirveJump.OnEnterState += DirveJump_OnEnterState;
        driveSM.StateDirveMax.OnEnterState += DirveMax_OnEnterState;
        driveSM.StateDrive.OnEnterState += Drive_OnEnterState;
        attackableSM = GetComponent<IAttackableSM>();
        attackableSM.StateKnockBack.OnEnterState += StateKnockBack_OnEnterState;
        attackableSM.StateHpZero.OnEnterState += StateHpZero_OnEnterState;
        //mounted change
        stateMachine = GetComponent<StateMachineBase>();
        mountableMod = GetComponent<IMountableMod>().MountableMod;
        mountableMod.MountableMg.OnChangeMounted += MountableMg_OnChangeMounted;
        
    }

    private void StateHpZero_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("CartDeadFlash");
    }
    
    private void StateKnockBack_OnEnterState(object sender, System.EventArgs e)
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
   
    private void Idle_OnEnterState(object sender, System.EventArgs e)
    {
        Idle();
    }
    //mounted OnChange
    private void MountableMg_OnChangeMounted(object sender, System.EventArgs e)
    {
        bool isMounted = mountableMod.MountableMg.GetIsMounted();
        if (stateMachine.GetStateNow() == driveSM.StateDrive)
        {
            if (isMounted)
            {
                Drive();
                return;
            }
            else
            {
                Break();
                return;
            }
        }
        if (stateMachine.GetStateNow() == driveSM.StateDirveMax)
        {
            if (isMounted)
            {
                DriveMax();
                return;
            }
            else
            {
                Break();
                return;
            }
        }
        if (isMounted)
        {
            Mounted();
            return;
        }
        else
        {
            Idle();
            return;
        }
    }

    private void StateInAir_OnEnterState(object sender, System.EventArgs e)
    {
        if (mountableMod.MountableMg.GetIsMounted())
        {
            Mounted();
        }
        else
        {
            Idle();
        }
    }
    private void Grabbed_OnEnterState(object sender, System.EventArgs e)
    {
        if (mountableMod.MountableMg.GetIsMounted())
        {
            Mounted();
            return;
        }
        else
        {
            Idle();
            return;
        }
    }

    //animations
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
    private void Break()
    {
        animator.Play(AniEnum.Cart.Main.Break.ToString());
    }
    
}
