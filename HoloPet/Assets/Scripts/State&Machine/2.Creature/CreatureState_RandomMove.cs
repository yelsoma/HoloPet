using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureState_RandomMove : StateBase
{
    private StateMachineBase stateMachine;
    private ICreatureSM creatureSM;
    [SerializeField] private StateBase[] randomStates;
    private int randomStateInt;

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
    //StateBase
    public override void Enter()
    {
        idleTime = UnityEngine.Random.Range(idleTimeMin, idleTimeMax);
        idleTimeCoroutine = StartCoroutine(CoIdleTime());

    }

    public override void StateUpdate()
    {
        if (!basicSM.BoundaryManager.CheckIsBotBounderyAndResetPos())
        {
            // exit to StateInAir
            stateMachine.ChangeState(basicSM.StateInAir);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        StopCoroutine(idleTimeCoroutine);
    }
    #endregion
    #region Coroutine
    //Coroutine
    private IEnumerator CoIdleTime()
    {
        yield return new WaitForSeconds(idleTime);
        // exit to StateRandomMove
        if (randomStates.Length > 0)
        {
            randomStateInt = UnityEngine.Random.Range(0, randomStates.Length);
            stateMachine.ChangeState(randomStates[randomStateInt]);
        }
        else
        {
            stateMachine.ChangeState(basicSM.StateIdle);
        }
    }
    #endregion
}
