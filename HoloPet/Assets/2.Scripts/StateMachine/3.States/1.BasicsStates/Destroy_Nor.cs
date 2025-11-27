using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicMod basicMod;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        basicMod = GetComponentInParent<IBasicMod>();
        if (basicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
    }

    public override void Enter()
    {
        //remove layer in layerMg
    }

    public override void StateUpdate()
    {
        Destroy(stateMachine.gameObject);
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
