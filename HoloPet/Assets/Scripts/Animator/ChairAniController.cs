using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairAniController : MonoBehaviour
{
    [Header("Need Refernce")]
    [SerializeField] private Animator animator;

    [Header("States")]
    [SerializeField] public FurnitureState_Spawn stateSpawn;
    [SerializeField] public FurnitureState_Grab stateGrab;

    private void Awake()
    {
        stateSpawn.OnEnterState += StateSpawn_OnEnterState;
        stateGrab.OnEnterState += StateGrab_OnEnterState;
        stateGrab.OnLeaveGrab += StateGrab_OnLeaveGrab;
    }

    private void StateGrab_OnLeaveGrab(object sender, System.EventArgs e)
    {
        animator.Play("ChairIdle");
    }

    private void StateGrab_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("ChairGrab");
    }

    private void StateSpawn_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("ChairIdle");
    }
}
