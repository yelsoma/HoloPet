using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureState_RandomMove : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
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
    public override void Enter()
    {
        if(randomStates.Length > 0)
        {
            randomStateInt = UnityEngine.Random.Range(0, randomStates.Length);
            // state change to idle
            stateMachine.ChangeState(randomStates[randomStateInt]);
        }
        else
        {
            stateMachine.ChangeState(basicSM.StateIdle);
        }
    }

    public override void StateUpdate()
    {

    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {

    }
    #endregion
}
