using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpZero_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    [SerializeField] private float deathTime;
    private float deathTimeNow;

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

    public override void Enter()
    {
        deathTimeNow = deathTime;
        basicMod.ClickableMg.SetIsClickable(false);
    }

    public override void StateUpdate()
    {
        deathTimeNow -=Time.deltaTime;
        if(deathTimeNow <= 0 )
        {
            stateMachine.ChangeState(basicMod.StateDestroy);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        basicMod.ClickableMg.SetIsClickable(true);
    }
}
