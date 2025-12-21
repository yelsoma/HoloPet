using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_TempItem : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    [SerializeField] private int addCoin;

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
        IMountableMod mountableMod = stateMachine.GetComponent<IMountableMod>();
        if(mountableMod != null)
        {
            mountableMod.MountableMod.MountableMg.SetIsMountableState(false);
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
        GameController.Instance.StateMachineListMg.RemoveObjectFromList(stateMachine);
        GameController.Instance.AFKManager.AddCoin(addCoin);
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
