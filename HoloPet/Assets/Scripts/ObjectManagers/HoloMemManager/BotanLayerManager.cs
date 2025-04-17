using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotanLayerManager : MonoBehaviour , ILayerManager
{
    [SerializeField] SpriteLayerManager bodySpriteLM;
    [SerializeField] SpriteLayerManager handSpriteLM;
    [SerializeField] SpriteLayerManager handBackSpriteLM;
    public void SetLayerAll()
    {
        LayerCenter.AddToLayerCenter(bodySpriteLM);
        LayerCenter.AddToLayerCenter(handBackSpriteLM);
        LayerCenter.AddToLayerCenter(handSpriteLM);
    }
    public void ResetLayerAll()
    {
        //remove out from list
        LayerCenter.RemoveFromLayerCenter(bodySpriteLM);
        LayerCenter.RemoveFromLayerCenter(handBackSpriteLM);
        LayerCenter.RemoveFromLayerCenter(handSpriteLM);

        //add in to list again
        LayerCenter.AddToLayerCenter(bodySpriteLM);
        LayerCenter.AddToLayerCenter(handBackSpriteLM);
        LayerCenter.AddToLayerCenter(handSpriteLM);
    }
    public void ResetLayerBot()
    {
        //remove out from list
        LayerCenter.RemoveFromLayerCenter(bodySpriteLM);
        LayerCenter.RemoveFromLayerCenter(handBackSpriteLM);
        LayerCenter.RemoveFromLayerCenter(handSpriteLM);

        //add only bot part to list
        LayerCenter.AddToLayerCenter(bodySpriteLM);
        LayerCenter.AddToLayerCenter(handBackSpriteLM);
        LayerCenter.AddToLayerCenter(handSpriteLM);
    }
    public void ResetLayerTop()
    {
        //add top part to list
    }
    public int GetMainLayer()
    {
        return bodySpriteLM.GetLayer();
    }
}
