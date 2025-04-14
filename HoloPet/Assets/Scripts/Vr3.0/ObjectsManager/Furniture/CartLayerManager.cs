using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartLayerManager : MonoBehaviour, ILayerManager
{
    [SerializeField] private SpriteRenderer backSprite;
    [SerializeField] private SpriteRenderer frontSprite;
    [SerializeField] private LayerCenter layerCenter;
    private int layerNow;
    private void Start()
    {
        layerCenter.OnResetLayer += LayerCenter_OnResetLayer;
    }
    public void ChangeLayerAll()
    {
        backSprite.sortingOrder = layerCenter.LayerNowPlusOneAndGet();
        layerNow = backSprite.sortingOrder;
        frontSprite.sortingOrder = layerCenter.LayerNowPlusOneAndGet();
    }

    public void ChangeLayerMain()
    {
        backSprite.sortingOrder = layerCenter.LayerNowPlusOneAndGet();
        layerNow = backSprite.sortingOrder;
    }

    public void ChangeLayerTop()
    {
        frontSprite.sortingOrder = layerCenter.LayerNowPlusOneAndGet();
    }

    public int GetLayerNow()
    {
        return layerNow;
    }

    //event
    private void LayerCenter_OnResetLayer(object sender, System.EventArgs e)
    {
        ChangeLayerAll();
    }
}


