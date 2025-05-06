using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainBoundary 
{
    private static Camera mainCamera = Camera.main;
    private static float leftBounderyVectorX;
    private static float rightBounderyVectorX;
    private static float botBounderyVectorY;
    private static float topBounderyVectorY;
    private static Vector2 screenSize;
    private static float taskBarHight;
    
    public static void SetBoudery()
    {
        screenSize.x = Vector2.Distance(mainCamera.ScreenToWorldPoint(new Vector2(0f, 0f)), mainCamera.ScreenToWorldPoint(new Vector2(UnityEngine.Screen.width, 0f)));
        screenSize.y = Vector2.Distance(mainCamera.ScreenToWorldPoint(new Vector2(0f, 0f)), mainCamera.ScreenToWorldPoint(new Vector2(0f, UnityEngine.Screen.height)));
        taskBarHight = Vector2.Distance(mainCamera.ScreenToWorldPoint(new Vector2(0f, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)), mainCamera.ScreenToWorldPoint(new Vector2(0f, UnityEngine.Screen.height)));
        leftBounderyVectorX = mainCamera.transform.position.x - (screenSize.x * 0.5f);
        rightBounderyVectorX = mainCamera.transform.position.x + (screenSize.x * 0.5f);
        botBounderyVectorY = mainCamera.transform.position.y - (screenSize.y * 0.5f) + taskBarHight;
        topBounderyVectorY = mainCamera.transform.position.y + (screenSize.y * 0.5f);
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

