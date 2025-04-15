
using System;
using UnityEngine;

public static class LayerCenter 
{   
    public static event EventHandler OnResetLayer;   
    private static int layerNow = -32768;
    public  static int LayerNowPlusOneAndGet()
    {
        layerNow = layerNow + 1;
        return layerNow;
    }   
    public static void ResetEveryLayer()
    {
        OnResetLayer?.Invoke(typeof(LayerManagerCenter), EventArgs.Empty);
    }
}
