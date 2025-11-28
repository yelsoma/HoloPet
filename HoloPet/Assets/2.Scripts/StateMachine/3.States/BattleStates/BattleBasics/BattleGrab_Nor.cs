using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGrab_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private BattleMod battleMod;
    [SerializeField] private float grabOffset = 0.5f;

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

        IBattleMod iBattleMod = GetComponentInParent<IBattleMod>();
        if(iBattleMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no battleMod found in parent.");
        }
        else
        {
            battleMod = iBattleMod.BattleMod;
        }
    }

    public override void Enter()
    {
        basicMod.ClickableMg.OnRelease += ClickableManager_OnRelease;
        basicMod.ClickableMg.OnGrabMousePos += ClickableManager_OnGrabMousePos;
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
        if (battleMod.GetIsInbattle())
        {
            stateMachine.ChangeState(battleMod.BattleStart);
            return;
        }
        stateMachine.ChangeState(basicMod.StateReleased);
    }

    private void ClickableManager_OnGrabMousePos(object sender, ClickableManager.GrabEventArgs e)
    {
        stateMachine.transform.position = new Vector2(e.MousePosition.x, e.MousePosition.y - grabOffset);
    }
}
