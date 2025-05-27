using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Wander : StateBase
{
    [SerializeField] private EnemyStateMachine stateMachine;
    [SerializeField] private float wanderMaxTime;
    [SerializeField] private float wanderMinTime;
    [SerializeField] private float wanderSpeed;
    private float wanderTimer;
    private bool wanderDirIsRight;

    public override void Enter()
    {
        //event       

        //start
        if(Random.Range(0f,1f) > 0.5f)
        {
            wanderDirIsRight = true;
        }
        else
        {
            wanderDirIsRight = false;
        }
        wanderTimer = Random.Range(wanderMinTime, wanderMaxTime);
        //can do
    }
    public override void StateUpdate()
    {
        if (wanderDirIsRight)
        {
            stateMachine.movement.MoveRight(wanderSpeed);
        }
        else
        {
            stateMachine.movement.MoveLeft(wanderSpeed);
        }
        if(wanderTimer >= 0)
        {
            wanderTimer -= Time.deltaTime;
        }
        else
        {
            //exit to idle
            stateMachine.ChangeState(stateMachine.stateIdle);
        }
    }
    public override void StateLateUpdate()
    {

    }
    public override void Exit()
    {

    }
    // < Events >

    // < Package Method >

    //courutine

}
