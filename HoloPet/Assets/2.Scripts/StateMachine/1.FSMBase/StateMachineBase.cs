using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachineBase : MonoBehaviour
{
 
    private StateBase currentState;
    private void Start()
    {       
        currentState = SetFirstState();
        if (currentState != null)
        {
            currentState.EnterStateEvent();
            currentState.Enter();
        }
    }
    private void Update()
    {
        if( currentState != null)
        {
            currentState.StateUpdate();
        }      
    }
    private void LateUpdate()
    {
        if(currentState != null)
        {
            currentState.StateLateUpdate();
        }
    }
    public void ChangeState(StateBase newState)
    {
        currentState.Exit();
        currentState.ExitStateEvent();
        currentState = newState;
        currentState.Enter();
        currentState.EnterStateEvent();
    }
    public StateBase GetStateNow()
    {
        return currentState;
    }
    protected virtual StateBase SetFirstState()
    {
        return null;
    }
    public bool IsStateCurrent(StateBase stateToCheck)
    {
        if(stateToCheck == currentState)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
