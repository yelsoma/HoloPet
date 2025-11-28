using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private StateMachineBase stateMachine;
    [SerializeField] private ItemType itemType;
    [SerializeField] private float attackDistance;
    private BasicMod basicMod;
    private bool isHolded;
    private ItemHolderManager holderMg;
    private ItemMod itemMod;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private ItemHitDetect itemHitDetect;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if(stateMachine == null)
        {
            Debug.LogError("no statemachinebase in " + transform.root.name);
        }

        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        else
        {
            basicMod = ibasicMod.BasicMod;
        }

        IItemMod iItemMod = GetComponentInParent<IItemMod>();
        if (iItemMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no iItemMod found in parent.");
        }
        else
        {
            itemMod = iItemMod.ItemMod;
        }

        if (itemHitDetect == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no itemHitDetect found in parent.");
        }
    }   

    private void Start()
    {
        isHolded = false;
    }
    public void SetIsHold(bool isHold)
    {
        this.isHolded = isHold; 
    }
    public bool GetIsHold()
    {
        return isHolded;
    }
    public ItemType GetItemType()
    {
        return itemType;
    }
    public bool TrySetHolderRayCast(float distance)
    {
        RaycastHit2D[] hits =  basicMod.RaycastMg.GetAllHits(Vector2.down, distance);
        if(hits.Length >= 0)
        {
            foreach(RaycastHit2D hit in hits)
            {
                IItemHolderMod iItemHolderMod = hit.transform.GetComponent<IItemHolderMod>();
                if(iItemHolderMod != null)
                {
                    holderMg = iItemHolderMod.ItemHolderMod.ItemHolderMg;
                    if (!holderMg.GetIsHolding())
                    {
                        return true;
                    }                  
                }              
            }
        }
        return false;
    }
    public void EnterHold()
    {
        isHolded = true;
        holderMg.SetIsHolding(true);
        stateMachine.transform.SetParent(holderMg.GetHoldPoint());
        stateMachine.transform.position = holderMg.GetHoldPoint().position;
        holderMg.SetItemHold(itemMod.ItemMg);
        IBasicMod holderIBasicMod = holderMg.GetComponentInParent<IBasicMod>();
        if(holderIBasicMod != null)
        {
            if (holderIBasicMod.BasicMod.FaceDirectionMg.GetIsFaceRight())
            {
                basicMod.FaceDirectionMg.SetFaceRight();
            }
            else
            {
                basicMod.FaceDirectionMg.SetFaceLeft();
            }
        }
    }
    public void ExitHold()
    {
        isHolded = false;
        holderMg.SetIsHolding(false);
        stateMachine.transform.SetParent(null);
        holderMg.RemoveItem();
    } 
    public float GetAttackDistance()
    {
        return attackDistance;
    }
    public ItemHolderManager GetItemHolder() => holderMg;
    public void ChangeToItemUse()
    {
        stateMachine.ChangeState(itemMod.StateItemUse);
    }

    public void SetColliderActive(bool active)
    {
        if (active)
        {
            itemHitDetect.transform.gameObject.SetActive(true);
        }
        else
        {
            itemHitDetect.transform.gameObject.SetActive(false);
        }
    }

    public ItemHitDetect GetColliderScript()
    {
        return itemHitDetect;
    }

    public void SetTargetLayerMask(LayerMask layerMask )
    {
        targetLayer = layerMask;
    }

    public LayerMask GetTargetLayerMask()
    {
        return targetLayer;
    }
}
