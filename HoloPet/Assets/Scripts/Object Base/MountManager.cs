using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MountManager : MonoBehaviour
{
    [SerializeField] private ObjectData data;
    [SerializeField] private MountManager self;
    private MountManager mount;
    private MountManager mounter;
    [SerializeField] private Transform seatPos;
    [SerializeField] private LayerManager layerManager;

    // this methed trys to set a mount with RaycastHits and retruns a bool if Mount is set
    public void SetTargetToMount(MountManager targetMountManager)
    {
        mount = targetMountManager;
    }
    public void SettTargetToMounter(MountManager targetMountManager)
    {
        mounter = targetMountManager;
    }
    public void SetMount(RaycastHit2D[] mountableArray)
    {
        if(mountableArray != null)
        {
            RaycastHit2D mountHit2D = mountableArray[0];
            float distance = Vector2.Distance(mountableArray[0].transform.position, transform.position);           
            for (int i = 1; i < mountableArray.Length; i++)
            {
                float iDistance = Vector2.Distance(mountableArray[i].transform.position, transform.position);
                if (distance >= iDistance)
                {
                    distance = iDistance;
                    mountHit2D = mountableArray[i];
                }                                                         
            }
            mount = mountHit2D.transform.GetComponent<MountManager>();            
        }      
    }

    //enter and exit setUp
    public void EnterMounting()
    {
        mount.SetMounter(self);
        data.SetIsMounting(true);
        mount.data.SetIsMounted(true);
    }
    public void ExitMounting()
    {
        mount.ClearMounter();
        data.SetIsMounting(false);
        mount.data.SetIsMounted(false);
    }
    public void SetMounter(MountManager mounter)
    {
        this.mounter = mounter;
    }
    public void ClearMounter()
    {
        mounter = null;
    }
   
    //mounting methods
    public Vector2 GetMountSeat()
    {
        return mount.seatPos.position;
    }
    public bool GetMountIsMountable()
    {
        return mount.data.GetisMountableState();
    }      
    public void FollowMountFaceDir()
    {
        data.SetIsFaceRight(mount.data.GetIsFaceRight());         
    }  
    
    //layer Change
    public void LayerModifyDown()
    {
        if (data.GetIsMounting())
        {
            // keep find bottom 
            mount.LayerModifyDown();          
        }
        else
        {
            // is bottom
            LayerModifyUp();
        }                    
    }      
    public void LayerModifyUp()
    {
        layerManager.ChangeMainLayer();
        if (data.GetIsMounting())
        {
            mount.layerManager.ChangeTopLayer();
        }
        if (data.GetIsMounted() == false)
        {
            layerManager.ChangeTopLayer();
        }  
        if(data.GetIsMounted())
        {
            mounter.LayerModifyUp();
        }
    }   
    public string GetMounterName()
    {
        return mounter.data.GetName();
    }
}
