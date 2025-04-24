using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct SpriteLayerCenter 
{
    private static List<SpriteLayer> spriteLayerList = new List<SpriteLayer>();

    public static void AddNewLayers(Transform selfTransform)
    {
        spriteLayerList.AddRange(SpriteLayersInChildren(selfTransform));
        UpdateLayer();
    }
    public static void RemoveLayers(Transform selfTransform)
    {
        foreach (SpriteLayer spriteLayer in SpriteLayersInChildren(selfTransform))
        {
            spriteLayerList.Remove(spriteLayer);
        }
        UpdateLayer();
    }
    public static void PullRootLayersToTop(Transform selfTransform)
    {
        SpriteLayer[] selfSpriteLayers = SpriteLayersInChildren(selfTransform.root);
        foreach (SpriteLayer spriteLayer in selfSpriteLayers)
        {
            spriteLayerList.Remove(spriteLayer);
        }
        spriteLayerList.AddRange(selfSpriteLayers);
        UpdateLayer();
    }
    public static void InsertLayersToParent(Transform selfTransform)
    {
        SpriteLayer[] spriteLayersInChildren = SpriteLayersInChildren(selfTransform);
        foreach (SpriteLayer spriteLayer in spriteLayersInChildren)
        {
            spriteLayerList.Remove(spriteLayer);
        }
        int insertTargetInt = spriteLayerList.IndexOf(selfTransform.GetComponentInParent<SpriteLayer>()) + 1;
        spriteLayerList.InsertRange(insertTargetInt,spriteLayersInChildren);
        UpdateLayer();
    }
    public static void InsertLayersAfterTransform(Transform selfTransform ,Transform targetTransform)
    {
        SpriteLayer[] selfSpriteLayers = SpriteLayersInChildren(selfTransform);
        foreach (SpriteLayer spriteLayer in selfSpriteLayers)
        {
            spriteLayerList.Remove(spriteLayer);
        }
        SpriteLayer[] targetSpriteLayers = SpriteLayersInChildren(targetTransform);
        int insertTargetInt = spriteLayerList.IndexOf(targetSpriteLayers[targetSpriteLayers.Length -1]) + 1;
        spriteLayerList.InsertRange(insertTargetInt, selfSpriteLayers);
        UpdateLayer();
    }

    //pack method
    private static void UpdateLayer()
    {
        for (int i = 0; i < spriteLayerList.Count; i++)
        {
            spriteLayerList[i].ChangeLayer(i);
        }
    }   
    private static SpriteLayer[] SpriteLayersInChildren(Transform selfTransform)
    {
        return selfTransform.GetComponentsInChildren<SpriteLayer>();
    }
}
