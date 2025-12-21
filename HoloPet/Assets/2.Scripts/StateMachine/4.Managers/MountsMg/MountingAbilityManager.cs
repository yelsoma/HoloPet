using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MountingAbilityManager : MonoBehaviour
{
    private bool isMounting;
    private MountableManager myMount;
    private Transform stateMachineTransform;
    private RaycastManager raycastManager;
    private BasicMod basicMod;

    private void Awake()
    {
        StateMachineBase stateMachineBase = GetComponentInParent<StateMachineBase>();
        if (stateMachineBase == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }
        stateMachineTransform = stateMachineBase.transform;

        IBasicMod iBasicMod = stateMachineTransform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            basicMod = iBasicMod.BasicMod;

        raycastManager = basicMod.RaycastMg;
    }

    public bool GetIsMounting()
    {
        return isMounting;
    }
    public void SetIsMounting(bool isMounting)
    {
        this.isMounting = isMounting;
    }
    public MountableManager GetMount()
    {
        return myMount;
    }
    public bool TrySetMount(MountableManager mount)
    {
        if (mount.GetIsMountable())
        {
            myMount = mount;
            return true;
        }
        return false;
    }
    public bool TrySetMountWithRaycast(Vector2 direction, float distance)
    {
        RaycastHit2D[] hits = raycastManager.GetAllHits(direction, distance);
        if (hits.Length == 0)
        {
            return false;
        }
        IMountableMod closestMountable = null;
        float closestDistance = float.MaxValue;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.TryGetComponent<IMountableMod>(out var iMountableMod) &&
                iMountableMod.MountableMod.MountableMg.GetIsMountable())
            {
                if (hit.distance < closestDistance)
                {
                    closestDistance = hit.distance;
                    closestMountable = iMountableMod;
                }
            }
        }

        if (closestMountable != null)
        {
            myMount = closestMountable.MountableMod.MountableMg;
            return true;
        }

        return false;
    }
    public bool TrySetMountWithRaycast( Vector2 direction,float distance, ObjectGangEnum excludeGang)
    {
        RaycastHit2D[] hits = raycastManager.GetAllHits(direction, distance);
        if (hits.Length == 0)
            return false;

        IMountableMod closestMountable = null;
        float closestDistance = float.MaxValue;
        foreach (RaycastHit2D hit in hits)
        {
            Transform t = hit.transform;

            if (t.TryGetComponent<IBasicMod>(out var ibasicMod) &&
                ibasicMod.BasicMod.ObjectDefinition.ObjectGangEnum == excludeGang)
                continue;

            if (!t.TryGetComponent<IMountableMod>(out var iMountableMod))
                continue;

            if (!iMountableMod.MountableMod.MountableMg.GetIsMountable())
                continue;

            if (hit.distance < closestDistance)
            {
                closestDistance = hit.distance;
                closestMountable = iMountableMod;
            }
        }

        if (closestMountable != null)
        {
            myMount = closestMountable.MountableMod.MountableMg;
            return true;
        }

        return false;
    }
    public void EnterMount()
    {
        stateMachineTransform.SetParent(myMount.GetMountPointTansform());
        if (myMount.GetComponentInParent<IBasicMod>().BasicMod.FaceDirectionMg.GetIsFaceRight())
        {
            stateMachineTransform.GetComponent<IBasicMod>().BasicMod.FaceDirectionMg.SetFaceRight();
        }
        else
        {
            stateMachineTransform.GetComponent<IBasicMod>().BasicMod.FaceDirectionMg.SetFaceLeft();
        }
        stateMachineTransform.localPosition = Vector3.zero;
        SetIsMounting(true);
        myMount.SetIsMounted(true);
        myMount.SetMounter(transform.GetComponent<MountingAbilityManager>());
        myMount.TriggerMountedChange();
    }
    public void ExitMount()
    {
        SetIsMounting(false);
        stateMachineTransform.SetParent(null);
        myMount.SetIsMounted(false);
        myMount.TriggerMountedChange();
    }
    public Transform GetStateMachineTransform()
    {
        return stateMachineTransform;
    }
}
