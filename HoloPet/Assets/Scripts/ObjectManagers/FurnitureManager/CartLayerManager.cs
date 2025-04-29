using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartLayerManager : MonoBehaviour, ILayerManager
{
    [Header("StateMachine")]
    [SerializeField] private CartStateMachine stateMachine;
    [Header("SpriteLayers")]
    [SerializeField] private SpriteLayer objectLayer;

    private void Awake()
    {
        stateMachine.stateKnockUp.OnEnterState += StateKnockUp_OnEnterState;
        stateMachine.stateSpawn.OnEnterState += StateSpawn_OnEnterState;
        stateMachine.stateJump.OnEnterState += StateJump_OnEnterState;
        stateMachine.stateGrab.OnEnterState += StateGrab_OnEnterState;
    }

    private void StateGrab_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(transform);
    }

    private void StateJump_OnEnterState(object sender, System.EventArgs e)
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
