using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    Transform selfTransform;
    private List<RaycastHit2D> raycastHitList = new List<RaycastHit2D>();
    private void Awake()
    {
        selfTransform = transform.root;
    }
    public bool TrySetRaycast(float raycastDistance, Vector2 dircection)
    {
        ClearHits();
        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position, dircection, raycastDistance);
        AddHitsToList(raycastHits);
        return IsSomethigHit();
    }
    public bool TrySetRaycastBothSide(float raycastDistance)
    {
        ClearHits();
        RaycastHit2D[] raycastHitsRight = Physics2D.RaycastAll(transform.position, Vector2.right, raycastDistance);
        AddHitsToList(raycastHitsRight);
        RaycastHit2D[] raycastHitsLeft = Physics2D.RaycastAll(transform.position, Vector2.left, raycastDistance);
        AddHitsToList(raycastHitsLeft);
        return IsSomethigHit();
    }
    private void AddHitsToList(RaycastHit2D[] raycastHits)
    {
        if (raycastHits != null)
        {
            for (int i = 0; i < raycastHits.Length; i++)
            {
                //check self                
                if (raycastHits[i].transform != selfTransform)
                {
                    raycastHitList.Add(raycastHits[i]);

                }
            }
        }
    }
    private bool IsSomethigHit()
    {
        if (raycastHitList.ToArray().Length <= 0)
        {
            return false;
        }
        return true;
    }
    public RaycastHit2D[] GetRaycastHits()
    {
        return raycastHitList.ToArray();
    }
    public void ClearHits()
    {
        raycastHitList.Clear();
    }
}
