using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidLayerManager : MonoBehaviour ,ILayerManager
{
    [Header("SpriteLayers")]
    [SerializeField] private SpriteLayer bodyLayer;
    [SerializeField] private SpriteLayer backHandLayer;

    private IBasicSM basicSM;
    private IMountingAbilitySM mountingAbilitySM;
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
       
        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{name} ¡X IBasicSM not found in parent.");
        }

        mountingAbilitySM = GetComponentInParent<IMountingAbilitySM>();
        if (mountingAbilitySM == null)
        {
            Debug.LogError($"{name} ¡X IMountingAbilitySM not found in parent.");
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

        basicSM.StateClicked.OnEnterState += StateClicked_OnEnterState;
        basicSM.StateGrabbed.OnEnterState += StateGrabbed_OnEnterState;
        mountingAbilitySM.StateMounting.OnEnterState += StateMounting_OnEnterState;
        basicSM.StateSpawn.OnEnterState += StateSpawn_OnEnterState;
        basicSM.StateGrabbed.OnExitState += StateGrabbed_OnExitState;
        interactableManager.InteractableMg.OnEnterInteractedChangeLayer += InteractableMg_OnEnterInteractedChangeLayer;
        attackableSM.StateHpZero.OnEnterState += StateHpZero_OnEnterState;
        basicSM.StateDestroy.OnEnterState += StateDeSpawn_OnEnterState;
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
