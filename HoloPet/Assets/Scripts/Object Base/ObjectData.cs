using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    [SerializeField] private string objectName;

    //mount
    [SerializeField] private bool isMountable;
    private bool isMounted;
    private bool isMounting;
    private bool isMountableState;

    //face
    private bool isFaceRight;
    public event EventHandler OnChangeFaceDirection;

    //Hp
    [SerializeField] private bool haveHp;
    [SerializeField] private int hpMax;
    private int hpNow;

    //Interact
    [SerializeField] private bool isInteractable;
    private bool isInteracting;
    private bool isInteractableState;



    public string GetName()
    {
        return objectName;
    }

    //mount
    
    public void SetIsMounted( bool isMounted)
    {
        this.isMounted = isMounted;
    }
    public void SetIsMounting( bool isMounting)
    {
        this.isMounting = isMounting;
    }
    public void SetIsMountableState( bool isMountableState)
    {
        this.isMountableState = isMountableState;
    }
    public bool GetIsMountable()
    {
        return isMountable;
    }
    public bool GetIsMounted()
    {
        return isMounted;
    }
    public bool GetIsMounting()
    {
        return isMounting;
    }
    public bool GetisMountableState()
    {
        return isMountableState;
    }

    //face dir

    public void SetIsFaceRight( bool isFaceRight)
    {
        if(this.isFaceRight != isFaceRight)
        {
            this.isFaceRight = isFaceRight;
            OnChangeFaceDirection?.Invoke(this, EventArgs.Empty);
        }       
    }
    public bool GetIsFaceRight()
    {
        return isFaceRight;
    }

    //hp
    public bool GetHaveHp()
    {
        return haveHp;
    }
    public void SetHpToMax()
    {
        hpNow = hpMax;
    }
    public void HpModify(int plusHp)
    {
        hpNow += plusHp;
    }
    public int GetHpNow()
    {
        return hpNow;
    }
    //interact   
    public void SetIsInteracting(bool isInteracting)
    {
        this.isInteracting = isInteracting;
    }
    public void SetIsInteractableState(bool isInteractableState)
    {
        this.isInteractableState = isInteractableState;
    }
    public bool GetIsInteractable()
    {
        return isInteractable;
    }
    public bool GetIsInteracting()
    {
        return isInteracting;
    } 
    public bool GetIsInteractableState()
    {
        return isInteractableState;
    }
}
