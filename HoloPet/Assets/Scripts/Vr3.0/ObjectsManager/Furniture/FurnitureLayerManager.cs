using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureLayerManager : MonoBehaviour ,ILayerManager
{
    [SerializeField] private SpriteRenderer sprite;

    private int layerNow;
    private void Start()
    {
        LayerCenter.OnResetLayer += LayerCenter_OnResetLayer;
    }
    public void ChangeLayerAll()
    {
        sprite.sortingOrder = LayerCenter.LayerNowPlusOneAndGet();
        layerNow = sprite.sortingOrder;
    }

    public void ChangeLayerMain()
    {
        sprite.sortingOrder = LayerCenter.LayerNowPlusOneAndGet();
        layerNow = sprite.sortingOrder;
    }

    public void ChangeLayerTop()
    {
        //change nothing
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
