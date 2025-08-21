using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateVidioStart :StateBase
{
    private StateMachineBase stateMachine;
    private IBasicSM basicSM;
    [SerializeField] private float waitTime;
    private float waitTimeNow;
    [SerializeField] private float bubbleTime;
    #region AutoSetRef
    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }

        basicSM = GetComponentInParent<IBasicSM>();
        if (basicSM == null)
        {
            Debug.LogError($"{transform} ¡X no basicSM found in parent.");
        }
    }
    #endregion

    #region StateBase
    public override void Enter()
    {        
        waitTimeNow = waitTime;
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
    #endregion
    private IEnumerator CoStartMovie()
    {
        yield return new WaitForSeconds(1);
        basicSM.FaceDirectionMg.SetFaceLeft();
        yield return new WaitForSeconds(0.2f);
        basicSM.FaceDirectionMg.SetFaceRight();
    }
}
