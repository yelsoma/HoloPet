using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static UnityEngine.UI.Image;

public class AttackAbilityManager : MonoBehaviour
{
    private RaycastManager raycastManager;
    private AttackableManager targetAttackable;
    private StateMachineBase stateMachine;
    private float targetDistance;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if(stateMachine == null)
        {
            Debug.LogError(transform.root.name + " no  StateMachine in parenet");
        }
        IBasicSM basicSM = stateMachine.transform.GetComponent<IBasicSM>();
        if( basicSM == null)
        {
            Debug.LogError(transform.root.name + " no IBasicSM in parenet");
        }
        raycastManager = basicSM.RaycastMg;
    }

    public bool TrySetAttackableAll(Vector2 direction, float distance, LayerMask mask = default, ObjectGangEnum? gangFilter = null)
    {
        if (mask == default)
            mask = Physics2D.DefaultRaycastLayers;

        // Get all hits along the given direction
        RaycastHit2D[] hits = raycastManager.GetAllHits(direction, distance, mask);

        if (hits.Length == 0)
            return false;

        // Find the closest valid attackable
        var closest = hits
            .Select(h => new { hit = h, atk = GetValidAttackable(h, gangFilter) })
            .Where(x => x.atk != null)
            .OrderBy(x => x.hit.distance)
            .FirstOrDefault();

        if (closest == null)
            return false;

        targetDistance = closest.hit.distance;
        targetAttackable = closest.atk.AttackableMg;
        return true;
    }

    //closest xy 2 differant way first with start point second dont need start point
    public bool TrySetClosestAttackable(Vector2 startPoint, Vector2 direction, float distance, LayerMask mask = default,ObjectGangEnum? gangFilter = null)
    {
        return TrySetClosestAttackableFromHits(
            new[] { raycastManager.GetFirstHit(startPoint, direction, distance, mask) },
            gangFilter
        );
    }
    public bool TrySetClosestAttackable(Vector2 direction, float distance, LayerMask mask = default, ObjectGangEnum? gangFilter = null)
    {
        Vector2 startPoint = stateMachine.transform.position;
        return TrySetClosestAttackable(startPoint, direction, distance, mask, gangFilter);
    }

    //closest Horizontal
    public bool TrySetClosestAttackableHorizontal( float yAdjust, float distance, LayerMask mask = default, ObjectGangEnum? gangFilter = null)
    {
        Vector2 startPoint = new Vector2(
            stateMachine.transform.position.x,
            stateMachine.transform.position.y + yAdjust
        );

        if (mask == default)
            mask = Physics2D.DefaultRaycastLayers;

        return TrySetClosestAttackableFromHits(
            new[]
            {
            raycastManager.GetFirstHit(startPoint, Vector2.left, distance, mask),
            raycastManager.GetFirstHit(startPoint, Vector2.right, distance, mask)
            },
            gangFilter
        );
    }
    public bool TrySetClosestAttackableHorizontal(float distance, LayerMask mask = default, ObjectGangEnum? gangFilter = null)
    {
        return TrySetClosestAttackableHorizontal(0f, distance, mask, gangFilter);
    }

    //Colsest Vertical
    public bool TrySetClosestAttackableVertical(float xAdjust, float distance, LayerMask mask = default, ObjectGangEnum? gangFilter = null)
    {
        Vector2 startPoint = new Vector2(
            stateMachine.transform.position.x + xAdjust,
            stateMachine.transform.position.y
        );

        if (mask == default)
            mask = Physics2D.DefaultRaycastLayers;

        return TrySetClosestAttackableFromHits(
            new[]
            {
            raycastManager.GetFirstHit(startPoint, Vector2.up, distance, mask),
            raycastManager.GetFirstHit(startPoint, Vector2.down, distance, mask)
            },
            gangFilter
        );
    }
    public bool TrySetClosestAttackableVertical( float distance, LayerMask mask = default, ObjectGangEnum? gangFilter = null)
    {
        return TrySetClosestAttackableVertical(0f, distance, mask, gangFilter);
    }

    public AttackableManager GetTarget() => targetAttackable;

    // ? Shared helper
    private bool TrySetClosestAttackableFromHits(IEnumerable<RaycastHit2D> hits, ObjectGangEnum? gangFilter)
    {
        var closest = hits
            .Select(h => new { hit = h, atk = GetValidAttackable(h, gangFilter) })
            .Where(x => x.atk != null)
            .OrderBy(x => x.hit.distance)
            .FirstOrDefault();

        if (closest == null)
            return false;

        targetDistance = closest.hit.distance;
        targetAttackable = closest.atk.AttackableMg;
        return true;
    }
    private IAttackableSM GetValidAttackable(RaycastHit2D hit, ObjectGangEnum? gangFilter)
    {
        if (hit.collider == null) return null;

        IAttackableSM attackable = hit.transform.GetComponent<IAttackableSM>();
        IBasicSM basicSM = hit.transform.GetComponent<IBasicSM>();

        if (attackable != null && basicSM != null &&
            (!gangFilter.HasValue || basicSM.BaseDataMg.GetObjectGang() == gangFilter.Value))
        {
            return attackable;
        }
        return null;
    }
    public bool GetIsTargetRight()
    {
        if(targetAttackable == null)
        {
            Debug.LogError(transform.root.name + " no target Attackable");
        }
        if (targetAttackable.GetStateMachine().transform.position.x > stateMachine.transform.position.x )
        {
            return true;
        }
        return false;
    }
    public float GetTargetDistance()
    {
        return targetDistance;
    }
    public bool GetIsTargetAttackableSet()
    {
        if(targetAttackable != null)
        {
            return true;
        }
        return false;
    }
}
