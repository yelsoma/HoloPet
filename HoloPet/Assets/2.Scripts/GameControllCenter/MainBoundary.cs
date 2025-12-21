using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MainBoundary 
{
    private static Camera mainCamera;
    private static float leftBounderyVectorX;
    private static float rightBounderyVectorX;
    private static float botBounderyVectorY;
    private static float botBounderyWithOutTaskBarY;
    private static float topBounderyVectorY;
    private static Vector2 screenSize;
    private static float taskBarHight;
    
    public static void SetBoudery()
    {
        if (Screen.width <= 0 || Screen.height <= 0)
        {
            Debug.LogError("Screen.width <= 0 or Screen.height <= 0 in main bounday set");
            return;
        }
           

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("mainCamera == null in main bounday set");
            return;
        }
        screenSize.x = Vector2.Distance(mainCamera.ScreenToWorldPoint(new Vector2(0f, 0f)), mainCamera.ScreenToWorldPoint(new Vector2(UnityEngine.Screen.width, 0f)));
        screenSize.y = Vector2.Distance(mainCamera.ScreenToWorldPoint(new Vector2(0f, 0f)), mainCamera.ScreenToWorldPoint(new Vector2(0f, UnityEngine.Screen.height)));
        taskBarHight = Vector2.Distance(mainCamera.ScreenToWorldPoint(new Vector2(0f, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)), mainCamera.ScreenToWorldPoint(new Vector2(0f, UnityEngine.Screen.height)));
        leftBounderyVectorX = mainCamera.transform.position.x - (screenSize.x * 0.5f);
        rightBounderyVectorX = mainCamera.transform.position.x + (screenSize.x * 0.5f);
        botBounderyVectorY = mainCamera.transform.position.y - (screenSize.y * 0.5f) + taskBarHight;
        botBounderyWithOutTaskBarY = mainCamera.transform.position.y - (screenSize.y * 0.5f);
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
    public static float GetBottBotBounderyYWithOutTaskBar()
    {
        return botBounderyWithOutTaskBarY;
    }
}

