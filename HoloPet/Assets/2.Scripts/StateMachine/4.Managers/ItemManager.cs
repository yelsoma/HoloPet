using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private StateMachineBase stateMachine;
    [SerializeField] private ItemType itemType;
    [SerializeField] private float atkDistance;
    [SerializeField] private float atkPerSec;
    [SerializeField] private float atkDamage;
    private float atkPerSecThisTime;
    private BasicMod basicMod;
    private bool isHolded;
    private ItemHolderManager holderMg;
    private ItemMod itemMod;
    private ObjectGangEnum targetGang;
    [SerializeField] private SpriteRenderer handBack;
    [SerializeField] private SpriteRenderer handFront;
    [SerializeField] private StateBase[] unHoldStates;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError("no statemachinebase in " + transform.root.name);

        IBasicMod iBasicMod = stateMachine.transform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            basicMod = iBasicMod.BasicMod;

        IItemMod iItemMod = stateMachine.transform.GetComponent<IItemMod>();
        if (iItemMod == null)
            Debug.LogError($"{transform.root.name} ¡X no iItemMod found in parent.");
        else
            itemMod = iItemMod.ItemMod;

        if (handFront == null || handBack == null)
            Debug.LogError($"{transform.root.name} ¡X no hand spriteRenderer set in item Mg.");

        if (atkDistance == 0f || atkPerSec == 0f)
            Debug.LogError($"{transform.root.name} ¡X item Mg stat not set have 0.");

        if (unHoldStates.Length > 0)
        {
            foreach (StateBase unHoldableState in unHoldStates)
            {
                unHoldableState.OnEnterState += UnHoldableState_OnEnterState;
            }
        }
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
        RaycastHit2D[] hits =  basicMod.RaycastMg.GetAllHits(Vector2.up, distance);
        if (hits.Length >= 0)
        {
            foreach(RaycastHit2D hit in hits)
            {
                IItemHolderMod iItemHolderMod = hit.transform.GetComponent<IItemHolderMod>();
                if(iItemHolderMod != null)
                {
                    holderMg = iItemHolderMod.ItemHolderMod.ItemHolderMg;
                    if (!holderMg.GetIsHolding() && holderMg.GetIsCanHoldState())
                    {
                        return true;
                    }                  
                }              
            }
        }
        return false;
    }
    public void SetHolderMg(ItemHolderManager itemHolderManager)
    {
        holderMg = itemHolderManager;
    }
    public void EnterHold()
    {
        isHolded = true;
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
        handFront.sprite = holderMg.GetHandFront();
        handBack.sprite = holderMg.GetHandBack();
        holderMg.SetIsHolding(true);
    }
    public void ExitHold()
    {
        isHolded = false;
        holderMg.SetIsHolding(false);
        stateMachine.transform.SetParent(null);
        holderMg.RemoveItem();
        handFront.sprite = null;
        handBack.sprite=null;
    } 
    public float GetAttackDistance()
    {
        return atkDistance;
    }
    public float GetAtkPerSec()
    {
        return atkPerSec;
    }
    public float GetAtkDamage()
    {
        return atkDamage;
    }
    public float GetAtkPerSecThisTime()
    {
        return atkPerSecThisTime;
    }
    public ItemHolderManager GetItemHolder() => holderMg;
    public void ChangeToItemUse(float attackPerSec , ObjectGangEnum targetGangEnum)
    {
        atkPerSecThisTime = attackPerSec;
        targetGang = targetGangEnum;
        stateMachine.ChangeState(itemMod.StateItemUse);
    }
    public void ChangeToHold()
    {
        stateMachine.ChangeState(itemMod.StateHold);
    }
    public StateMachineBase GetStateMachine() => stateMachine;
    public ObjectGangEnum GetTargetGang()
    {
        return targetGang;
    }
    //event
    private void UnHoldableState_OnEnterState(object sender, System.EventArgs e)
    {
        if(holderMg != null)
        {
            ExitHold();
        }
    }
}
