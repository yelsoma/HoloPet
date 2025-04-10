using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoundary : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private float leftBounderyVectorX;
    private float rightBounderyVectorX;
    private float botBounderyVectorY;
    private float topBounderyVectorY;
    private Vector2 screenSize;
    private float taskBarHight;
    private void Awake()
    {
        SetBoudery();
    }    
    public void SetBoudery()
    {
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)), Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Screen.width, 0f)));
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)), Camera.main.ScreenToWorldPoint(new Vector2(0f, UnityEngine.Screen.height)));
        taskBarHight = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0f, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)), Camera.main.ScreenToWorldPoint(new Vector2(0f, UnityEngine.Screen.height)));
        leftBounderyVectorX = mainCamera.transform.position.x - (screenSize.x * 0.5f);
        rightBounderyVectorX = mainCamera.transform.position.x + (screenSize.x * 0.5f);
        botBounderyVectorY = mainCamera.transform.position.y - (screenSize.y * 0.5f) + taskBarHight;
        topBounderyVectorY = mainCamera.transform.position.y + (screenSize.y * 0.5f);
    }
    public float GetLeftBounderyVectorX()
    {
        return leftBounderyVectorX;
    }
    public float GetRightBounderyVectorX()
    {
        return rightBounderyVectorX;
    }
    public float GetBotBounderyVectorY()
    {
        return botBounderyVectorY;
    }
    public float GetTopBounderyVectorY()
    {
        return topBounderyVectorY;
    }
}
