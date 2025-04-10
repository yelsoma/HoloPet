using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanataAnimator : MonoBehaviour
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
    [SerializeField] private KanataRage rageState;
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
        rageState.OnEnterState += RageState_OnEnterState;
        rageState.OnPunch += RageState_OnPunch;
        interactState.OnInteractHappy += InteractState_OnInteractHappy;
        interactedState.OnInteractHappy += InteractedState_OnInteractHappy;
        findTargetState.OnFindTarget += FindTargetState_OnFindTarget;
        
    }

    private void FindTargetState_OnFindTarget(object sender, System.EventArgs e)
    {
        animator.Play("KanataWander");
    }

    private void InteractedState_OnInteractHappy(object sender, System.EventArgs e)
    {
        animator.Play("KanataHappy");
    }

    private void InteractState_OnInteractHappy(object sender, System.EventArgs e)
    {
        animator.Play("KanataHappy");
    }

    private void RageState_OnPunch(object sender, System.EventArgs e)
    {
        animator.Play("KanataPunch");
    }

    private void RageState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("KanataRageFall");
    }

    private void MountingState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("KanataMount");
    }

    private void KnockUpState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("KanataKnockUp",0,0);
    }

    private void GrabState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("KanataGrab");
    }

    private void WanderState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("KanataWander");
    }

    private void WanderLeftState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("KanataWander");
    }

    private void FallState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("KanataFall");
    }

    private void IdleState_OnEnterState(object sender, System.EventArgs e)
    {
        animator.Play("KanataIdle");
    }

    

}
