using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureStateMachine : StateMachineBase
{
    [Header("Need Refernce")]
    [SerializeField] public BoundaryManagerVr2 boundaryManager;
    [SerializeField] public MouseInputVr2 mouseInput;
    [SerializeField] public MovementVr2 movement;
    [SerializeField] public FurnitureMountManager mountManager;
    [SerializeField] public FaceDirectionVr2 faceDirection;
    [SerializeField] public FurnitureLayerManager layerManager;
    [SerializeField] public RaycastManagerVr2 raycastManager;

    //States
    [Header("States")]
    [SerializeField] public StateBase stateSpawn;
    [SerializeField] public StateBase stateGrab;
    [SerializeField] public StateBase stateFall;
    [SerializeField] public StateBase stateIdle;
    [SerializeField] public StateBase stateMounted;
    [SerializeField] public StateBase stateKnockUp;
    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}
