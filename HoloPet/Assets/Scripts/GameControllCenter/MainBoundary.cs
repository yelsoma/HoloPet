using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainBoundary 
{
    [SerializeField] private static Camera mainCamera;
    private static float leftBounderyVectorX;
    private static float rightBounderyVectorX;
    private static float botBounderyVectorY;
    private static float topBounderyVectorY;
    private static Vector2 screenSize;
    private static float taskBarHight;
    
    public static void SetBoudery()
    {
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)), Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Screen.width, 0f)));
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)), Camera.main.ScreenToWorldPoint(new Vector2(0f, UnityEngine.Screen.height)));
        taskBarHight = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0f, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)), Camera.main.ScreenToWorldPoint(new Vector2(0f, UnityEngine.Screen.height)));
        leftBounderyVectorX = Camera.main.transform.position.x - (screenSize.x * 0.5f);
        rightBounderyVectorX = Camera.main.transform.position.x + (screenSize.x * 0.5f);
        botBounderyVectorY = Camera.main.transform.position.y - (screenSize.y * 0.5f) + taskBarHight;
        topBounderyVectorY = Camera.main.transform.position.y + (screenSize.y * 0.5f);
    }
    public static float GetLeftBounderyVectorX()
    {
        return leftBounderyVectorX;
    }
    public static float GetRightBounderyVectorX()
    {
        return rightBounderyVectorX;
    }
    public static float GetBotBounderyVectorY()
    {
        return botBounderyVectorY;
    }
    public static float GetTopBounderyVectorY()
    {
        return topBounderyVectorY;
    }
}

