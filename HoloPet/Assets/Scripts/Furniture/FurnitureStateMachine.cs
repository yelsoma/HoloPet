using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureStateMachine : StateMachineBase
{
    //Need Refernce
    [Header("Need Refernce")]
    public BoundaryManager boundaryManager;
    public MouseInput mouseInput;
    public Movement movement;
    public FaceDirection faceDirection;
    public MountManager mountManager;
    public LayerManager layerManager;
    public ObjectData data;
    public InteractManager interactManager;

    //States
    [Header("States")]
    public StateBase stateGrab;
    public StateBase stateFall;
    public StateBase stateIdle;
    public StateBase stateKnockUp;
    public StateBase stateMounted;
    public StateBase stateSpawn;
    public StateBase stateInteracted;
    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}
