
using System;
using System.Collections.Generic;
using UnityEngine;

public static class LayerCenter 
{    
    private static List<SpriteLayerManager> layerManagerList = new List<SpriteLayerManager>();

    public static void AddToLayerCenter(SpriteLayerManager layerManager)
    {
        layerManagerList.Add(layerManager);
    }
    public static void RemoveFromLayerCenter(SpriteLayerManager layerManager)
    {
        layerManagerList.Remove(layerManager);
    }
    public static void InsertToLayerCenter(int i, SpriteLayerManager layerManager)
    {
        layerManagerList.Insert(i, layerManager);
    }
    public static void ResetAllLayer()
    {
        for (int i = 0; i < layerManagerList.Count; i++)
        {
            layerManagerList[i].ChangeLayer(i);
        }
    }  
}
