using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicLayerManager : MonoBehaviour ,ILayerManager
{
    [Header("StateMachine")]
    private IBasicSM stateMachine;
    [Header("SpriteLayers")]
    [SerializeField] private SpriteLayer mainLayer;

    private void Awake()
    {
        stateMachine = GetComponentInParent<IBasicSM>();
        if (stateMachine == null)
        {
            Debug.Log(transform + "no IBasicSM for BasicLayerManager");
        }
        stateMachine.StateClicked.OnEnterState += StateClicked_OnEnterState;
        stateMachine.StateGrabbed.OnEnterState += StateGrabbed_OnEnterState;
        stateMachine.StateSpawn.OnEnterState += StateSpawn_OnEnterState;      
    }

    private void StateSpawn_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.AddNewLayers(transform);
    }

    private void StateGrabbed_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(transform);
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
