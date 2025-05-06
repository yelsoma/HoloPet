using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemStateMachine : StateMachineBase 
{
    [Header("Basic Managers")]
    [SerializeField] public BoundaryManager boundaryManager;
    [SerializeField] public FaceDirection faceDirection;
    [SerializeField] public Movement movement;
    [SerializeField] public RaycastManager raycastManager;

    [Header("HoloMem Managers")] 
    [SerializeField] public HoloMemInteractManager interactManager;
    [SerializeField] public HoloMemMountManager mountManager;
    [SerializeField] public HoloMemLayerManager layerManager;
    [SerializeField] public HoloMemInputManager inputManager;


    [Header("States")]
    [SerializeField] public StateBase stateBullied;
    [SerializeField] public StateBase stateBully;
    [SerializeField] public StateBase stateChooseARandom;
    [SerializeField] public StateBase stateFall;
    [SerializeField] public StateBase stateFollowTarget;
    [SerializeField] public StateBase stateGrab;
    [SerializeField] public StateBase stateHappyChat;
    [SerializeField] public StateBase stateHappyChatInteracted;
    [SerializeField] public StateBase stateIdle;
    [SerializeField] public StateBase stateKnockUp;
    [SerializeField] public StateBase stateMounting;
    [SerializeField] public StateBase stateMountingInteract;
    [SerializeField] public StateBase stateSpawn;
    [SerializeField] public StateBase stateThink;
    [SerializeField] public StateBase stateWander;

    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}
