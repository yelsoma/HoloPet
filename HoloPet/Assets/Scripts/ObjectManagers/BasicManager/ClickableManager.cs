using System;
using UnityEngine;

public class ClickableManager : MonoBehaviour
{
    public event EventHandler<GrabEventArgs> OnGrab;
    public class GrabEventArgs : EventArgs
    {
        public Vector2 MousePosition;
    }
    public event EventHandler OnRelease;

    [SerializeField] private StateBase[] unClickableStates;

    private StateMachineBase stateMachine;
    private IBasicSM basicSM;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform} ¡X no basicSM found in parent.");
        }
    }

    public void Click()
    {
        // go to state Clicked
        stateMachine.ChangeState(basicSM.StateClicked);
    }

    public void Grab(Vector2 mousePosition)
    {
        // go to state Grabbed
        stateMachine.ChangeState(basicSM.StateGrabbed);
        OnGrab?.Invoke(this, new GrabEventArgs { MousePosition = mousePosition});
    }

    public void Release()
    {
        OnRelease?.Invoke(this, EventArgs.Empty);
    }

    public bool GetIsNowClickable()
    {
        if (unClickableStates == null || unClickableStates.Length == 0)
            return true;

        StateBase currentState = stateMachine.GetStateNow();
        foreach (StateBase state in unClickableStates)
        {
            if (currentState == state)
                return false;
        }

        return true;
    }
}
