using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartStateMachine : StateMachineBase
{
    [Header("Need Refernce")]
    [SerializeField] public BoundaryManager boundaryManager;
    [SerializeField] public FaceDirection faceDirection;
    [SerializeField] public MouseInput mouseInput;
    [SerializeField] public Movement movement;
    [SerializeField] public ObjectLayerManager layerManager;
    [SerializeField] public RaycastManager raycastManager;
    [SerializeField] public FurnitureMountManager mountManager;  
  
    //States
    [Header("States")]
    [SerializeField] public StateBase stateDash;
    [SerializeField] public StateBase stateDashMaxSpeed;
    [SerializeField] public StateBase stateFall;
    [SerializeField] public StateBase stateGrab;
    [SerializeField] public StateBase stateIdle;
    [SerializeField] public StateBase stateJump;
    [SerializeField] public StateBase stateKnockUp;
    [SerializeField] public StateBase stateMounted;
    [SerializeField] public StateBase stateSpawn;

    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}
