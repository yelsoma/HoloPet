using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander_Nor : StateBase
{
    [SerializeField] private float wanderMaxTime;
    [SerializeField] private float wanderMinTime;
    private float wanderTimer;
    private float randomDir;
    private bool wanderRight;
    private StateMachineBase stateMachine;
    private BasicMod basicMod;

    #region AutoSetRef
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        else
        {
            basicMod = ibasicMod.BasicMod;
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        wanderTimer = UnityEngine.Random.Range(wanderMinTime, wanderMaxTime);
        randomDir = UnityEngine.Random.Range(1f, 0f);
        if (randomDir >= 0.5)
        {
            wanderRight = true;
            basicMod.FaceDirectionMg.SetFaceRight();
        }
        else
        {
            wanderRight = false;
            basicMod.FaceDirectionMg.SetFaceLeft();
        }
        if (!basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicMod.StateInAir);
        }
    }

    public override void StateUpdate()
    {
        //side check
        if (basicMod.BoundaryMg.CheckIsLeftBounderyAndResetPos())
        {
            wanderRight = true;
            basicMod.FaceDirectionMg.SetFaceRight();
        }
        if (basicMod.BoundaryMg.CheckIsRightBounderyAndResetPos())
        {

            wanderRight = false;
            basicMod.FaceDirectionMg.SetFaceLeft();
        }
        //time check
        if (wanderTimer <= 0f)
        {
            //exit to idle
            stateMachine.ChangeState(basicMod.StateIdle);
            return;
        }
        // keep  wander right
        wanderTimer -= Time.deltaTime;
    }
    public override void StateLateUpdate()
    {
        if (wanderRight)
        {
            basicMod.PhysicsMg.MoveRightMultiply(1f);
        }
        else
        {
            basicMod.PhysicsMg.MoveLeftMultiply(1f);
        }
    }
    public override void Exit()
    {
    }
    #endregion
}
