using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreatureState_Wander : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private ICreatureSM creatureSM;
    [SerializeField] private float wanderMaxTime;
    [SerializeField] private float wanderMinTime;
    [SerializeField] private float wanderSpeed;
    private float wanderTime;
    private float randomDir;
    private bool wanderRight;
    private Coroutine wanderCoroutine;

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
            Debug.LogError($"{transform} ¡X no basicSM found in parent.");
        }

        creatureSM = GetComponentInParent<ICreatureSM>();
        if (creatureSM == null)
        {
            Debug.LogError($"{transform} ¡X no ICreatureSM found in parent.");
        }
    }

    #region StateBase
    public override void Enter()
    {
        wanderCoroutine = StartCoroutine(CoStartWander());
    }

    public override void StateUpdate()
    {

    }

    public override void StateLateUpdate()
    {

    }

    public override void Exit()
    {
        StopCoroutine(wanderCoroutine);
    }
    #endregion

    #region Coroutine
    private IEnumerator CoStartWander()
    {
        wanderTime = UnityEngine.Random.Range(wanderMinTime, wanderMaxTime);
        randomDir = UnityEngine.Random.Range(0f, 1f);
        if (randomDir < 0.5)
        {
            wanderRight = true;
        }
        else
        {
            wanderRight = false;
        }
        while (wanderTime > 0)
        {
            if (wanderRight)
            {
                basicSM.Movement.MoveRight(wanderSpeed);
                if (basicSM.BoundaryManager.CheckIsRightBounderyAndResetPos())
                {
                    wanderRight = false;
                }
            }
            else
            {
                basicSM.Movement.MoveLeft(wanderSpeed);
                if (basicSM.BoundaryManager.CheckIsLeftBounderyAndResetPos())
                {
                    wanderRight = true;
                }
            }
            wanderTime -= Time.deltaTime;
            yield return null;
        }
        stateMachine.ChangeState(basicSM.StateIdle);
    }
    #endregion
}
