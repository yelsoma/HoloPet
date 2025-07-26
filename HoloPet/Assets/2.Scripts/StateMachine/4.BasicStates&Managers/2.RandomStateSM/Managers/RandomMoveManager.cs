using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveManager : MonoBehaviour
{
    [SerializeField] private StateBase startState;
    [SerializeField] private RandomMoveChanceStruct[] randomMoveChanceStructs;
    [SerializeField] private float MaxWaitTime;
    [SerializeField] private float MinWaitTime;
    private Coroutine ChangeCountDown;
    private StateMachineBase stateMachine;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
        }

        if (startState!= null)
        {
            startState.OnEnterState += StartState_OnEnterState;
            startState.OnExitState += StartState_OnExitState;
        }
        else
        {
            Debug.LogError(stateMachine.transform.name + "'s RandomMoveMg startState is not set");
        }   
        
        if(randomMoveChanceStructs.Length == 0)
        {
            Debug.LogError(stateMachine.transform.name + "'s RandomMoveMg randomMoveChanceStructs is not set");
        }
    }

    private void StartState_OnEnterState(object sender, System.EventArgs e)
    {
        ChangeCountDown = StartCoroutine(CoStartCountDown());
    }

    private void StartState_OnExitState(object sender, System.EventArgs e)
    {
        StopCoroutine(ChangeCountDown);       
    }

    private IEnumerator CoStartCountDown()
    {
        float randomTime = UnityEngine.Random.Range(MinWaitTime, MaxWaitTime);
        yield return new WaitForSeconds(randomTime);
        stateMachine.ChangeState(GetRandomState());
    }

    private StateBase GetRandomState()
    {
        float addChance = 0;
        foreach(RandomMoveChanceStruct randomMoveChanceStruct in randomMoveChanceStructs)
        {
            addChance += randomMoveChanceStruct.RandomChance;
        }
        if(addChance >= 0)
        {
            float randomChanceInt = UnityEngine.Random.Range(0f, addChance);
            float chanceIntNow = 0;
            foreach (RandomMoveChanceStruct randomMoveChanceStruct in randomMoveChanceStructs)
            {
                chanceIntNow += randomMoveChanceStruct.RandomChance;
                if (chanceIntNow >= randomChanceInt)
                {
                    return randomMoveChanceStruct.RandomState;
                }
            }
        }
        return null;
    }
}
