using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoloMemInputManager : MonoBehaviour ,IClickable
{
    [SerializeField] HoloMemStateMachine stateMachine;
    private bool isNowClickable = true;
    //event
    public event EventHandler OnMouseRelease;

    public void Click()
    {
        stateMachine.ChangeState(stateMachine.stateKnockUp);
    }
    public void Drag(Vector2 mouseVector2)
    {        
        // if is not grab change to grab
        if(stateMachine.GetStateNow() == stateMachine.stateGrab)
        {
            stateMachine.transform.position = mouseVector2;
        }
        else
        {
            stateMachine.ChangeState(stateMachine.stateGrab);
        }
    }
    public void Release()
    {
        OnMouseRelease?.Invoke(this, EventArgs.Empty);
    }

    public void SetIsNowClickable(bool isNowClickable)
    {
        this.isNowClickable = isNowClickable;
    }
    public bool GetIsNowClickable()
    {
        return isNowClickable;
    }
}

