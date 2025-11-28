using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicForV : StateBase
{

    private StateMachineBase stateMachine;
    private BasicMod basicMod;
    private AttackableMod attackableMod;
    private bool runRight;
    [SerializeField]private float speed;
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

        IAttackableMod iAttackableMod = GetComponentInParent<IAttackableMod>();
        if (iAttackableMod == null)
        {
            Debug.LogError($"{name} ¡X iAttackableMod not found in parent.");
        }
        else
        {
            attackableMod = iAttackableMod.AttackableMod;
        }
    }

    public override void Enter()
    {
        //if (attackableSM.AttackableMg.GetIsAttackerLeft())
        //{
        //    runRight = true;
        //    basicSM.FaceDirectionMg.SetFaceRight();
        //}
        //else
        //{
        //    runRight = false;
        //    basicSM.FaceDirectionMg.SetFaceLeft();
        //}
    }

    public override void StateUpdate()
    {
        if (basicMod.BoundaryMg.CheckIsLeftBounderyAndResetPos())
        {
            runRight = true;
            basicMod.FaceDirectionMg.SetFaceRight();
        }
        if (basicMod.BoundaryMg.CheckIsRightBounderyAndResetPos())
        {

            runRight = false;
            basicMod.FaceDirectionMg.SetFaceLeft();
        }
        if (runRight)
        {
            basicMod.PhysicsMg.MoveRight(speed);
        }
        else
        {
            basicMod.PhysicsMg.MoveLeft(speed);
        }
    }

    public override void StateLateUpdate()
    {
    }

    public override void Exit()
    {
    }
}
