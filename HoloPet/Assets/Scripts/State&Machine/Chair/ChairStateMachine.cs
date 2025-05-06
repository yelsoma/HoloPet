using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairStateMachine : StateMachineBase
{
    [Header("Need Refernce")]
    [SerializeField] public BoundaryManager boundaryManager;
    [SerializeField] public ChairLayerManager layerManager;
    [SerializeField] public Movement movement;
    [SerializeField] public FurnitureMountManager mountManager;
    [SerializeField] public FaceDirection faceDirection;
    [SerializeField] public ChairInputManager inputManager;
    [SerializeField] public ChairInteractManager interactManager;

    //States
    [Header("States")]
    [SerializeField] public StateBase stateSpawn;
    [SerializeField] public StateBase stateGrab;
    [SerializeField] public StateBase stateFall;
    [SerializeField] public StateBase stateIdle;
    [SerializeField] public StateBase stateKnockUp;
    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}
