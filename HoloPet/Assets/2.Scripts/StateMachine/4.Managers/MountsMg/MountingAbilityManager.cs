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
    private IBasicSM basicSM;

    private void Awake()
    {
        stateMachineTransform = GetComponentInParent<StateMachineBase>().transform;
        basicSM = stateMachineTransform.GetComponent<IBasicSM>();
        if(basicSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in StateMachineBase.");
        }
        raycastManager = basicSM.RaycastMg;
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
        IMountableSM closestMountable = null;
        float closestDistance = float.MaxValue;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.TryGetComponent<IMountableSM>(out var mountableSM) &&
                mountableSM.MountableMg.GetIsMountable())
            {
                if (hit.distance < closestDistance)
                {
                    closestDistance = hit.distance;
                    closestMountable = mountableSM;
                }
            }
        }

        if (closestMountable != null)
        {
            myMount = closestMountable.MountableMg;
            return true;
        }

        return false;
    }
    public void EnterMount()
    {
        stateMachineTransform.SetParent(myMount.GetMountPointTansform());
        if (transform.root.GetComponent<IBasicSM>().FaceDirectionMg.GetIsFaceRight())
        {
            stateMachineTransform.GetComponent<IBasicSM>().FaceDirectionMg.SetFaceRight();
        }
        else
        {
            stateMachineTransform.GetComponent<IBasicSM>().FaceDirectionMg.SetFaceLeft();
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
