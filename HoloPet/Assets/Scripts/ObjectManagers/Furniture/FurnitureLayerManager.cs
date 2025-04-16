using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureLayerManager : MonoBehaviour ,ILayerManager
{
    [SerializeField] SpriteLayerManager spriteLM;

    public void SetLayerAll()
    {
        LayerCenter.AddToLayerCenter(spriteLM);
    }
    public void ResetLayerAll()
    {
        //remove out from list
        LayerCenter.RemoveFromLayerCenter(spriteLM);

        //add in to list again
        LayerCenter.AddToLayerCenter(spriteLM);
    }
    public void ResetLayerBot()
    {
        //remove out from list
        LayerCenter.RemoveFromLayerCenter(spriteLM);

        //add only bot part to list
        LayerCenter.AddToLayerCenter(spriteLM);
    }
    public void ResetLayerTop()
    {
        //add top part to list

    }
    public int GetMainLayer()
    {
        return spriteLM.GetLayer();
    }
}
