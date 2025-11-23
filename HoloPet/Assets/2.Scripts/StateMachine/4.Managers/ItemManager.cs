using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private StateMachineBase stateMachine;
    [SerializeField] private ItemType itemType;
    [SerializeField] private float attackDistance;
    private IBasicSM basicSM;
    private bool isHolded;
    private ItemHolderManager holderMg;
    private IItemSM itemSM;
    [SerializeField]private LayerMask targetLayer;
    [SerializeField] private ItemHitDetect itemHitDetect;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if(stateMachine == null)
        {
            Debug.LogError("no statemachinebase in " + transform.root.name);
        }
        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        itemSM = GetComponentInParent<IItemSM>();
        if (itemSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no itemHoldSM found in parent.");
        }
        if (itemHitDetect == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no itemHitDetect found in parent.");
        }
        Debug.Log(targetLayer.value + "start");
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
        RaycastHit2D[] hits =  basicSM.RaycastMg.GetAllHits(Vector2.down, distance);
        if(hits.Length >= 0)
        {
            foreach(RaycastHit2D hit in hits)
            {
                IItemHolderSM itemHolderSM = hit.transform.GetComponent<IItemHolderSM>();
                if(itemHolderSM != null)
                {
                    holderMg = itemHolderSM.ItemHolderMg;
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
        holderMg.SetItemHold(itemSM.ItemMg);
        IBasicSM holderBasicSM = holderMg.GetComponentInParent<IBasicSM>();
        if(holderBasicSM != null)
        {
            if (holderBasicSM.FaceDirectionMg.GetIsFaceRight())
            {
                basicSM.FaceDirectionMg.SetFaceRight();
            }
            else
            {
                basicSM.FaceDirectionMg.SetFaceLeft();
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
        stateMachine.ChangeState(itemSM.StateItemUse);
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
