using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbed_Item : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IItemSM itemSM;
    [SerializeField] private float grabOffset = -0.5f;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }

        itemSM = GetComponentInParent<ItemSM>();
        if(itemSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no itemSM found in parent.");
        }
    }

    public override void Enter()
    {
        basicSM.ClickableMg.OnRelease += ClickableManager_OnRelease;
        basicSM.ClickableMg.OnGrabMousePos += ClickableManager_OnGrabMousePos;
        if (itemSM.ItemMg.GetIsHold())
        {
            itemSM.ItemMg.ExitHold();
        }      
    }

    public override void StateUpdate()
    {
    }

    public override void StateLateUpdate()
    {
        basicSM.BoundaryMg.CheckAllBouderyAndResetPos();
    }

    public override void Exit()
    {
        basicSM.ClickableMg.OnRelease -= ClickableManager_OnRelease;
        basicSM.ClickableMg.OnGrabMousePos -= ClickableManager_OnGrabMousePos;
    }

    private void ClickableManager_OnRelease(object sender, System.EventArgs e)
    {
        stateMachine.ChangeState(basicSM.StateReleased);
    }

    private void ClickableManager_OnGrabMousePos(object sender, ClickableManager.GrabEventArgs e)
    {
        stateMachine.transform.position = new Vector2(e.MousePosition.x, e.MousePosition.y - grabOffset);
    }
}
