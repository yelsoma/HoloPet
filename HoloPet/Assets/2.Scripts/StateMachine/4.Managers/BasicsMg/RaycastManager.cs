using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    private StateMachineBase stateMachine;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
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
            if (hit.transform == stateMachine.transform || hit.transform.IsChildOf(stateMachine.transform))
                continue;

            filteredHits.Add(hit);
        }

        return filteredHits.ToArray();
    }
}
