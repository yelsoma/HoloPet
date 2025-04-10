using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolStateMachine : StateMachineBase
{
    //Need Refernce
    [Header("Need Refernce")]
    [SerializeField] public BoundaryManager boundaryManager;
    [SerializeField] public MouseInput mouseInput;
    [SerializeField] public Movement movement;
    [SerializeField] public Transform mountTop;
    [SerializeField] public RaycastManager raycastManager;
    [SerializeField] public MountManager mountManager;
    [SerializeField] public LayerManager layerManager;
    [SerializeField] public ObjectData data;
    [SerializeField] public InteractManager interactManager;


    //States
    [Header("States")]
    [SerializeField] public StateBase stateGrab;
    [SerializeField] public StateBase stateFall;
    [SerializeField] public StateBase stateIdle;
    [SerializeField] public StateBase stateChooseARandom;
    [SerializeField] public StateBase stateWander;
    [SerializeField] public StateBase stateKnockUp;
    [SerializeField] public StateBase stateMounting;
    [SerializeField] public StateBase stateRage;
    [SerializeField] public StateBase stateSpawn;
    [SerializeField] public StateBase stateInteract;
    [SerializeField] public StateBase stateInteracted;
    [SerializeField] public StateBase stateFindTarget;
    [SerializeField] public StateBase stateHappyTalk;
    protected override StateBase SetFirstState()
    {
        return stateSpawn;
    }
}
