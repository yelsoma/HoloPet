using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairAnimator : MonoBehaviour
{
    //Reference
    [SerializeField] private Animator animator;
    [SerializeField] private FurnitureStateMachine stateMachine;

    //States
    [SerializeField] private FurnitureIdle idle;
    [SerializeField] private FurnitureGrab grab;
    [SerializeField] private FurnitureFall fall;
    [SerializeField] private FurnitureKnockUp knockUp;
    [SerializeField] private FurnitureMounted mounted;

    //Animation const
    private const string _Idle = "ChairIdle";
    private const string _Fall = "ChairFall";
    private const string _Grab = "ChairGrab";
    private const string _KnockUp = "ChairKnockUp";
    private const string _Mounted = "ChairMounted";

    private void Awake()
    {
        idle.OnEnterState += Idle_OnEnterState;
        grab.OnEnterState += Grab_OnEnterState;
        fall.OnEnterState += Fall_OnEnterState;
        knockUp.OnEnterState += KnockUp_OnEnterState;
        mounted.OnEnterState += Mounted_OnEnterState;
    }

    private void Mounted_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(_Mounted);
    }

    private void KnockUp_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(_KnockUp, 0, 0);
    }
    private void Fall_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(_Fall);
    }

    private void Grab_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(_Grab, 0, 0);
    }

    private void Idle_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play(_Idle);
    }
}
