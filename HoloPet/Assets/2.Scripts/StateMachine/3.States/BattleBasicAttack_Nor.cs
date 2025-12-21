using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBasicAttack_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private BattleMod battleMod;
    private AttackAbilityMod attackAbilityMod;
    [SerializeField] private float atkPerSec;
    private float atkSpeedTime;
    private float faceRoarTime;
    private bool isFaceAniPlay;
    private ObjectGangEnum targetGang;
    [SerializeField] private HitBoxDetect hitBox;
    private Animator animator;
    private float damageThisTime;
    private void Awake()
    {
        // Get StateMachine
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
            return;
        }

        // IBasicMod -------------------------------------------------
        if (stateMachine.TryGetComponent<IBasicMod>(out var ibasicMod))
            basicMod = ibasicMod.BasicMod;
        else
            Debug.LogError($"{transform.root.name} ¡X no IBasicMod found on StateMachine.");

        // IBattleMod ------------------------------------------------
        if (stateMachine.TryGetComponent<IBattleMod>(out var iBattleMod))
            battleMod = iBattleMod.BattleMod;
        else
            Debug.LogError($"{transform.root.name} ¡X no IBattleMod found on StateMachine.");

        // IAttackAbilityMod -----------------------------------------
        if (stateMachine.TryGetComponent<IAttackAbilityMod>(out var iAttackAbilityMod))
            attackAbilityMod = iAttackAbilityMod.AttackAbilityMod;
        else
            Debug.LogError($"{transform.root.name} ¡X no IAttackAbilityMod found on StateMachine.");
        
        animator = stateMachine.GetComponent<Animator>();

        if(animator == null)
            Debug.LogError($"{transform.root.name} ¡X no animator found on StateMachine.");
    }

    public override void Enter()
    {
        atkSpeedTime = atkPerSec / attackAbilityMod.OffenceStatMg.GetAtkSpeed();
        if (atkSpeedTime < atkPerSec)
        {
            animator.speed = atkPerSec / atkSpeedTime;
        }
        else
        {
            animator.speed = 1f; // normal speed
        }
        hitBox.OnTriggerHitBox += OnHitBoxTriggered;
        if (basicMod.ObjectDefinition.ObjectGangEnum == ObjectGangEnum.Enemy)
        {
            targetGang = ObjectGangEnum.Player;
        }
        else
        {
            targetGang = ObjectGangEnum.Enemy;
        }
        faceRoarTime = atkSpeedTime * 0.5f;
        isFaceAniPlay = false;
        damageThisTime = attackAbilityMod.OffenceStatMg.GetATK() * attackAbilityMod.OffenceStatMg.GetDamageMultiplier();
    }

    public override void StateUpdate()
    {
        if (atkSpeedTime >= 0f)
        {
            if(!isFaceAniPlay && atkSpeedTime < faceRoarTime)
            {
                TriggerAni1();
                isFaceAniPlay=true;
            }
            atkSpeedTime -= Time.deltaTime;
            return;
        }
        stateMachine.ChangeState(battleMod.BattleStart);
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
        animator.speed = 1f; // normal speed
        hitBox.OnTriggerHitBox -= OnHitBoxTriggered;
        hitBox.gameObject.SetActive(false);
    }
    private void OnHitBoxTriggered(object sender, HitBoxDetect.HitEventArgs e)
    {
        var smTransform = e.stateMachine.transform;
        if (!smTransform.TryGetComponent<IBasicMod>(out IBasicMod basicMod))
            return;
        if (basicMod.BasicMod.ObjectDefinition.ObjectGangEnum != targetGang)
            return;
        if (!smTransform.TryGetComponent<IAttackableMod>(out IAttackableMod attackableMod))
            return;
        var attackableMg = attackableMod.AttackableMod.AttackableMg;
        if (!attackableMg.GetIsAttackable())
            return;

        // attack here
        attackableMg.AttackHP(damageThisTime);

        // Do FX --------------------------------------------------------
        if (smTransform.TryGetComponent<IFXMod>(out IFXMod IFXMod))
        {
            IFXMod.FXMod.FlashFX.StartDamageFlash();
            IFXMod.FXMod.NumberSpawner.SpawnNumber(damageThisTime);
        }
    }
}
