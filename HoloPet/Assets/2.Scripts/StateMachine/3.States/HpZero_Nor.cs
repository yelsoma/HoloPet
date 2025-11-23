using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpZero_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    [SerializeField] private float deathTime;
    private float deathTimeNow;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
    }

    public override void Enter()
    {
        deathTimeNow = deathTime;
        basicSM.ClickableMg.SetIsClickable(false);
    }

    public override void StateUpdate()
    {
        deathTimeNow -=Time.deltaTime;
        if(deathTimeNow <= 0 )
        {
            stateMachine.ChangeState(basicSM.StateDestroy);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        basicSM.ClickableMg.SetIsClickable(true);
    }
}
