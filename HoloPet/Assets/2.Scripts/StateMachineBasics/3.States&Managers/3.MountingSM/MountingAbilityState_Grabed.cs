using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountingAbilityState_Grabed : StateBase
{

    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IMountingAbilitySM mountingAbilitySM;

    #region AutoSetRef
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform} ¡X no basicSM found in parent.");
        }

        mountingAbilitySM = GetComponentInParent<IMountingAbilitySM>();
        if (mountingAbilitySM == null)
        {
            Debug.LogError($"{transform} ¡X no IMountingAbilitySM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        basicSM.ClickableMg.OnRelease += ClickableManager_OnRelease;
        basicSM.ClickableMg.OnGrab += ClickableManager_OnGrab;
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
        basicSM.ClickableMg.OnGrab -= ClickableManager_OnGrab;
    }
    #endregion

    #region Events
    private void ClickableManager_OnRelease(object sender, System.EventArgs e)
    {
        if(basicSM.RaycastMg.TrySetRaycast(1f, Vector2.down) && mountingAbilitySM.MountingAbilityMg.TrySetMountWithRaycast(basicSM.RaycastMg.GetRaycastHits()))
        {
            basicSM.RaycastMg.ClearHits();
            stateMachine.ChangeState(mountingAbilitySM.StateMounting);
            return;
        }
        if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicSM.StateIdle);
        }
        else
        {
            stateMachine.ChangeState(basicSM.StateInAir);
        }
    }

    private void ClickableManager_OnGrab(object sender, ClickableManager.GrabEventArgs e)
    {
        stateMachine.transform.position = e.MousePosition;
    }
    #endregion
}
