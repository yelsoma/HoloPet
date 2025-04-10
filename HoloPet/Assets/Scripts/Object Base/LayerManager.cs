using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LayerManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer mainSpriteRenderer;
    [SerializeField] SpriteRenderer topSpriteRenderer;
    private void Awake()
    {
        LayerManagerCenter.OnResetLayer += LayerManagerCenter_OnResetLayer;
    }

    private void LayerManagerCenter_OnResetLayer(object sender, EventArgs e)
    {
        mainSpriteRenderer.sortingOrder = 0;
    }

    public void ChangeAllLayer()
    {
        mainSpriteRenderer.sortingOrder = LayerManagerCenter.GetOrderLayer();
        if(topSpriteRenderer != null)
        {
            topSpriteRenderer.sortingOrder = LayerManagerCenter.GetOrderLayer();
        }
    }
    public void ChangeMainLayer()
    {          
        mainSpriteRenderer.sortingOrder = LayerManagerCenter.GetOrderLayer();
    }
    public void ChangeTopLayer()
    {
        if (topSpriteRenderer != null)
        {
            topSpriteRenderer.sortingOrder = LayerManagerCenter.GetOrderLayer();
        }
    }
    public int GetLayerNow()
    {
        return mainSpriteRenderer.sortingOrder;
    }

}
