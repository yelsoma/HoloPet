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
    [SerializeField] private LayerCenter layerCenter;
    private int layerNow;
    private void Start()
    {
        layerCenter.OnResetLayer += LayerCenter_OnResetLayer;
    }
    public void ChangeLayerAll()
    {
        mainSprite.sortingOrder = layerCenter.LayerNowPlusOneAndGet();
        layerNow = mainSprite.sortingOrder;
        handSprite.sortingOrder = layerCenter.LayerNowPlusOneAndGet();
        handBackSprit.sortingOrder = handSprite.sortingOrder;
        topSprite.sortingOrder = layerCenter.LayerNowPlusOneAndGet();
    }
    public void ChangeLayerTop()
    {
        handSprite.sortingOrder = layerCenter.LayerNowPlusOneAndGet();
        handBackSprit.sortingOrder = handSprite.sortingOrder;
        topSprite.sortingOrder = layerCenter.LayerNowPlusOneAndGet();
    }
    public void ChangeLayerMain()
    {
        mainSprite.sortingOrder = layerCenter.LayerNowPlusOneAndGet();
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
