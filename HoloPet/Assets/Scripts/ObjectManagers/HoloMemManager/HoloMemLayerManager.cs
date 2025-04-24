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


    private void Start()
    {
        stateMachine.stateKnockUp.OnEnterState += StateKnockUp_OnEnterState;
        stateMachine.stateSpawn.OnEnterState += StateSpawn_OnEnterState;
        stateMachine.stateGrab.OnEnterState += StateGrab_OnEnterState;
        stateMachine.stateMounting.OnEnterState += StateMounting_OnEnterState;
    }

    private void StateMounting_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(transform);
    }

    private void StateGrab_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(transform);
    }

    private void StateSpawn_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.AddNewLayers(transform);
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
