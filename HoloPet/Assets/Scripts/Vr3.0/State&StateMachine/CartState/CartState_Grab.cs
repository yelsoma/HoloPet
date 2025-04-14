using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartState_Grab : StateBase
{
    [SerializeField] private CartStateMachine stateMachine;


    // < State Base >
    public override void Enter()
    {
        //event
        stateMachine.mouseInput.OnDrag += MouseInput_OnDrag;
        stateMachine.mouseInput.OnRelease += Input_OnRelease;

        //start
        if (stateMachine.mountManager.GetIsMounted())
        {
            stateMachine.mountManager.LayerChainUpStart();
        }
        else
        {
            stateMachine.layerManager.ChangeLayerAll();
        }
    }



    public override void StateUpdate()
    {
        // logic in event
    }
    public override void StateLateUpdate()
    {
        stateMachine.boundaryManager.CheckAllBouderyAndResetPos();
    }
    public override void Exit()
    {
        //event
        stateMachine.mouseInput.OnDrag -= MouseInput_OnDrag;
        stateMachine.mouseInput.OnRelease -= Input_OnRelease;
    }

    // < Events >
    private void MouseInput_OnDrag(object sender, MouseInputVr2.OnDragEventArgs e)
    {
        //drag  
        stateMachine.transform.position = e.mousePos;
    }
    private void Input_OnRelease(object sender, System.EventArgs e)
    {        
        stateMachine.raycastManager.ClearHits();
        //exit to fall
        stateMachine.ChangeState(stateMachine.stateFall);
        return;
    }
}
