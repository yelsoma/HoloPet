
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    [SerializeField] Transform selfTransform;
    private List<RaycastHit2D> mountableList = new List<RaycastHit2D>();
    private List<RaycastHit2D> interacabletList = new List<RaycastHit2D>();
    private List<RaycastHit2D> forceInteracabletList = new List<RaycastHit2D>();
    private List<RaycastHit2D> raycastHitList = new List<RaycastHit2D>();

    enum raycastType
    {
        mountable,
        interactable
    }

    public bool TrySetRayCastList(Vector2 direction, float raycastDistance)
    {
        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, direction, raycastDistance);
        if (raycastHits != null)
        {
            for (int i = 0; i < raycastHits.Length; i++)
            {
                //check self
                if (raycastHits[i].transform.TryGetComponent(out ObjectData hitData) == true && raycastHits[i].transform != selfTransform)
                {
                    raycastHitList.Add(raycastHits[i]);
                }
            }
            if(raycastHitList != null)
            {
                return true;
            }           
        }
        return false;
    }
    public void ClearRayCast()
    {
        raycastHitList.Clear();
    }
    public RaycastHit2D[] GetRayCastList()
    {
        return raycastHitList.ToArray();
    }
    public void SetRaycastHits(float raycastDistance, Vector2 dircection)
    {
        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, dircection, raycastDistance);
        if (raycastHits != null)
        {           
            for (int i = 0; i < raycastHits.Length; i++)
            {
                //check self
                if (raycastHits[i].transform.TryGetComponent(out ObjectData hitData) == true && raycastHits[i].transform != selfTransform)
                {
                    
                    //mountable
                    if (hitData.GetIsMountable() == true && hitData.GetIsMounted() == false && hitData.GetisMountableState() == true)
                    {
                        mountableList.Add(raycastHits[i]);

                    }
                    //Interactable
                    if (hitData.GetIsInteractable() == true && hitData.GetIsInteractableState() == true)
                    {
                        interacabletList.Add(raycastHits[i]);
                    }                   
                }               
            }           
        }        
    }
    public RaycastHit2D[] GetMoutableArray()
    {
        return mountableList.ToArray();
    }
    public void ClearLists()
    {
        mountableList.Clear();
        interacabletList.Clear();
        forceInteracabletList.Clear();
    }
    public RaycastHit2D[] GetInteractableArray()
    {
        return interacabletList.ToArray();
    }    
    public void SetForceInteractRaycastHits(float raycastDistance, Vector2 dircection)
    {
        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, dircection, raycastDistance);
        if (raycastHits != null)
        {
            for (int i = 0; i < raycastHits.Length; i++)
            {
                //check self
                if (raycastHits[i].transform.TryGetComponent(out ObjectData hitData) == true && raycastHits[i].transform != selfTransform)
                {                    
                    //Interactable
                    if ( hitData.GetIsInteractableState() == true)
                    {
                        forceInteracabletList.Add(raycastHits[i]);
                    }
                }
            }
        }
    }
    public bool GetIsThereForceInteract()
    {
        if (forceInteracabletList.Count > 0)
        {
            return true;
        }
        return false;
    }
    public RaycastHit2D GetForceInteract()
    {
        return forceInteracabletList[0];
    }
}
