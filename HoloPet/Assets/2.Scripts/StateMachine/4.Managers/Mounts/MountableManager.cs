using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MountableManager : MonoBehaviour
{
    private bool isMountableState = true;
    private bool isMounted = false;
    private MountingAbilityManager myMounter;
    [SerializeField] Transform mountingPoint;
    [SerializeField] StateBase[] unMountableStates;
    private Transform stateMachineTransform;

    private void Awake()
    {
        stateMachineTransform = transform.root;
        if(unMountableStates.Length > 0)
        {
            foreach (StateBase unmountableState in unMountableStates)
            {
                unmountableState.OnEnterState += UnmountableState_OnEnterState;
                unmountableState.OnExitState += UnmountableState_OnExitState;
            }
        }
        else
        {
            Debug.LogWarning(stateMachineTransform.name + "'s " + "MountableMg unMountableState is 0");
        }
        if(mountingPoint == null)
        {
            Debug.LogError(stateMachineTransform.name + "'s " + "MountableMg MountPoint not set");
        }
    }

    private void UnmountableState_OnEnterState(object sender, System.EventArgs e)
    {
        isMountableState = false;
    }

    private void UnmountableState_OnExitState(object sender, System.EventArgs e)
    {
        isMountableState = true;
    }

    public bool GetIsMountable()
    {
        if (isMountableState == true && isMounted == false)
        {
            return true;
        }
        return false;
    }
    public bool GetIsMounted()
    {
        return isMounted;
    }

    public bool GetIsMountableState()
    {
        return isMountableState;
    }

    public MountingAbilityManager GetMounter()
    {
        return myMounter;
    }

    public void SetIsMounted(bool isMounted)
    {
        this.isMounted = isMounted;
    }

    public void SetMounter(MountingAbilityManager mounter)
    {
        myMounter = mounter;
    }

    public void SetIsMountableState(bool isMountableState)
    {
        this.isMountableState = isMountableState;
    }

    public Transform GetMountPointTansform()
    {
        return mountingPoint;
    }

    public Transform GetStateMachineTransform()
    {
        return stateMachineTransform;
    }
}
