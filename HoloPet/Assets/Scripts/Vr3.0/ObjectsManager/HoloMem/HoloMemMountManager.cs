using UnityEngine;

public class HoloMemMountManager : MonoBehaviour, IMountable, IMountingAbility
{
    // mounting ability
    private bool isMounting;
    private IMountable myMount;

    // moutable
    private bool isMountableState = true;
    private bool isMounted = false;
    private IMountingAbility myMounter;
    [SerializeField] Transform mountingPoint;
   
    // mounting ability
    public bool GetIsMounting()
    {
        return isMounting;
    }
    public void SetIsMounting(bool isMounting)
    {
        this.isMounting = isMounting;
    }   
    public IMountable GetMount()
    {
        return myMount;
    }
    public void SetMount(IMountable mount)
    {
        myMount = mount;
    }
    public bool TrySetMountWithRaycast(RaycastHit2D[] raycastHit2Ds)
    {
        RaycastHit2D selectedMountRarcast2D = new RaycastHit2D();
        bool isAMountSet = false;
        float distanceNow = 0;

        //Try set mount with Raycast Hit Array
        for (int i = 0; i < raycastHit2Ds.Length; i++)
        {
            // check is transform Mountable
            if (CheckRaycastHitMountable(raycastHit2Ds[i]))
            {
                //compar distance , check is mount set
                if (isAMountSet == false)
                {
                    //mount  is not set , set the mount
                    selectedMountRarcast2D = raycastHit2Ds[i];
                    distanceNow = raycastHit2Ds[i].distance;
                    isAMountSet = true;
                }
                else
                {
                    //mount set , check is distance closer than mount now
                    if (distanceNow > raycastHit2Ds[i].distance)
                    {
                        selectedMountRarcast2D = raycastHit2Ds[i];
                        distanceNow = raycastHit2Ds[i].distance;
                    }
                }
            }
        }
        if (isAMountSet)
        {
            myMount = selectedMountRarcast2D.transform.GetComponent<IMountable>();
        }
        return isAMountSet;
    }
    private bool CheckRaycastHitMountable(RaycastHit2D raycastHit2D)
    {
        if (raycastHit2D.transform.TryGetComponent(out IMountable mountableObject))
        {
            if (mountableObject.GetIsMountable())
            {
                return true;
            }
        }
        return false;
    }
    public void EnterMount()
    {
        transform.SetParent(myMount.GetTransform());
        transform.position = myMount.GetMountingPoint();
        if (myMount.GetMountingDirIsRight())
        {
            transform.GetComponent<FaceDirectionVr2>().SetFaceRight();
        }
        else
        {
            transform.GetComponent<FaceDirectionVr2>().SetFaceLeft();
        }
        myMount.SetIsMounted(true);
    }
    public void ExitMount()
    {
        transform.SetParent(null);
        myMount.SetIsMounted(false);
    }

    // moutable
    public bool GetIsMounted()
    {
        return isMounted;
    }
    public void SetIsMounted(bool isMounted)
    {
        this.isMounted = isMounted;
    }
    public IMountingAbility GetMounter()
    {
        return myMounter;
    }   
    public void SetMounter(IMountingAbility mounter)
    {
        myMounter = mounter;
    }
    public void SetIsMountableState(bool isMountableState)
    {
        this.isMountableState = isMountableState;
    }
    public bool GetIsMountableState()
    {
        return isMountableState;
    }
    public bool GetIsMountable()
    {
        if(isMountableState == true && isMounted == false)
        {
            return true;
        }
        return false;
    }
    public Vector2 GetMountingPoint()
    {
        return mountingPoint.position;
    }
    public Transform GetTransform()
    {
        return transform;
    }
    public bool GetMountingDirIsRight()
    {
        return transform.GetComponent<FaceDirectionVr2>().GetIsFaceRight();
    }   
}
