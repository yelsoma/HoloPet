using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartLayerManager : MonoBehaviour , ILayerManager
{
    [SerializeField] SpriteLayerManager backSpriteLM;
    [SerializeField] SpriteLayerManager topSpriteLM;

    public void SetLayerAll()
    {
        LayerCenter.AddToLayerCenter(backSpriteLM);
        LayerCenter.AddToLayerCenter(topSpriteLM);
    }
    public void ResetLayerAll()
    {
        //remove out from list
        LayerCenter.RemoveFromLayerCenter(backSpriteLM);
        LayerCenter.RemoveFromLayerCenter(topSpriteLM);

        //add in to list again
        LayerCenter.AddToLayerCenter(backSpriteLM);
        LayerCenter.AddToLayerCenter(topSpriteLM);
    }
    public void ResetLayerBot()
    {
        //remove out from list
        LayerCenter.RemoveFromLayerCenter(backSpriteLM);
        LayerCenter.RemoveFromLayerCenter(topSpriteLM);

        //add only bot part to list
        LayerCenter.AddToLayerCenter(backSpriteLM);
    }
    public void ResetLayerTop()
    {
        //add top part to list
        LayerCenter.AddToLayerCenter(topSpriteLM);
    }
    public int GetMainLayer()
    {
        return topSpriteLM.GetLayer();
    }
}
