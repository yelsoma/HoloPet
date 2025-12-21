using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Hover : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;

    [SerializeField] private float hoverCenter = 0.8f;   
    [SerializeField] private float popHight = 0.1f;  
    [SerializeField] private float popSpeed = 0.15f;
    [SerializeField] private float popSpeedChange = 0.4f;
    [SerializeField] private float maxOffsetFromCenter = 0.8f; 
    private float popSpeedNow;
    private float currentY;
    private float hoverTopY;
    private float hoverFallY; 
    private bool popDown;


    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");
        }

        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        }
        else
        {
            basicMod = ibasicMod.BasicMod;
        }
    }

    public override void Enter()
    {
        float groundY = basicMod.BoundaryMg.GetBotBoundary();
        currentY = stateMachine.transform.position.y;
        float centerY = groundY + hoverCenter;

        // Only reject if it's REALLY far
        if (Mathf.Abs(currentY - centerY) > maxOffsetFromCenter)
        {
            stateMachine.ChangeState(basicMod.StateInAir);
            return;
        }
        hoverTopY = currentY;
        hoverFallY  = currentY - popHight;
        popDown = true;
        popSpeedNow = 0f;
    }

    public override void StateUpdate()
    {
        currentY = stateMachine.transform.position.y;
        if (popDown)
        {
            if(currentY - hoverFallY > 0)
            {
                if (popSpeedNow <= popSpeed)
                {
                    popSpeedNow += popSpeedChange * Time.deltaTime;
                }
                else
                {
                    popSpeedNow = popSpeed;
                }
            }
            else
            {
                popSpeedNow -= popSpeedChange * Time.deltaTime;
                if (popSpeedNow <= 0)
                {
                    popSpeedNow = 0;
                    popDown = false;
                }
            }
            basicMod.PhysicsMg.MoveDown(popSpeedNow);
        }
        else
        {
            if (hoverTopY - currentY  > 0)
            {
                if (popSpeedNow <= popSpeed)
                {
                    popSpeedNow += popSpeedChange * Time.deltaTime;
                }
                else
                {
                    popSpeedNow = popSpeed;
                }
            }
            else
            {
                popSpeedNow -= popSpeedChange * Time.deltaTime;
                if(popSpeedNow <= 0)
                {
                    popSpeedNow = 0;
                    popDown = true;
                }
            }
            basicMod.PhysicsMg.MoveUp(popSpeedNow);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
