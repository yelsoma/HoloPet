using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotanAnimator : MonoBehaviour
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
        animator.Play("BotanWander");
    }
    private void InteractedState_OnInteractHappy(object sender, System.EventArgs e)
    {
        animator.Play("BotanHappy");
    }

    private void InteractState_OnInteractHappy(object sender, System.EventArgs e)
    {
        animator.Play("BotanHappy");
    }

    private void MountingState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanMount");
    }

    private void KnockUpState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanKnockUp", 0, 0);
    }

    private void GrabState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanGrab");
    }

    private void WanderState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanWander");
    }

    private void WanderLeftState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanWander");
    }

    private void FallState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanFall");
    }

    private void IdleState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("BotanIdle");
    }
}
