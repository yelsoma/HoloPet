using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILayerManager 
{
    public void ChangeLayerAll();
    public void ChangeLaterTop();
    public int GetLayerNow();
}
