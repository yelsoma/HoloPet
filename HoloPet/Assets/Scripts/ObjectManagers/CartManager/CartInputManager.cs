using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartInputManager : MonoBehaviour ,IClickable
{
    [SerializeField] private CartStateMachine stateMachine;
    private bool isNowClickable = true;
    public void Click()
    {
        if (stateMachine.mountManager.GetIsMounted())
        {
            stateMachine.ChangeState(stateMachine.stateJump);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.stateKnockUp);
        }       
    }

    public void Drag(Vector2 mouseVector2)
    {
        // if is not grab change to grab
        if (stateMachine.GetStateNow() == stateMachine.stateGrab)
        {
            stateMachine.transform.position = mouseVector2;
        }
        else
        {
            stateMachine.ChangeState(stateMachine.stateGrab);
        }
    }

    public bool GetIsNowClickable()
    {
        return isNowClickable;
    }

    public void SetIsNowClickable(bool isNowClickable)
    {
        this.isNowClickable = isNowClickable;
    }

    public void Release()
    {
        if(stateMachine.GetStateNow() == stateMachine.stateGrab)
        {
            stateMachine.ChangeState(stateMachine.stateFall);
        }       
    }
}
