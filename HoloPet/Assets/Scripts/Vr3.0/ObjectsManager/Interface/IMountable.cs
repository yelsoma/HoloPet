using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMountable 
{
    public void SetIsMounted(bool isMounted);
    public void SetMounter(IMountingAbility mounter);
    public IMountingAbility GetMounter();
    public bool GetIsMountable();
    public Transform GetTransform();
    public Vector2 GetMountingPoint();
    public bool GetMountingDirIsRight();
    public bool GetIsMountableState();
}
