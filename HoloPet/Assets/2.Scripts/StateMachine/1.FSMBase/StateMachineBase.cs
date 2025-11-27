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
            currentState.Enter();
            currentState.EnterStateEvent();
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
        if(newState == null)
        {
            Debug.LogError($"StatePassInIsNull | Called by: { transform.root.name + currentState?.GetType().Name}");
            return;
        }
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
