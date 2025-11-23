using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMounted_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    private IInteractableSM interactableSM;
    private InteractableManager myInteractableMg;
    private IMountableSM mountableSM;
    private IDriveSM driveSM;

    private float fallSpeedIncreese = 6.5f;
    private float fallSpeedMax = 9f;
    private float fallSpeedNow;
    #region AutoSetRef
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

        interactableSM = GetComponentInParent<IInteractableSM>();
        if (interactableSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IInteractableSM found in parent.");
        }

        mountableSM = GetComponentInParent < IMountableSM>();
        if(mountableSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no mountableSM  found in parent.");
        }

        driveSM = GetComponentInParent<IDriveSM>();
        if (driveSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no driveSM   found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {
        fallSpeedNow = 0f;
    }
    public override void StateUpdate()
    {
        myInteractableMg = interactableSM.InteractableMg;
        Transform interacterTransform = myInteractableMg.GetInteracterManager().GetStateMachineTransform();
        StateMachineBase interacterSM = interacterTransform.GetComponent<StateMachineBase>();
        IBasicSM interacterBasicSM = interacterTransform.GetComponent<IBasicSM>();
        if (interacterTransform.TryGetComponent<IMountingAbilitySM>(out IMountingAbilitySM MounterMountingAbilitySM) && MounterMountingAbilitySM.MountingAbilityMg.TrySetMount(mountableSM.MountableMg))
        {
            //sucsses set mounter
            interacterSM.ChangeState(MounterMountingAbilitySM.StateMounting);

            if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                //check is it batan
                if (interacterBasicSM.BaseDataMg.GetObjectName() == ObjectNameEnum.Botan)
                {
                    stateMachine.ChangeState(driveSM.StateDrive);
                    return;
                }
            }
            else
            {
                basicSM.PhysicsMg.MoveDown(fallSpeedNow);
                if (fallSpeedNow <= fallSpeedMax)
                {
                    fallSpeedNow += fallSpeedIncreese;                    
                }
                else
                {
                    fallSpeedNow = fallSpeedMax;
                }
            }           
        }
        //fail
        if (basicSM.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicSM.StateIdle);
        }
        else
        {
            stateMachine.ChangeState(basicSM.StateInAir);
        }           
    }
    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
    }
    #endregion
}
