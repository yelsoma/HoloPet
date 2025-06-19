using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountingAbilityManager : MonoBehaviour
{
    private bool isMounting;
    private MountableManager myMount;
    private Transform stateMachineTransform;

    private void Awake()
    {
        stateMachineTransform = transform.root;
    }

    public bool GetIsMounting()
    {
        return isMounting;
    }
    public void SetIsMounting(bool isMounting)
    {
        this.isMounting = isMounting;
    }
    public MountableManager GetMount()
    {
        return myMount;
    }
    public bool TrySetMount(MountableManager mount)
    {
        if (mount.GetIsMountable())
        {
            myMount = mount;
            return true;
        }
        return false;
    }
    public bool TrySetMountWithRaycast(RaycastHit2D[] raycastHit2Ds)
    {
        RaycastHit2D MountableRarcast2DNow = new RaycastHit2D();
        bool isAMountSet = false;
        float distanceNow = 0;
        foreach(RaycastHit2D raycastHit2D in raycastHit2Ds)
        {
            if(raycastHit2D.transform.TryGetComponent( out IMountableSM mountableSM) && mountableSM.MountableMg.GetIsMountable())
            {
                if (isAMountSet == false)
                {
                    MountableRarcast2DNow = raycastHit2D;
                    distanceNow = raycastHit2D.distance;
                    isAMountSet = true;
                }
                else
                {
                    if(distanceNow >= raycastHit2D.distance)
                    {
                        MountableRarcast2DNow = raycastHit2D;
                        distanceNow = raycastHit2D.distance;
                    }
                }
            }
        }
        if (isAMountSet)
        {
            myMount = MountableRarcast2DNow.transform.GetComponent<IMountableSM>().MountableMg;           
        }
        return isAMountSet;
    }
    public void EnterMount()
    {
        stateMachineTransform.SetParent(myMount.GetMountPointTansform());
        if (transform.root.GetComponent<IBasicSM>().FaceDirectionMg.GetIsFaceRight())
        {
            stateMachineTransform.GetComponent<IBasicSM>().FaceDirectionMg.SetFaceRight();
        }
        else
        {
            stateMachineTransform.GetComponent<IBasicSM>().FaceDirectionMg.SetFaceLeft();
        }
        stateMachineTransform.position = myMount.GetMountPointTansform().position;
        SetIsMounting(true);
        myMount.SetIsMounted(true);
        myMount.SetMounter(transform.GetComponent<MountingAbilityManager>());
    }
    public void ExitMount()
    {
        SetIsMounting(false);
        stateMachineTransform.SetParent(null);
        myMount.SetIsMounted(false);
    }
}
