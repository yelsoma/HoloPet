using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    [SerializeField] private float objectHight;
    [SerializeField] private float objectWidth;
    private Transform objectTransform;
    private float leftObjectBoundery;
    private float rightObjectBoundery;
    private float botObjectBoundery;
    private float topObjectBoundery;
    private void Awake()
    {
        objectTransform = transform.root;
    }
    private void Start()
    {
        SetToMainBoudery();
    }
    public void SetToMainBoudery()
    {
        leftObjectBoundery = MainBoundary.GetLeftBounderyVectorX() + objectWidth;
        rightObjectBoundery = MainBoundary.GetRightBounderyVectorX() - objectWidth;
        botObjectBoundery = MainBoundary.GetBotBounderyVectorY() + objectHight;
        topObjectBoundery = MainBoundary.GetTopBounderyVectorY() - objectHight;
    }
    public void CheckAllBouderyAndResetPos()
    {
        if (CheckIsLeftBounderyAndResetPos())
        {
            objectTransform.position = new Vector2(leftObjectBoundery, objectTransform.position.y);
        }
        if (CheckIsRightBounderyAndResetPos())
        {
            objectTransform.position = new Vector2(rightObjectBoundery, objectTransform.position.y);
        }
        if (CheckIsBotBounderyAndResetPos())
        {
            objectTransform.position = new Vector2(objectTransform.position.x, botObjectBoundery);
        }
        if (CheckIsTopBounderyAndResetPos())
        {
            objectTransform.position = new Vector2(objectTransform.position.x, topObjectBoundery);
        }
    }
    public bool CheckIsLeftBounderyAndResetPos()
    {
        if (objectTransform.position.x <= leftObjectBoundery)
        {
            objectTransform.position = new Vector2(leftObjectBoundery, objectTransform.position.y);
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckIsRightBounderyAndResetPos()
    {
        if (objectTransform.position.x >= rightObjectBoundery)
        {
            objectTransform.position = new Vector2(rightObjectBoundery, objectTransform.position.y);
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckIsBotBounderyAndResetPos()
    {
        if (objectTransform.position.y <= botObjectBoundery)
        {
            objectTransform.position = new Vector2(objectTransform.position.x, botObjectBoundery);
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckIsTopBounderyAndResetPos()
    {
        if (objectTransform.position.y >= topObjectBoundery)
        {
            objectTransform.position = new Vector2(objectTransform.position.x, topObjectBoundery);
            return true;
        }
        else
        {
            return false;
        }
    } 
    public void SetToBotBoundary()
    {
        objectTransform.position = new Vector2(objectTransform.position.x, botObjectBoundery);
    }
}
