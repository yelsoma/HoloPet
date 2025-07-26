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
    private Transform stateMachineTransform;

    private void Awake()
    {
        stateMachineTransform = transform.root;
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

        if(mainLayer == null)
        {
            Debug.LogError(stateMachineTransform + " mainLayer is missing on HumanoidLayerManager");
        }

        if(backHandLayer == null)
        {
            Debug.LogError(stateMachineTransform + " backHandLayer is missing on HumanoidLayerManager");
        }

        if (basicSM.StateClicked != null)
        {
            basicSM.StateClicked.OnEnterState += StateClicked_OnEnterState;
        }
        else
        {
            Debug.LogWarning(stateMachineTransform + "  is missing StateClicked on HumanoidLayerManager");          
        }

        if (basicSM.StateGrabbed != null)
        {
            basicSM.StateGrabbed.OnEnterState += StateGrabbed_OnEnterState;
        }
        else
        {
            Debug.LogWarning(stateMachineTransform + "  is missing StateGrabbed on HumanoidLayerManager");
        }
        
        if (mountingAbilitySM.StateMounting != null)
        {
            mountingAbilitySM.StateMounting.OnEnterState += StateMounting_OnEnterState;
        }
        else
        {
            Debug.LogWarning(stateMachineTransform + "  is missing StateMounting on HumanoidLayerManager");
        }
        
        if (basicSM.StateSpawn != null)
        {
            basicSM.StateSpawn.OnEnterState += StateSpawn_OnEnterState;
        }
        else
        {
            Debug.LogWarning(stateMachineTransform + "  is missing StateSpawn on HumanoidLayerManager");
        }
       
        if (basicSM.StateGrabbed != null)
        {
            basicSM.StateGrabbed.OnExitState += StateGrabbed_OnExitState;
        }
        else
        {
            Debug.LogWarning(stateMachineTransform + "  is missing StateGrabbed on HumanoidLayerManager");
        }
        
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
        return mainLayer.GetSpriteLayer();
    }
}
