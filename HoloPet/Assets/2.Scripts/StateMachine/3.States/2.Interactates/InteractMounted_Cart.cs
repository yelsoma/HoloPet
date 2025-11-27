using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMounted_Cart : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private IInteractableSM interactableSM;
    private InteractableManager myInteractableMg;
    private MountableMod mountableMod;
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

        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        else
        {
            basicMod = ibasicMod.BasicMod;
        }

        interactableSM = GetComponentInParent<IInteractableSM>();
        if (interactableSM == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no IInteractableSM found in parent.");
        }

        IMountableMod imountableMod = GetComponentInParent < IMountableMod>();
        if(imountableMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no mountableMod  found in parent.");
        }
        else
        {
            mountableMod = imountableMod.MountableMod;
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
        BasicMod interacterBasicMod = interacterTransform.GetComponent<IBasicMod>().BasicMod;
        if (interacterTransform.TryGetComponent<IMountingAbilityMod>(out IMountingAbilityMod iMounterMountingAbilityMod) && iMounterMountingAbilityMod.MountingAbilityMod.MountingAbilityMg.TrySetMount(mountableMod.MountableMg))
        {
            //sucsses set mounter
            interacterSM.ChangeState(iMounterMountingAbilityMod.MountingAbilityMod.StateMounting);

            if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
            {
                //check is it batan
                if (interacterBasicMod.BaseDataMg.GetObjectName() == ObjectNameEnum.Botan)
                {
                    stateMachine.ChangeState(driveSM.StateDrive);
                    return;
                }
            }
            else
            {
                basicMod.PhysicsMg.MoveDown(fallSpeedNow);
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
        if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
        {
            stateMachine.ChangeState(basicMod.StateIdle);
        }
        else
        {
            stateMachine.ChangeState(basicMod.StateInAir);
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
