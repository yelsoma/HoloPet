using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMountable 
{
    public bool GetIsMountable();
    public bool GetIsMounted();
    public void SetIsMounted(bool isMounted);
    public void SetMounter(IMountingAbility mounter);
    public IMountingAbility GetMounter();
    public Transform GetTransform();
    public bool GetIsMountableState();
    public void SetIsMountableState(bool isMountableState);
    public Transform GetMountPointTansform();
}
