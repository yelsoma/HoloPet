using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    Transform rootTransform;
    private StateMachineBase stateMachine;

    private void Awake()
    {
        rootTransform = transform.root;
        stateMachine = GetComponentInParent<StateMachineBase>();
    }

    public RaycastHit2D GetFirstHit(Vector2 direction, float raycastDistance, LayerMask mask)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            stateMachine.transform.position,
            direction,
            raycastDistance,
            mask
        );
        if (hit.collider != null)
        {
            if (hit.transform == rootTransform || hit.transform.IsChildOf(rootTransform))
            {
                return default; // return an "empty" RaycastHit2D
            }
        }

        return hit;
    }

    public RaycastHit2D[] GetAllHits(Vector2 direction, float raycastDistance, LayerMask mask = default)
    {
        if (mask == default)
            mask = Physics2D.DefaultRaycastLayers;

        RaycastHit2D[] allHits = Physics2D.RaycastAll(
            stateMachine.transform.position,
            direction,
            raycastDistance,
            mask
        );

        List<RaycastHit2D> filteredHits = new List<RaycastHit2D>();
        foreach (var hit in allHits)
        {
            if (hit.transform == null) continue;
            if (hit.transform == rootTransform || hit.transform.IsChildOf(rootTransform))
                continue;

            filteredHits.Add(hit);
        }

        return filteredHits.ToArray();
    }
}
