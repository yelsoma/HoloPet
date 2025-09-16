using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHoldManager : MonoBehaviour
{
    private StateMachineBase stateMachine;
    [SerializeField] private ItemType itemType;
    [SerializeField] private float attackDistance;
    private IBasicSM basicSM;
    private bool isHolded;
    private ItemHolderManager holderMg;
    private IItemHoldSM itemHoldSM;
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if(stateMachine == null)
        {
            Debug.LogError("no statemachinebase in " + transform);
        }
        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform} ¡X no basicSM found in parent.");
        }
        itemHoldSM = GetComponentInParent<IItemHoldSM>();
        if (itemHoldSM == null)
        {
            Debug.LogError($"{transform} ¡X no itemHoldSM found in parent.");
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
    public bool TrySetHolder(float distance)
    {
        RaycastHit2D[] hits =  basicSM.RaycastMg.GetAllHits(Vector2.down, distance);
        if(hits.Length >= 0)
        {
            foreach(RaycastHit2D hit in hits)
            {
                IHumanAttackSM humanSM = hit.transform.GetComponent<IHumanAttackSM>();
                if(humanSM != null)
                {
                    holderMg = humanSM.ItemHolderMg;
                    Debug.Log(holderMg.transform.root);
                    return true;
                }              
            }
        }
        return false;
    }
    public void EnterHolder()
    {
        isHolded = true;
        holderMg.SetIsHolding(true);
        stateMachine.transform.SetParent(holderMg.GetHoldPoint());
        stateMachine.transform.position = holderMg.GetHoldPoint().position;
        holderMg.SetItemHold(itemHoldSM.ItemHoldMg);
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
}
