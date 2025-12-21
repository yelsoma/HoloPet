using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_TempItem : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    [SerializeField] private float deleteTime = 3f;
    private float timeNow;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");

        IBasicMod iBasicMod = stateMachine.transform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            basicMod = iBasicMod.BasicMod;
    }

    public override void Enter()
    {
        if (!basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicMod.StateInAir);
        }
        timeNow = deleteTime;
    }

    public override void StateUpdate()
    {
        if (timeNow >= 0f)
        {
            timeNow -= Time.deltaTime;
        }
        else
        {
            stateMachine.ChangeState(basicMod.StateDestroy);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
