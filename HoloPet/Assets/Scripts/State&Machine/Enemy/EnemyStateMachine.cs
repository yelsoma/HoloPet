using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachineBase
{
    [Header("Basic Managers")]
    [SerializeField] public BoundaryManager boundaryManager;
    [SerializeField] public FaceDirection faceDirection;
    [SerializeField] public Movement movement;
    [SerializeField] public RaycastManager raycastManager;

    [Header("Enemy Managers")]
    [SerializeField] public EnemyInputManager inputManager;
    [SerializeField] public HoloMemMountManager mountManager;
    [Header("States")]
    [SerializeField] public StateBase stateFollowTarget;
    [SerializeField] public StateBase stateAttack;
    [SerializeField] public StateBase stateKnockBack;
    [SerializeField] public StateBase stateGrab;
    [SerializeField] public StateBase stateIdle;
    [SerializeField] public StateBase stateWander;
    [SerializeField] public StateBase stateFall;
    [SerializeField] public StateBase stateDeath;
    [SerializeField] public StateBase stateMounting;   
}
