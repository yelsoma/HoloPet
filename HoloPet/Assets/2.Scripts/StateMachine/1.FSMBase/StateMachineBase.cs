using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachineBase : MonoBehaviour
{
 
    private StateBase currentState;
    private void Start()
    {
        GameController.Instance.StateMachineListMg.AddObjectTolist(this);
        StartCoroutine(InitStateNextFrame());
    }
    private IEnumerator InitStateNextFrame()
    {
        yield return null; // wait one frame
        if (currentState == null)
        {
            currentState = StateOverride(SetFirstState());
            if (currentState != null)
            {
                currentState.Enter();
                currentState.EnterStateEvent();
            }
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
        newState = StateOverride(newState);
        if (newState == null)
        {
            Debug.LogError($"StatePassInIsNull | Called by: {transform.root.name}.");
            return;
        }
        if (currentState != null)
        {
            currentState.Exit();
            currentState.ExitStateEvent();
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
    protected virtual StateBase StateOverride(StateBase requested)
    {
        return requested; // default: do nothing
    }
}
