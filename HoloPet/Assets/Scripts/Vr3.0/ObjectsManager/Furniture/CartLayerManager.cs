using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartLayerManager : MonoBehaviour, ILayerManager
{
    [SerializeField] private SpriteRenderer backSprite;
    [SerializeField] private SpriteRenderer frontSprite;

    private int layerNow;
    private void Start()
    {
        LayerCenter.OnResetLayer += LayerCenter_OnResetLayer;
    }
    public void ChangeLayerAll()
    {
        backSprite.sortingOrder = LayerCenter.LayerNowPlusOneAndGet();
        layerNow = backSprite.sortingOrder;
        frontSprite.sortingOrder = LayerCenter.LayerNowPlusOneAndGet();
    }

    public void ChangeLayerMain()
    {
        backSprite.sortingOrder = LayerCenter.LayerNowPlusOneAndGet();
        layerNow = backSprite.sortingOrder;
    }

    public void ChangeLayerTop()
    {
        frontSprite.sortingOrder = LayerCenter.LayerNowPlusOneAndGet();
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


