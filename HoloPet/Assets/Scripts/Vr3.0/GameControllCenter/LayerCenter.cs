
using System;
using UnityEngine;

public class LayerCenter : MonoBehaviour
{   
    public event EventHandler OnResetLayer;   
    private int layerNow = -32768;
    public int LayerNowPlusOneAndGet()
    {
        layerNow = layerNow + 1;
        return layerNow;
    }   
    public void ResetEveryLayer()
    {
        OnResetLayer?.Invoke(this,EventArgs.Empty);
    }
}
