using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureState_Idle : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IRandomMoveSM creatureSM;
    [SerializeField] private float idleTimeMax;
    [SerializeField] private float idleTimeMin;
    private Coroutine idleTimeCoroutine;
    private float idleTime;

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

        creatureSM = GetComponentInParent<IRandomMoveSM>();
        if (creatureSM == null)
        {
            Debug.LogError($"{transform} ¡X no ICreatureSM found in parent.");          
        }
    }
    #region StateBase
    //StateBase
    public override void Enter()
    {
        idleTime = UnityEngine.Random.Range(idleTimeMin,idleTimeMax);
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
        stateMachine.ChangeState(creatureSM.StateWander);
    }
    #endregion
}
