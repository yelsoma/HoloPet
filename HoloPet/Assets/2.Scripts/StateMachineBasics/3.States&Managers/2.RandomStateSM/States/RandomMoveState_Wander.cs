using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveState_Wander : StateBase
{
    [SerializeField] private float wanderMaxTime;
    [SerializeField] private float wanderMinTime;
    [SerializeField] private float wanderSpeed;
    private float wanderTimer;
    private float randomDir;
    private bool wanderRight;
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IRandomMoveSM randomMoveSM;

    #region AutoSetRef
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform} ¡X no IBasicSM found in parent.");
        }

        randomMoveSM = GetComponentInParent<IRandomMoveSM>();
        if (randomMoveSM == null)
        {
            Debug.LogError($"{transform} ¡X no IRandomMoveSM found in parent.");
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
            basicSM.FaceDirectionMg.SetFaceRight();
        }
        else
        {
            wanderRight = false;
            basicSM.FaceDirectionMg.SetFaceLeft();
        }
    }

    public override void StateUpdate()
    {
        //side check
        if (basicSM.BoundaryMg.CheckIsLeftBounderyAndResetPos())
        {
            wanderRight = true;
            basicSM.FaceDirectionMg.SetFaceRight();
        }
        if (basicSM.BoundaryMg.CheckIsRightBounderyAndResetPos())
        {

            wanderRight = false;
            basicSM.FaceDirectionMg.SetFaceLeft();
        }
        //time check
        if (wanderTimer <= 0f)
        {
            //exit to idle
            stateMachine.ChangeState(basicSM.StateIdle);
            return;
        }
        // keep  wander right
        wanderTimer -= Time.deltaTime;
    }
    public override void StateLateUpdate()
    {
        if (wanderRight)
        {
            basicSM.MovementMg.MoveRight(wanderSpeed);
        }
        else
        {
            basicSM.MovementMg.MoveLeft(wanderSpeed);
        }
    }
    public override void Exit()
    {
    }
    #endregion
}
