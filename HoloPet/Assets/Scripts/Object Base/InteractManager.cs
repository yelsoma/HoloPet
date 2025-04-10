using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    RaycastHit2D targetHit2D;
    private InteractManager interactTarget;
    private InteractManager interacter;
    [SerializeField] private RaycastManager raycastManager;
    [SerializeField] private ObjectData data;
    private float tete = 0.7f;
    private float teteToMuch = 0.65f;
    public event EventHandler OnInteract;
    public event EventHandler OnTargetHappy;
    public event EventHandler OnTargetKnockUp;
    public bool SetTarget( float distance)
    {
        //setRightArray
        raycastManager.SetRaycastHits(distance, Vector2.right);
        RaycastHit2D[] arrayRight = raycastManager.GetInteractableArray();
        raycastManager.ClearLists();
        //setLeftArray
        raycastManager.SetRaycastHits(distance, Vector2.left);
        RaycastHit2D[] arrayLeft = raycastManager.GetInteractableArray();
        raycastManager.ClearLists();
        int both = arrayRight.Length + arrayLeft.Length;
        if (both > 0)
        {
            RaycastHit2D[] arrayAll = new RaycastHit2D[both];
            for ( int i = 0; i < arrayRight.Length ; i++)
            {
                arrayAll[i] = arrayRight[i];
            }
            for( int i = 0; i < arrayLeft.Length; i++)
            {
                arrayAll[i + arrayRight.Length] = arrayLeft[i];
            }
            int random = UnityEngine.Random.Range(0, arrayAll.Length);
            targetHit2D = arrayAll[random];
            interactTarget = targetHit2D.transform.GetComponent<InteractManager>();

            return true;
        }
        return false;
    } 
    public void SetTarget(RaycastHit2D raycastHit2D)
    {
        interactTarget = raycastHit2D.transform.GetComponent<InteractManager>();
    }
    public RaycastHit2D GetTarget()
    {
        return targetHit2D;
    }       
    public bool GetTargetIsInteractableState()
    {
        if(interactTarget.data.GetIsInteractableState())
        {
            return true;
        }
        return false;
    } 
    public bool GetTargetIsInteracting()
    {
        if (interactTarget.data.GetIsInteracting())
        {
            return true;
        }
        return false;
    }
    public void SetTagetInteracted()
    {
        interactTarget.interacter = this;
        interactTarget.OnInteract?.Invoke(this, EventArgs.Empty);
    }   
    public Vector2 GetSelfVector2()
    {
        return transform.position;
    }
    public bool GetIsTargetRight()
    {
        if(interactTarget.GetSelfVector2().x - transform.position.x > 0)
        {
            return true;
        }
        return false;
    }
    public bool GetIsTargetClose()
    {
        if(Vector2.Distance(interactTarget.GetSelfVector2(), transform.position) < tete)
        {
            return true;
        }
        return false;
    }
    public bool GetIsTargetTooClose()
    {
        if (Vector2.Distance(interactTarget.GetSelfVector2(), transform.position) < teteToMuch)
        {
            return true;
        }
        return false;
    }
    public bool GetIsInteracterRight()
    {
        if (interacter.GetSelfVector2().x - transform.position.x > 0)
        {
            return true;
        }
        return false;
    }
    public void SetTargetHappy()
    {
        interactTarget.OnTargetHappy?.Invoke(this, EventArgs.Empty);
    }
    public void SetTargetKnockUp()
    {
        interactTarget.OnTargetKnockUp?.Invoke(this, EventArgs.Empty);
    }
}
