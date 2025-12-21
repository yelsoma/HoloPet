using System;
using UnityEngine;

public class ClickableManager : MonoBehaviour
{
    public event EventHandler<GrabEventArgs> OnGrabMousePos;
    public class GrabEventArgs : EventArgs
    {
        public Vector2 MousePosition;
    }
    public event EventHandler OnRelease;

    [SerializeField] private StateBase[] unClickableStates;

    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private bool isClickable;

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

    private void Start()
    {
        SetIsClickable(true);
    }
    public void Click()
    {
        if (!isClickable)
            return;
        // go to state Clicked
        stateMachine.ChangeState(basicMod.StateClicked);
    }

    public void Grab()
    {
        // go to state Grabbed
        if (!isClickable)
            return;
        stateMachine.ChangeState(basicMod.StateGrabbed);
    }

    public void GrabMousePos(Vector2 mousePosition)
    {
        OnGrabMousePos?.Invoke(this, new GrabEventArgs { MousePosition = mousePosition });
    }

    public void Release()
    {
        if (!isClickable)
            return;
        OnRelease?.Invoke(this, EventArgs.Empty);
    }

    public bool GetIsClickable()
    {       
        bool isClickableState = true;
        StateBase currentState = stateMachine.GetStateNow();
        if (unClickableStates.Length >= 0)
        {
            foreach (StateBase state in unClickableStates)
            {
                if (currentState == state)
                {
                    isClickableState = false;
                    break;
                }
            }
        }       
        if(isClickableState && isClickable)
        {
            return true;
        }
        return false;
    }

    public void SetIsClickable(bool isClickable)
    {
        this.isClickable = isClickable;
    }
}
