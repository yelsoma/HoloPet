using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayerManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void ChangeLayer(int i)
    {
        if(spriteRenderer.sortingOrder != i)
        {
            spriteRenderer.sortingOrder = i;
        }      
    }
    public int GetLayer()
    {
       return spriteRenderer.sortingOrder;
    }
}
