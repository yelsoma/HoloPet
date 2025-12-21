using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        IBasicMod iBasicMod = stateMachine.transform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            basicMod = iBasicMod.BasicMod;
    }

    public override void Enter()
    {
        basicMod.ClickableMg.SetIsClickable(false);
        IInteractableMod iInteractableMod = stateMachine.GetComponent<IInteractableMod>();      
        if(iInteractableMod != null)
        {
            iInteractableMod.InteractableMod.InteractableMg.SetIsInteractable(false);
        }
        IAttackableMod attackableMod = stateMachine.GetComponent<IAttackableMod>();
        if(attackableMod != null)
        {
            attackableMod.AttackableMod.AttackableMg.SetIsAttackable(false);
        }
        IMountableMod mountableMod = stateMachine.GetComponent<IMountableMod>();
        if(mountableMod != null)
        {
            mountableMod.MountableMod.MountableMg.SetIsMountableState(false);
        }
        IItemHolderMod itemHolderMod = stateMachine.GetComponent<IItemHolderMod>();
        if(itemHolderMod != null)
        {
            ItemHolderManager itemHolderMg = itemHolderMod.ItemHolderMod.ItemHolderMg;
            itemHolderMg.SetIsCanHoldState(false);
            if (itemHolderMg.GetIsHolding())
            {
                ItemManager itemHoldMg = itemHolderMg.GetItem();
                itemHoldMg.ExitHold();
                itemHoldMg.GetStateMachine().ChangeState(itemHoldMg.GetStateMachine().GetComponent<IBasicMod>().BasicMod.StateClicked); 
            }
        }
        IItemMod iItemMod = GetComponentInParent<IItemMod>();
        if (iItemMod != null)
        {
            ItemManager itemMg = iItemMod.ItemMod.ItemMg;
            if (itemMg.GetIsHold())
            {
                itemMg.ExitHold();
            }
        }
        //layer is removed in layerMg on enter
        GameController.Instance.StateMachineListMg.RemoveObjectFromList(stateMachine);
        ObjectDefinition objectDefinition = basicMod.ObjectDefinition;
        if (GameController.Instance.IsBattleActive)
        {
            if (objectDefinition.ObjectGangEnum == ObjectGangEnum.Player)
            {
                GameController.Instance.DeathPlayerList.Add(objectDefinition);
            }
        }      
        //destroy
        Destroy(stateMachine.gameObject);
    }

    public override void StateUpdate()
    {
       
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        Destroy(stateMachine.gameObject);
    }
}
