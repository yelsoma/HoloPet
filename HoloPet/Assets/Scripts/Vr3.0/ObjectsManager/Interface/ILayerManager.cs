using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILayerManager 
{
    public void ChangeLayerAll();
    public void ChangeLayerTop();
    public void ChangeLayerMain();
    public int GetLayerNow();
}
