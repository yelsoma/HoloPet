using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeLayerManager : MonoBehaviour ,ILayerManager
{
    [Header("SpriteLayers")]
    [SerializeField] private SpriteLayer mainLayer;

   
    

    public int GetObjectMainLayer()
    {
        return mainLayer.GetSpriteLayer();
    }
}
