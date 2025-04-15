using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoloMemLayerManager : MonoBehaviour, ILayerManager
{
    [SerializeField] private SpriteRenderer mainSprite;
    [SerializeField] private SpriteRenderer handSprite;
    [SerializeField] private SpriteRenderer handBackSprit;
    [SerializeField] private SpriteRenderer topSprite;
    private int layerNow;
    private void Start()
    {
        LayerCenter.OnResetLayer += LayerCenter_OnResetLayer;
    }
    public void ChangeLayerAll()
    {
        mainSprite.sortingOrder = LayerCenter.LayerNowPlusOneAndGet();
        layerNow = mainSprite.sortingOrder;
        handSprite.sortingOrder = LayerCenter.LayerNowPlusOneAndGet();
        handBackSprit.sortingOrder = handSprite.sortingOrder;
        topSprite.sortingOrder = LayerCenter.LayerNowPlusOneAndGet();
    }
    public void ChangeLayerTop()
    {
        handSprite.sortingOrder = LayerCenter.LayerNowPlusOneAndGet();
        handBackSprit.sortingOrder = handSprite.sortingOrder;
        topSprite.sortingOrder = LayerCenter.LayerNowPlusOneAndGet();
    }
    public void ChangeLayerMain()
    {
        mainSprite.sortingOrder = LayerCenter.LayerNowPlusOneAndGet();
        layerNow = mainSprite.sortingOrder;
    }
    public int GetLayerNow()
    {
        return layerNow;
    }
    //events
    private void LayerCenter_OnResetLayer(object sender, System.EventArgs e)
    {
        ChangeLayerAll();
    }
       
}
