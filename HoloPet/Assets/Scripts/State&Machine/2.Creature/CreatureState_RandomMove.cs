using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureState_RandomMove : StateBase
{
    private StateMachineBase stateMachine;
    private IRandomMoveSM creatureSM;
    [SerializeField] private StateBase[] randomStates;
    private int randomStateInt;

    #region AutoSetRef
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }

        creatureSM = GetComponentInParent<IRandomMoveSM>();
        if (creatureSM == null)
        {
            Debug.LogError($"{transform} ¡X no ICreatureSM found in parent.");
        }
    }
    #endregion

    #region StateBase
    //StateBase
    public override void Enter()
    {
        if (randomStates.Length > 0)
        {
            randomStateInt = UnityEngine.Random.Range(0, randomStates.Length);
            stateMachine.ChangeState(randomStates[randomStateInt]);
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
