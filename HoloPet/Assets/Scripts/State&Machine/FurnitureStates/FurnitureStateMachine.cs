using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureStateMachine : StateMachineBase
{
    [Header("Need Refernce")]
    [SerializeField] public BoundaryManager boundaryManager;
    //[SerializeField] public MouseInput mouseInput;
    [SerializeField] public Movement movement;
    [SerializeField] public FurnitureMountManager mountManager;
    [SerializeField] public FaceDirection faceDirection;
    //[SerializeField] public ObjectLayerManager layerManager;
    [SerializeField] public RaycastManager raycastManager;

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
