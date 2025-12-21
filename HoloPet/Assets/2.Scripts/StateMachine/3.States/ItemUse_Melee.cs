using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse_Melee : StateBase
{
    private StateMachineBase stateMachine;
    private ItemMod itemMod;
    private float damageThisTime;
    private float atkSpeedThisTime;
    private Animator animator;
    private ObjectGangEnum targetGang;
    [SerializeField] private HitBoxDetect hitBox;

    #region AutoSetRef
    private void Awake()
    {
        // StateMachine ---------------------------------------------------------
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
            return;
        }
        // ItemMod --------------------------------------------------------------
        if (stateMachine.TryGetComponent<IItemMod>(out var iItemMod))
            itemMod = iItemMod.ItemMod;
        else
            Debug.LogError($"{transform.root.name} ¡X no IItemMod found on StateMachine.");
        // Animator -------------------------------------------------------------
        animator = stateMachine.GetComponent<Animator>();
        if (animator == null)
            Debug.LogError($"{transform.root.name} ¡X no Animator found on StateMachine.");
        if (hitBox == null)
            Debug.LogError($"{transform.root.name} ¡X no hit box set in  {transform.name}.");
    }
    #endregion


        #region StateBase
    public override void Enter()
    {
        targetGang = itemMod.ItemMg.GetTargetGang();
        // Damage calculation ----------------------------------------------
        if (itemMod.ItemMg.GetItemHolder().GetSMTransform()
            .TryGetComponent<IAttackAbilityMod>(out var iAttackAbilityMod))
        {
            OffenceStatManager offenceMg = iAttackAbilityMod.AttackAbilityMod.OffenceStatMg;
            damageThisTime = offenceMg.GetDamageMultiplier() * offenceMg.GetLevelMultiplier() * itemMod.ItemMg.GetAtkDamage();
        }
        else
        {
            damageThisTime = 0f;
        }
        // Attack speed ----------------------------------------------------
        float baseAtkSec = itemMod.ItemMg.GetAtkPerSec();
        atkSpeedThisTime = itemMod.ItemMg.GetAtkPerSecThisTime();
        // Only speed up animation; never slow it down
        if (atkSpeedThisTime < baseAtkSec)
            animator.speed = baseAtkSec / atkSpeedThisTime;
        else
            animator.speed = 1f;
        // Activate the hitbox
        hitBox.OnTriggerHitBox += ItemUse_Sword_OnTriggerHitBox;
    }

    public override void Exit()
    {
        animator.speed = 1f;
        hitBox.OnTriggerHitBox -= ItemUse_Sword_OnTriggerHitBox;
    }
    #endregion


    #region Event
    private void ItemUse_Sword_OnTriggerHitBox(object sender, HitBoxDetect.HitEventArgs e)
    {
        var smTransform = e.stateMachine.transform;
        if (!smTransform.TryGetComponent<IBasicMod>(out var basicMod))
            return;
        if (basicMod.BasicMod.ObjectDefinition.ObjectGangEnum != targetGang)
            return;
        if (!smTransform.TryGetComponent<IAttackableMod>(out var attackableMod))
            return;
        var attackableMg = attackableMod.AttackableMod.AttackableMg;
        if (!attackableMg.GetIsAttackable())
            return;

        // Do damage --------------------------------------------------------
        attackableMg.AttackHP(damageThisTime);

        // Do FX --------------------------------------------------------
        if (smTransform.TryGetComponent<IFXMod>(out IFXMod IFXMod))
        {
            IFXMod.FXMod.FlashFX.StartDamageFlash();
            IFXMod.FXMod.NumberSpawner.SpawnNumber(damageThisTime);
        }
    }
    #endregion
}

