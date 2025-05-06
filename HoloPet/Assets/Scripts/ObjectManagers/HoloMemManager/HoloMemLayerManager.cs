using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemLayerManager : MonoBehaviour ,ILayerManager
{
    [Header("StateMachine")]
    [SerializeField] private HoloMemStateMachine stateMachine;
    [Header("SpriteLayers")]
    [SerializeField] private SpriteLayer mainLayer;
    [SerializeField] private SpriteLayer backHandLayer;
    [Header("State")]
    [SerializeField] private HoloMemState_Grab stateGrab;


    private void Awake()
    {
        stateMachine.stateKnockUp.OnEnterState += StateKnockUp_OnEnterState;
        stateMachine.stateGrab.OnEnterState += StateGrab_OnEnterState;
        stateMachine.stateMounting.OnEnterState += StateMounting_OnEnterState;
        stateMachine.stateSpawn.OnEnterState += StateSpawn_OnEnterState;
        stateGrab.OnExitGrab += StateGrab_OnExitGrab;
        stateMachine.stateHappyChatInteracted.OnEnterState += StateHappyChatInteracted_OnEnterState;
    }

    private void StateHappyChatInteracted_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.InsertLayersAfterTransform(transform, stateMachine.interactManager.GetInteracter().GetTransform());
    }

    private void StateSpawn_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.AddNewLayers(transform);
    }

    private void StateGrab_OnExitGrab(object sender, System.EventArgs e)
    {
        backHandLayer.SortingOrderPlus(-2);
    }

    private void StateMounting_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.InsertLayersToParent(transform);
    }

    private void StateGrab_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(transform);
        backHandLayer.SortingOrderPlus(2);
    }

    private void StateKnockUp_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(transform);
    }

    public int GetObjectMainLayer()
    {
        return mainLayer.GetSpriteLayer();
    }
}
