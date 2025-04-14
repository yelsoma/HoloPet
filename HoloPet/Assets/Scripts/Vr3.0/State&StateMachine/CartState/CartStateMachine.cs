using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartStateMachine : StateMachineBase
{
    [Header("Need Refernce")]
    [SerializeField] public BoundaryManagerVr2 boundaryManager;
    [SerializeField] public MouseInputVr2 mouseInput;
    [SerializeField] public MovementVr2 movement;
    [SerializeField] public RaycastManagerVr2 raycastManager;
    [SerializeField] public FurnitureMountManager mountManager;
    [SerializeField] public FaceDirectionVr2 faceDirection;
    [SerializeField] public CartLayerManager layerManager;

    //States
    [Header("States")]
    [SerializeField] public StateBase stateSpawn;
    [SerializeField] public StateBase stateGrab;
    [SerializeField] public StateBase stateFall;
    [SerializeField] public StateBase stateIdle;
    [SerializeField] public StateBase stateDash;
    [SerializeField] public StateBase stateMounted;
    [SerializeField] public StateBase stateKnockUp;
    [SerializeField] public StateBase stateDashMaxSpeed;
    [SerializeField] public StateBase stateJump;
    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}
