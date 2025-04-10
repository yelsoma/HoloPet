using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerManagerCenter 
{
    private static int layerNow = 0;
    private static int layerMax = 32767;
    public static event EventHandler OnResetLayer;
    
    public static int GetOrderLayer()
    {
        if (layerNow < layerMax)
        {
            layerNow++;
            return layerNow;
        }
        layerNow = -32767;
        OnResetLayer?.Invoke(typeof(LayerManagerCenter), EventArgs.Empty);
        return layerNow;
    }    
}
