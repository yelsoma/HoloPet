using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private float objectHight;
    [SerializeField] private float objectWidth;
    private float leftWallBoundery;
    private float rightWallBoundery;
    private float botWallBoundery;
    private float topWallBoundery;
    private Vector2 screenSize;
    private float taskBarHight;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Start()
    {
        SetBoudery();
    }
    private void LateUpdate()
    {
        UpdatBoudery();
    }

    // Boundery
    public void SetBoudery()
    {
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)), Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Screen.width, 0f)));
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)), Camera.main.ScreenToWorldPoint(new Vector2(0f, UnityEngine.Screen.height)));
        taskBarHight = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0f, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)), Camera.main.ScreenToWorldPoint(new Vector2(0f, UnityEngine.Screen.height)));
        leftWallBoundery = mainCamera.transform.position.x - (screenSize.x * 0.5f) + objectWidth;
        rightWallBoundery = mainCamera.transform.position.x + (screenSize.x * 0.5f) - objectWidth;
        botWallBoundery = mainCamera.transform.position.y - (screenSize.y * 0.5f) + taskBarHight + objectHight;
        topWallBoundery = mainCamera.transform.position.y + (screenSize.y * 0.5f) - objectHight;
    }
    private void UpdatBoudery()
    {
        if (CheckIsLeftBoundery())
        {
            transform.position = new Vector2(leftWallBoundery, transform.position.y);
        }
        if (CheckIsRightBoundery())
        {
            transform.position = new Vector2(rightWallBoundery, transform.position.y);
        }
        if (CheckIsBotBoundery())
        {
            transform.position = new Vector2(transform.position.x, botWallBoundery);
        }
        if (CheckIsTopBoundery())
        {
            transform.position = new Vector2(transform.position.x, topWallBoundery);
        }
    }
    public bool CheckIsLeftBoundery()
    {
        if (transform.position.x <= leftWallBoundery)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckIsRightBoundery()
    {
        if (transform.position.x >= rightWallBoundery)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckIsBotBoundery()
    {
        if (transform.position.y <= botWallBoundery)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckIsTopBoundery()
    {
        if (transform.position.y >= topWallBoundery)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
