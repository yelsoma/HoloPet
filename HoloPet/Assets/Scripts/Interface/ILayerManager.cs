using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILayerManager 
{
    public void SetLayerAll();
    public void ResetLayerAll();
    public void ResetLayerBot();
    public void ResetLayerTop();
    public int GetMainLayer();
}
