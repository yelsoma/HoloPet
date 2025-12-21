using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMounting_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private MountingAbilityMod mountingAbilityMod;
    private bool canHoldItem;
    private ItemHolderManager itemHolderMg;
    private AttackAbilityMod attackAbilityMod;
    private float atkPerSec;
    private bool isAtkSet;
    private bool isFirstAttack;
    private float firstAttackDelay;
    private ObjectGangEnum targetGang;

    #region AutoSetRef
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");

        IBasicMod iBasicMod = stateMachine.transform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            basicMod = iBasicMod.BasicMod;

        IMountingAbilityMod iMountingAbilityMod = stateMachine.transform.GetComponent<IMountingAbilityMod>();
        if (iMountingAbilityMod == null)
            Debug.LogError($"{transform.root.name} ¡X no imountingAbilityMod found in parent.");
        else
            mountingAbilityMod = iMountingAbilityMod.MountingAbilityMod;

        IAttackAbilityMod iAttackAbilityMod = GetComponentInParent<IAttackAbilityMod>();
        if (iAttackAbilityMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no iAttackAbilityMod found in parent.");
        }
        else
        {
            attackAbilityMod = iAttackAbilityMod.AttackAbilityMod;
        }
        IItemHolderMod iItemHolderMod = stateMachine.transform.GetComponent<IItemHolderMod>();
        if (iItemHolderMod == null)
            canHoldItem = false;
        else
        {
            itemHolderMg = iItemHolderMod.ItemHolderMod.ItemHolderMg;
            canHoldItem = true;
        }           

    }

    #endregion

    #region StateBase
    public override void Enter()
    {
        mountingAbilityMod.MountingAbilityMg.EnterMount();
        mountingAbilityMod.MountingAbilityMg.GetMount().OnEnterUnMountableState += BattleMounting_Nor_OnEnterUnMountableState;

        isAtkSet = false;
        isFirstAttack = true;
        firstAttackDelay = Random.Range(2f, 3f);
        if (basicMod.ObjectDefinition.ObjectGangEnum == ObjectGangEnum.Enemy)
            targetGang = ObjectGangEnum.Player;
        else
            targetGang = ObjectGangEnum.Enemy;
    }

    private void BattleMounting_Nor_OnEnterUnMountableState(object sender, System.EventArgs e)
    {        
        if (GameController.Instance.IsBattleActive)
        {
            IMountableMod mountableMod = stateMachine.GetComponent<IMountableMod>();
            if (mountableMod != null && mountableMod.MountableMod.MountableMg.GetIsMounted())
            {
                stateMachine.ChangeState(basicMod.StateInAir);
                return;
            }
        }
        stateMachine.ChangeState(basicMod.StateClicked);
        return;
    }

    public override void StateUpdate()
    {
        if (!GameController.Instance.IsBattleActive)
            return;

        if (!canHoldItem || !itemHolderMg.GetIsHolding())
            return;

        HandleItemAttack();
    }

    public override void StateLateUpdate()
    {
    }
    public override void Exit()
    {
        if (itemHolderMg.GetIsHolding())
        {
            itemHolderMg.GetItem().ChangeToHold();
        }
        mountingAbilityMod.MountingAbilityMg.ExitMount(); 
        mountingAbilityMod.MountingAbilityMg.GetMount().OnEnterUnMountableState -= BattleMounting_Nor_OnEnterUnMountableState;
    }
    #endregion

    private void HandleItemAttack()
    {
        // first time delay
        if (isFirstAttack)
        {
            firstAttackDelay -= Time.deltaTime;
            if (firstAttackDelay > 0f)
                return;

            isFirstAttack = false;
            isAtkSet = false; // allow first real attack
        }

        if (!isAtkSet)
        {
            atkPerSec =
                itemHolderMg.GetItem().GetAtkPerSec() /
                attackAbilityMod.OffenceStatMg.GetAtkSpeed();

            itemHolderMg.GetItem().ChangeToItemUse(atkPerSec, targetGang);
            isAtkSet = true;
            return;
        }

        atkPerSec -= Time.deltaTime;

        if (atkPerSec < 0f)
        {
            isAtkSet = false;
        }
    }
}
