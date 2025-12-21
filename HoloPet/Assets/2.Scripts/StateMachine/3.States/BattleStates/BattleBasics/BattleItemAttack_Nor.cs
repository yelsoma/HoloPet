using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemAttack_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private BattleMod battleMod;
    private ItemHolderMod itemHolderMod;
    private AttackAbilityMod attackAbilityMod;
    private float atkPerSec;
    private float faceRoarTime;
    private bool isFaceAniPlay;
    private ObjectGangEnum targetGang;
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

        IItemHolderMod iItemHolderMod = GetComponentInParent<IItemHolderMod>();
        if (iItemHolderMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no itemHolderSM found in parent.");
        }
        else
        {
            itemHolderMod = iItemHolderMod.ItemHolderMod;
        }

        IBattleMod iBattleMod = GetComponentInParent<IBattleMod>();
        if (iBattleMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no battleMod found in parent.");
        }
        else
        {
            battleMod = iBattleMod.BattleMod;
        }

        IAttackAbilityMod iAttackAbilityMod= GetComponentInParent<IAttackAbilityMod>();
        if (iAttackAbilityMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no iAttackAbilityMod found in parent.");
        }
        else
        {
            attackAbilityMod = iAttackAbilityMod.AttackAbilityMod;
        }     
        if(basicMod.ObjectDefinition.ObjectGangEnum == ObjectGangEnum.Enemy)
        {
            targetGang = ObjectGangEnum.Player;
        }
        else
        {
            targetGang = ObjectGangEnum.Enemy;
        }
       
    }

    public override void Enter()
    {
        atkPerSec = itemHolderMod.ItemHolderMg.GetItem().GetAtkPerSec()/attackAbilityMod.OffenceStatMg.GetAtkSpeed();
        itemHolderMod.ItemHolderMg.GetItem().ChangeToItemUse(atkPerSec,targetGang);
        faceRoarTime = atkPerSec * 0.5f;
        isFaceAniPlay = false;
    }

    public override void StateUpdate()
    {
        if (!itemHolderMod.ItemHolderMg.GetIsHolding())
        {
            stateMachine.ChangeState(battleMod.BattleStart);
            return;
        }
        if(atkPerSec >= 0f)
        {
            if (!isFaceAniPlay && atkPerSec < faceRoarTime)
            {
                TriggerAni1();
                isFaceAniPlay = true;
            }
            atkPerSec -= Time.deltaTime;
            return;
        }
        stateMachine.ChangeState(battleMod.BattleStart);
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        if (itemHolderMod.ItemHolderMg.GetIsHolding())
        {
            itemHolderMod.ItemHolderMg.GetItem().ChangeToHold();
        }
    }
}
