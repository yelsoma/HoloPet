using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicLayerManager : MonoBehaviour ,ILayerManager
{
    [Header("SpriteLayers")]
    [SerializeField] private SpriteLayer mainLayer;
    private IBasicSM basicSM;
    private Transform stateMachineTransform;

    private void Awake()
    {
        stateMachineTransform = transform.root;
        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.Log(transform + "no IBasicSM for BasicLayerManager");
        }
        basicSM.StateClicked.OnEnterState += StateClicked_OnEnterState;
        basicSM.StateGrabbed.OnEnterState += StateGrabbed_OnEnterState;
        basicSM.StateSpawn.OnEnterState += StateSpawn_OnEnterState;
        basicSM.StateDestroy.OnEnterState += StateDeSpawn_OnEnterState;
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
