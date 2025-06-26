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
    private Transform stateMachineTransform;

    private void Awake()
    {
        stateMachineTransform = transform.root;
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
        SpriteLayerCenter.AddNewLayers(stateMachineTransform);
    }

    private void StateGrabbed_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(stateMachineTransform);
    }

    private void StateClicked_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.PullRootLayersToTop(stateMachineTransform);
    }

    public int GetObjectMainLayer()
    {
        return mainLayer.GetSpriteLayer();
    }
}
