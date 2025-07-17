using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStateNow : MonoBehaviour
{
    [SerializeField] private StateMachineBase stateMachine;
    private void Update()
    {
        Debug.Log(stateMachine.GetStateNow());        
    }
}
