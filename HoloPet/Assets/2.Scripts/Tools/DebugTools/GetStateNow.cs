using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStateNow : MonoBehaviour
{
    [SerializeField] private StateMachineBase stateMachine;
    private void Update()
    {
        if(stateMachine != null)
        {
            Debug.Log(stateMachine.GetStateNow());
        }  
    }
}
