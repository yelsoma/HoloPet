using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Hover : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;

    [SerializeField] private float hoverCenter = 0.8f;
    [SerializeField] private float springStrength = 20f;
    [SerializeField] private float damping = 6f;
    [SerializeField] private float stabilityThreshold = 0.03f;    // how close to center
    [SerializeField] private float velocityThreshold = 0.05f;      // how slow
    [SerializeField] private float stableTimeNeeded = 0.3f;        // how long before switching

    private float verticalVelocity;
    private float groundY;
    private float gravity;
    private float stableTimer;

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
        verticalVelocity = 0f;
        stableTimer = 0f;
        groundY = basicMod.BoundaryMg.GetBotBoundary();
        gravity = basicMod.PhysicsMg.GetGravity();
    }

    public override void StateUpdate()
    {
        float dt = Time.deltaTime;
        float currentY = stateMachine.transform.position.y;
        float targetY = groundY + hoverCenter;

        float displacement = targetY - currentY;

        // Falling naturally if above center
        if (currentY > targetY)
        {
            verticalVelocity -= gravity * dt;

            // Start braking ONLY when approaching center
            if (verticalVelocity < 0 && displacement >= 0f)
            {
                float accel = displacement * springStrength
                              - verticalVelocity * damping;
                verticalVelocity += accel * dt;
            }
        }
        else
        {
            float accel = displacement * springStrength
                          - verticalVelocity * damping;
            verticalVelocity += accel * dt;
        }

        // Apply movement
        if (verticalVelocity > 0)
            basicMod.PhysicsMg.MoveUp(verticalVelocity);
        else if (verticalVelocity < 0)
            basicMod.PhysicsMg.MoveDown(-verticalVelocity);

        // -------------------------------------------------------------
        // STABILITY CHECK (to transition to Wander)
        // -------------------------------------------------------------
        if (Mathf.Abs(displacement) < stabilityThreshold &&
            Mathf.Abs(verticalVelocity) < velocityThreshold)
        {
            stableTimer += dt;

            if (stableTimer >= stableTimeNeeded)
            {
                // Switch to Wander when stable
                stateMachine.ChangeState(basicMod.StateIdle);
                return;
            }
        }
        else
        {
            stableTimer = 0f;
        }
    }


    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
