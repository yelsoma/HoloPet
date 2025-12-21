using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static UnityEngine.UI.Image;

public class AttackAbilityManager : MonoBehaviour
{
    private RaycastManager raycastManager;
    private StateMachineBase stateMachine;
    private LayerMask stateMachineMask;
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachine found in parent.");

        IBasicMod iBasicMod = stateMachine.transform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            raycastManager = iBasicMod.BasicMod.RaycastMg;

        int layer = LayerMask.NameToLayer("StateMachine");
        stateMachineMask = 1 << layer;
    }

    public AttackableManager TryGetTargetHorizantal(ObjectGangEnum gangFilter)
    {
        Vector2 start = stateMachine.transform.position;
        float rayDist = 99;

        // Get all hits in both directions
        var leftHits = raycastManager.GetAllHits(Vector2.left, rayDist, stateMachineMask);
        var rightHits = raycastManager.GetAllHits(Vector2.right, rayDist, stateMachineMask);

        // Combine them
        var allHits = leftHits.Concat(rightHits);

        AttackableMod closestAtk = null;
        float closestDist = float.MaxValue;

        foreach (var hit in allHits)
        {
            var atk = ValidateAttackable(hit, gangFilter);
            if (atk == null)
                continue;

            if (hit.distance < closestDist)
            {
                closestDist = hit.distance;
                closestAtk = atk;
            }
        }

        return closestAtk?.AttackableMg;
    }

    public AttackableManager TryGetAttackableFront(ObjectGangEnum gangFilter, float checkDistance, Vector2 dir)
    {
        var hits = raycastManager.GetAllHits(dir, checkDistance, stateMachineMask);

        AttackableMod closestAtk = null;
        float closestDist = float.MaxValue;

        foreach (var hit in hits)
        {
            var atk = ValidateAttackable(hit, gangFilter);
            if (atk == null)
                continue;

            if (hit.distance < closestDist)
            {
                closestDist = hit.distance;
                closestAtk = atk;
            }
        }

        return closestAtk?.AttackableMg;
    }


    //helper
    private AttackableMod ValidateAttackable(RaycastHit2D hit, ObjectGangEnum gangFilter)
    {
        if (hit.collider == null)
            return null;

        // Must have IAttackableMod
        if (!hit.transform.TryGetComponent<IAttackableMod>(out var iAtk))
            return null;

        var atkMod = iAtk.AttackableMod;
        var attackableMg = atkMod.AttackableMg;

        // Must be in correct gang
        if (!hit.transform.TryGetComponent<IBasicMod>(out var ibasic))
            return null;

        if (ibasic.BasicMod.ObjectDefinition.ObjectGangEnum != gangFilter)
            return null;

        // Must be currently attackable
        if (!attackableMg.GetIsAttackable())
            return null;

        return atkMod;
    }
}
