using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void ChangeLayer(int i)
    {
        spriteRenderer.sortingOrder = i;
    }
    public int GetSpriteLayer()
    {
        return spriteRenderer.sortingOrder;
    }
    public void SortingOrderPlus(int i)
    {
        spriteRenderer.sortingOrder += i;
    }
}
