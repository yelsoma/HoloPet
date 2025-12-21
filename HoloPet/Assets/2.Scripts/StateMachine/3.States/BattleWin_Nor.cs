using System.Collections;
using UnityEngine;

public class BattleWin_Nor : StateBase
{
    private StateMachineBase stateMachine;
    private BasicMod basicMod;

    [SerializeField] private float jumpUpPower = 3;
    [SerializeField] private int jumpCount = 3;

    private Coroutine jumpCoroutine;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");

        IBasicMod ibasicMod = GetComponentInParent<IBasicMod>();
        if (ibasicMod == null)
            Debug.LogError($"{transform.root.name} ¡X no basicSM found in parent.");
        else
            basicMod = ibasicMod.BasicMod;
    }

    public override void Enter()
    {
        jumpCoroutine = StartCoroutine(CoBattleWinJump());
    }

    public override void Exit()
    {
        if (jumpCoroutine != null)
            StopCoroutine(jumpCoroutine);
    }

    private IEnumerator CoBattleWinJump()
    {
        int jumpLeft = jumpCount;

        basicMod.PhysicsMg.SetJump(jumpUpPower);
        basicMod.PhysicsMg.ResetFall();

        while (jumpLeft > 0)
        {
            if (basicMod.PhysicsMg.KeepJump())
            {
                if (basicMod.BoundaryMg.CheckIsTopBounderyAndResetPos())
                {
                    basicMod.PhysicsMg.SetJump(0);
                }
            }
            else
            {
                basicMod.PhysicsMg.KeepFall();
                if (basicMod.BoundaryMg.CheckIsBotBounderyAndResetPos())
                {
                    jumpLeft--;
                    if (jumpLeft > 0)
                    {
                        basicMod.PhysicsMg.SetJump(jumpUpPower);
                        basicMod.PhysicsMg.ResetFall();
                    }
                }
            }

            yield return null;
        }

        stateMachine.ChangeState(basicMod.StateIdle);
    }
}
