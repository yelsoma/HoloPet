using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidLayerManager : MonoBehaviour ,ILayerManager
{
    [Header("SpriteLayers")]
    [SerializeField] private SpriteLayer bodyLayer;
    [SerializeField] private SpriteLayer backHandLayer;

    private BasicMod basicMod;
    private MountingAbilityMod mountingAbilityMod;
    private Transform stateMachineTransform;
    private InteractableMod interactableMod;
    private AttackableMod attackableMod;
    private void Awake()
    {
        StateMachineBase stateMachineBase = GetComponentInParent<StateMachineBase>();
        if (stateMachineBase == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");

        stateMachineTransform = stateMachineBase.transform;

        if (bodyLayer == null)
            Debug.LogError($"{transform.root.name} ¡X no BasicLayerSet In LayerManager");

        if (backHandLayer == null)
            Debug.LogError($"{transform.root.name} ¡X no BackhandLayerSet In LayerManager");

        IBasicMod iBasicMod = stateMachineTransform.GetComponent<IBasicMod>();
        if (iBasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            basicMod = iBasicMod.BasicMod;

        IMountingAbilityMod iMountingMod = stateMachineTransform.GetComponent<IMountingAbilityMod>();
        if (iMountingMod == null)
            Debug.LogError($"{transform.root.name} ¡X no imountingAbilityMod found in parent.");
        else
            mountingAbilityMod = iMountingMod.MountingAbilityMod;

        IInteractableMod iInteractMod = stateMachineTransform.GetComponent<IInteractableMod>();
        if (iInteractMod == null)
            Debug.LogError($"{transform.root.name} ¡X no IInteractableMod found in parent.");
        else
            interactableMod = iInteractMod.InteractableMod;

        IAttackableMod iAttackMod = stateMachineTransform.GetComponent<IAttackableMod>();
        if (iAttackMod == null)
            Debug.LogError($"{name} ¡X iAttackableMod not found in parent.");
        else
            attackableMod = iAttackMod.AttackableMod;

        basicMod.StateClicked.OnEnterState += StateClicked_OnEnterState;
        basicMod.StateGrabbed.OnEnterState += StateGrabbed_OnEnterState;
        mountingAbilityMod.StateMounting.OnEnterState += StateMounting_OnEnterState;
        basicMod.StateGrabbed.OnExitState += StateGrabbed_OnExitState;
        interactableMod.InteractableMg.OnEnterInteractedChangeLayer += InteractableMg_OnEnterInteractedChangeLayer;
        attackableMod.StateHpZero.OnEnterState += StateHpZero_OnEnterState;
        basicMod.StateDestroy.OnEnterState += StateDeSpawn_OnEnterState;

        SpriteLayerCenter.AddNewLayers(stateMachineTransform);
    }

    private void StateDeSpawn_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.RemoveLayers(stateMachineTransform);
    }

    private void StateHpZero_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(stateMachineTransform);
    }

    private void InteractableMg_OnEnterInteractedChangeLayer(object sender, InteractableManager.ChangeLayerEventArgs e)
    {
        SpriteLayerCenter.InsertLayersAfterTransform(stateMachineTransform, e.Transform);
    }

    private void StateGrabbed_OnExitState(object sender, System.EventArgs e)
    {
        backHandLayer.SortingOrderPlus(-2);
    }

    private void StateMounting_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.InsertLayersToParent(stateMachineTransform);
    }

    private void StateGrabbed_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(stateMachineTransform);
        backHandLayer.SortingOrderPlus(2);
    }

    private void StateClicked_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(stateMachineTransform);
    }

    public int GetObjectMainLayer()
    {
        return bodyLayer.GetSpriteLayer();
    }
}
