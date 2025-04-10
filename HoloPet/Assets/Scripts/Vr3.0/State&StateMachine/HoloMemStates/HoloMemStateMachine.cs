using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemStateMachine : StateMachineBase
{
    //Need Refernce
    [Header("Need Refernce")]
    [SerializeField] public BoundaryManagerVr2 boundaryManager;
    [SerializeField] public MouseInputVr2 mouseInput;
    [SerializeField] public MovementVr2 movement;
    [SerializeField] public RaycastManagerVr2 raycastManager;
    [SerializeField] public HoloMemMountManager mountManager;
    [SerializeField] public FaceDirectionVr2 faceDirection;
    [SerializeField] public HoloMemInteractManager interactManager;
    [SerializeField] public HoloMemLayerManager layerManager;


    //States
    [Header("States")]
    [SerializeField] public StateBase stateGrab;
    [SerializeField] public StateBase stateFall;
    [SerializeField] public StateBase stateIdle;
    [SerializeField] public StateBase stateChooseARandom;
    [SerializeField] public StateBase stateWander;
    [SerializeField] public StateBase stateKnockUp;
    [SerializeField] public StateBase stateMounting;
    [SerializeField] public StateBase stateSpawn;
    [SerializeField] public StateBase stateInteract;
    [SerializeField] public StateBase stateFindInteractTarget;
    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}
