using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatameAnimator : MonoBehaviour
{
    //ref
    [SerializeField] private Animator animator;
    [SerializeField] private IdolStateMachine stateMachine;

    //states
    [SerializeField] private IdolIdle idleState;
    [SerializeField] private IdolFall fallState;
    [SerializeField] private IdolWander wanderState;
    [SerializeField] private IdolGrab grabState;
    [SerializeField] private IdolKnockup knockUpState;
    [SerializeField] private IdolMounting mountingState;
    [SerializeField] private IdolInteract interactState;
    [SerializeField] private IdolInteracted interactedState;
    [SerializeField] private IdolFIndTarget findTargetState;

    private void Awake()
    {
        idleState.OnEnterState += IdleState_OnEnterState;
        fallState.OnEnterState += FallState_OnEnterState;
        wanderState.OnEnterState += WanderState_OnEnterState;
        grabState.OnEnterState += GrabState_OnEnterState;
        knockUpState.OnEnterState += KnockUpState_OnEnterState;
        mountingState.OnEnterState += MountingState_OnEnterState;
        interactState.OnInteractHappy += InteractState_OnInteractHappy;
        interactedState.OnInteractHappy += InteractedState_OnInteractHappy;
        findTargetState.OnFindTarget += FindTargetState_OnFindTarget;
    }

    private void FindTargetState_OnFindTarget(object sender, System.EventArgs e)
    {
        animator.Play("WatameIdle");
    }
    private void InteractedState_OnInteractHappy(object sender, System.EventArgs e)
    {
        animator.Play("WatameIdle");
    }

    private void InteractState_OnInteractHappy(object sender, System.EventArgs e)
    {
        animator.Play("WatameIdle");
    }

    private void MountingState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("WatameIdle");
    }

    private void KnockUpState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("WatameIdle", 0, 0);
    }

    private void GrabState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("WatameIdle");
    }

    private void WanderState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("WatameIdle");
    }

    private void WanderLeftState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("WatameIdle");
    }

    private void FallState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("WatameIdle");
    }

    private void IdleState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("WatameIdle");
    }
}
