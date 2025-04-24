using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemLayerManager : MonoBehaviour ,ILayerManager
{
    [Header("StateMachine")]
    [SerializeField] private HoloMemStateMachine stateMachine;
    [Header("SpriteLayers")]
    [SerializeField] private SpriteLayer objectLayer;
    [SerializeField] private SpriteLayer backHandLayer;
    [Header("State")]
    [SerializeField] private HoloMemState_Grab stateGrab;
    [SerializeField] private HoloMemState_Spawn stateSpawn;


    private void Start()
    {
        stateMachine.stateKnockUp.OnEnterState += StateKnockUp_OnEnterState;
        stateMachine.stateGrab.OnEnterState += StateGrab_OnEnterState;
        stateMachine.stateMounting.OnEnterState += StateMounting_OnEnterState;
        stateGrab.OnExitGrab += StateGrab_OnExitGrab;
        stateSpawn.OnSpawn += StateSpawn_OnSpawn;
    }

    private void StateSpawn_OnSpawn(object sender, System.EventArgs e)
    {
        Debug.Log(transform);
        SpriteLayerCenter.AddNewLayers(transform);
    }

    private void StateGrab_OnExitGrab(object sender, System.EventArgs e)
    {
        backHandLayer.SortingOrderPlus(-2);
    }

    private void StateMounting_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(transform);
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

    public int GetObjectLayer()
    {
        return objectLayer.GetSpriteLayer();
    }
}
