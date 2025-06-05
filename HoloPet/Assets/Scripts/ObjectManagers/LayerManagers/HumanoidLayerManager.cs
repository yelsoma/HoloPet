using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidLayerManager : MonoBehaviour ,ILayerManager
{
    [Header("SpriteLayers")]
    [SerializeField] private SpriteLayer mainLayer;
    [SerializeField] private SpriteLayer backHandLayer;

    private IBasicSM basicSM;
    private IMountingAbilitySM mountingAbilitySM;

    private void Awake()
    {
        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{name} ¡X IBasicSM not found in parent.");
            return;
        }

        mountingAbilitySM = GetComponentInParent<IMountingAbilitySM>();
        if (mountingAbilitySM == null)
        {
            Debug.LogError($"{name} ¡X IMountingAbilitySM not found in parent.");
            return;
        }

        if (basicSM.StateClicked != null)
            basicSM.StateClicked.OnEnterState += StateClicked_OnEnterState;

        if (basicSM.StateGrabbed != null)
            basicSM.StateGrabbed.OnEnterState += StateGrabbed_OnEnterState;

        if (mountingAbilitySM.StateMounting != null)
            mountingAbilitySM.StateMounting.OnEnterState += StateMounting_OnEnterState;

        if (basicSM.StateSpawn != null)
            basicSM.StateSpawn.OnEnterState += StateSpawn_OnEnterState;

        if (basicSM.StateGrabbed != null)
            basicSM.StateGrabbed.OnExitState += StateGrabbed_OnExitState;
    }

    private void StateGrabbed_OnExitState(object sender, System.EventArgs e)
    {
        backHandLayer.SortingOrderPlus(-2);
    }

    private void StateSpawn_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.AddNewLayers(transform);
    }

    private void StateMounting_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.InsertLayersToParent(transform);
    }

    private void StateGrabbed_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(transform);
        backHandLayer.SortingOrderPlus(2);
    }

    private void StateClicked_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(transform);
    }

    public int GetObjectMainLayer()
    {
        return mainLayer.GetSpriteLayer();
    }
}
