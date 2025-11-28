using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbed_Item : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private ItemMod itemMod;
    [SerializeField] private float grabOffset = -0.5f;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
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
        if(iItemMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no iItemMod found in parent.");
        }
        else
        {
            itemMod = iItemMod.ItemMod;
        }
    }

    public override void Enter()
    {
        basicMod.ClickableMg.OnRelease += ClickableManager_OnRelease;
        basicMod.ClickableMg.OnGrabMousePos += ClickableManager_OnGrabMousePos;
        if (itemMod.ItemMg.GetIsHold())
        {
            itemMod.ItemMg.ExitHold();
        }      
    }

    public override void StateUpdate()
    {
    }

    public override void StateLateUpdate()
    {
        basicMod.BoundaryMg.CheckAllBouderyAndResetPos();
    }

    public override void Exit()
    {
        basicMod.ClickableMg.OnRelease -= ClickableManager_OnRelease;
        basicMod.ClickableMg.OnGrabMousePos -= ClickableManager_OnGrabMousePos;
    }

    private void ClickableManager_OnRelease(object sender, System.EventArgs e)
    {
        stateMachine.ChangeState(basicMod.StateReleased);
    }

    private void ClickableManager_OnGrabMousePos(object sender, ClickableManager.GrabEventArgs e)
    {
        stateMachine.transform.position = new Vector2(e.MousePosition.x, e.MousePosition.y - grabOffset);
    }
}
