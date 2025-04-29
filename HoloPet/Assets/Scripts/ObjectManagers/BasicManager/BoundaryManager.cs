using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    [SerializeField] private float objectHight;
    [SerializeField] private float objectWidth;
    private float leftObjectBoundery;
    private float rightObjectBoundery;
    private float botObjectBoundery;
    private float topObjectBoundery;
    private void Start()
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
            transform.position = new Vector2(leftObjectBoundery, transform.position.y);
        }
        if (CheckIsRightBounderyAndResetPos())
        {
            transform.position = new Vector2(rightObjectBoundery, transform.position.y);
        }
        if (CheckIsBotBounderyAndResetPos())
        {
            transform.position = new Vector2(transform.position.x, botObjectBoundery);
        }
        if (CheckIsTopBounderyAndResetPos())
        {
            transform.position = new Vector2(transform.position.x, topObjectBoundery);
        }
    }
    public bool CheckIsLeftBounderyAndResetPos()
    {
        if (transform.position.x <= leftObjectBoundery)
        {
            transform.position = new Vector2(leftObjectBoundery, transform.position.y);
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckIsRightBounderyAndResetPos()
    {
        if (transform.position.x >= rightObjectBoundery)
        {
            transform.position = new Vector2(rightObjectBoundery, transform.position.y);
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckIsBotBounderyAndResetPos()
    {
        if (transform.position.y <= botObjectBoundery)
        {
            transform.position = new Vector2(transform.position.x, botObjectBoundery);
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckIsTopBounderyAndResetPos()
    {
        if (transform.position.y >= topObjectBoundery)
        {
            transform.position = new Vector2(transform.position.x, topObjectBoundery);
            return true;
        }
        else
        {
            return false;
        }
    } 
    public void SetToBotBoundary()
    {
        transform.position = new Vector2(transform.position.x, botObjectBoundery);
    }
}
