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
    private IInteractableSM interactableManager;
    private IAttackableSM attackableSM;
    private void Awake()
    {
        stateMachineTransform = transform.root;
        if (bodyLayer == null)
        {
            Debug.LogError(stateMachineTransform + " mainLayer is missing on HumanoidLayerManager");
        }
        if (backHandLayer == null)
        {
            Debug.LogError(stateMachineTransform + " backHandLayer is missing on HumanoidLayerManager");
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

        IMountingAbilityMod iMountingAbilityMod = GetComponentInParent<IMountingAbilityMod>();
        if (iMountingAbilityMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no imountingAbilityMod found in parent.");
        }
        else
        {
            mountingAbilityMod = iMountingAbilityMod.MountingAbilityMod;
        }

        interactableManager = GetComponentInParent<IInteractableSM>();
        if (interactableManager == null)
        {
            Debug.LogError($"{name} ¡X IInteractableSM not found in parent.");
        }
        attackableSM = GetComponentInParent<IAttackableSM>();
        if (attackableSM == null)
        {
            Debug.LogError($"{name} ¡X attackableSM not found in parent.");
        }

        basicMod.StateClicked.OnEnterState += StateClicked_OnEnterState;
        basicMod.StateGrabbed.OnEnterState += StateGrabbed_OnEnterState;
        mountingAbilityMod.StateMounting.OnEnterState += StateMounting_OnEnterState;
        basicMod.StateSpawn.OnEnterState += StateSpawn_OnEnterState;
        basicMod.StateGrabbed.OnExitState += StateGrabbed_OnExitState;
        interactableManager.InteractableMg.OnEnterInteractedChangeLayer += InteractableMg_OnEnterInteractedChangeLayer;
        attackableSM.StateHpZero.OnEnterState += StateHpZero_OnEnterState;
        basicMod.StateDestroy.OnEnterState += StateDeSpawn_OnEnterState;
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

    private void StateSpawn_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.AddNewLayers(stateMachineTransform);
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
