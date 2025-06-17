using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveManager : MonoBehaviour
{
    [SerializeField] private StateBase StartState;
    [SerializeField] private RandomMoveChanceStruct[] randomMoveChanceStructs;
    [SerializeField] private float MaxWaitTime;
    [SerializeField] private float MinWaitTime;
    private Coroutine ChangeCountDown;
    private StateMachineBase stateMachine;

    private void Awake()
    {
        StartState.OnEnterState += StartState_OnEnterState;
        StartState.OnExitState += StartState_OnExitState;
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
        {
            Debug.LogError($"{transform} ¡X no StateMachineBase found in parent.");
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
