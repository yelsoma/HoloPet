using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicLayerManager : MonoBehaviour ,ILayerManager
{
    [Header("SpriteLayers")]
    [SerializeField] private SpriteLayer mainLayer;
    private BasicMod basicMod;
    private Transform stateMachineTransform;

    private void Awake()
    {
        stateMachineTransform = GetComponentInParent<StateMachineBase>().transform;
        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        else
        {
            basicMod = ibasicMod.BasicMod;
        }
        basicMod.StateClicked.OnEnterState += StateClicked_OnEnterState;
        basicMod.StateGrabbed.OnEnterState += StateGrabbed_OnEnterState;
        basicMod.StateSpawn.OnEnterState += StateSpawn_OnEnterState;
        basicMod.StateDestroy.OnEnterState += StateDeSpawn_OnEnterState;
        if(mainLayer == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no BasicLayerSet In LayerManager");
        }
    }

    private void StateDeSpawn_OnEnterState(object sender, System.EventArgs e)
    {
        SpriteLayerCenter.RemoveLayers(stateMachineTransform);
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
