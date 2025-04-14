using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureMountManager : MonoBehaviour, IMountable
{
    private bool isMountableState = true;
    private bool isMounted = false;
    private IMountingAbility myMounter;
    [SerializeField] Transform mountingPoint;

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

    public IMountingAbility GetMounter()
    {
        return myMounter;
    }

    public Vector2 GetMountingPoint()
    {
        return mountingPoint.position;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void LayerChainUpStart()
    {
        if (isMounted)
        {
            //change my main sprite , my mounter will change my top sprite
            transform.GetComponent<ILayerManager>().ChangeLayerMain();
            //tell my mounter to layerChainUp
            myMounter.LayerChainUp();
        }
        else
        {
            transform.GetComponent<ILayerManager>().ChangeLayerAll();
        }
    }

    public void SetIsMounted(bool isMounted)
    {
        this.isMounted = isMounted;
    }

    public void SetMounter(IMountingAbility mounter)
    {
        myMounter = mounter;
    }
    public void SetIsMountableState(bool isMountableState)
    {
        this.isMountableState = isMountableState;
    }
}
