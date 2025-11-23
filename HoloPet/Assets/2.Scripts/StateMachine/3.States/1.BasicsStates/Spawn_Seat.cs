using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Seat : StateBase
{
    private SeatSM stateMachine;
    [SerializeField] private bool isFaceRight;

    private void Awake()
    {
        stateMachine = GetComponentInParent<SeatSM>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }
    }

    public override void Enter()
    {
        // If spawnState call BoundaryManager here will have bug because of code order. BoundaryManager is also  set on start
        //So call it at lateupdate
        if (isFaceRight)
        {
            stateMachine.FaceDirectionManager.SetFaceRight();
        }
        else
        {
            stateMachine.FaceDirectionManager.SetFaceRight();
        }       
    }

    public override void StateUpdate()
    {
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
