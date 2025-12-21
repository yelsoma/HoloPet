using UnityEngine;

public class RandomMoveManager : MonoBehaviour
{
    [SerializeField] private StateBase startState;
    [SerializeField] private RandomMoveChanceStruct[] randomMoveChanceStructs;
    [SerializeField] private float MaxWaitTime = 3f;
    [SerializeField] private float MinWaitTime = 1f;

    private float waitTimeNow;
    private bool startCountDown = false;
    private StateMachineBase stateMachine;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachineBase>();
        if (stateMachine == null)
            Debug.LogError($"{transform.root.name} ¡X no StateMachineBase found in parent.");

        if (randomMoveChanceStructs.Length == 0)
            Debug.LogError($"{stateMachine?.name}'s RandomMoveManager: randomMoveChanceStructs not set!");

        if (startState != null)
        {
            startState.OnEnterState += StartState_OnEnterState;
            startState.OnExitState += StartState_OnExitState;
        }
        else
            Debug.LogError($"{stateMachine?.name}'s RandomMoveManager: startState not set!");
    }

    private void Update()
    {
        if(startCountDown)
        {
            waitTimeNow -= Time.deltaTime;
            if (waitTimeNow <= 0f)
            {
                StateBase nextState = GetRandomState();
                if (nextState != null)
                {
                    stateMachine.ChangeState(nextState);
                }
                else
                {
                    Debug.LogWarning($"{stateMachine?.name}'s RandomMoveManager: No valid random state returned.");
                }
                if(waitTimeNow <= 0f)
                {
                    startCountDown = false;
                }
            }
        }
    }

    private void StartState_OnEnterState(object sender, System.EventArgs e)
    {
        waitTimeNow = Random.Range(MinWaitTime, MaxWaitTime);
        startCountDown = true;
    }

    private void StartState_OnExitState(object sender, System.EventArgs e)
    {
        startCountDown = false;
    }

    private StateBase GetRandomState()
    {
        float totalChance = 0f;
        foreach (var entry in randomMoveChanceStructs)
        {
            if (entry.RandomState != null)
                totalChance += entry.RandomChance;
        }

        if (totalChance <= 0f)
            return null;

        float pick = Random.Range(0f, totalChance);
        float cumulative = 0f;

        foreach (var entry in randomMoveChanceStructs)
        {
            if (entry.RandomState == null) continue;

            cumulative += entry.RandomChance;
            if (pick <= cumulative)
                return entry.RandomState;
        }

        return null;
    }
}
