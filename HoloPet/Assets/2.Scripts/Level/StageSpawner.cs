using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawner : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private Transform spawnPointLeft;
    public Transform SpawnPointLeft => spawnPointLeft;

    [SerializeField] private Transform spawnPointRight;
    public Transform SpawnPointRight => spawnPointRight;

    [SerializeField] private Transform spawnPointBoss;
    public Transform SpawnPointBoss => spawnPointBoss;

    [SerializeField] private Transform spawnPointLeftTop;
    public Transform SpawnPointLeftTop => spawnPointLeftTop;

    [SerializeField] private Transform spawnPointRightTop;
    public Transform SpawnPointRightTop => spawnPointRightTop;

    [Header("Stages")]
    [SerializeField] private List<StageData> stages;

    private bool isSpawnEnd;
    public bool GetIsSpawnEnd() => isSpawnEnd;

    private int currentStageIndex;

    public void StartStage(int stageIndex)
    {
        StopAllCoroutines();

        if (stages == null || stages.Count == 0)
        {
            Debug.LogError("No stages configured.");
            return;
        }

        currentStageIndex = ((stageIndex % stages.Count) + stages.Count) % stages.Count;
        isSpawnEnd = false;

        StartCoroutine(StageRoutine(stages[currentStageIndex]));
    }

    private IEnumerator StageRoutine(StageData stage)
    {
        List<Coroutine> runningSpawns = new List<Coroutine>();

        foreach (var entry in stage.entries)
        {
            if (entry.spawnable is not ISpawnable spawnable)
            {
                Debug.LogWarning($"{entry.spawnable.name} does not implement ISpawnable.");
                continue;
            }

            // each entry schedules its OWN delayed start
            Coroutine c = StartCoroutine(RunSpawnEntry(entry, spawnable));
            runningSpawns.Add(c);
        }

        // wait until ALL spawnables finish
        foreach (var coroutine in runningSpawns)
        {
            yield return coroutine;
        }

        isSpawnEnd = true;
    }

    private IEnumerator RunSpawnEntry(StageSpawnEntry entry, ISpawnable spawnable)
    {
        if (entry.DelayTime > 0f)
            yield return new WaitForSeconds(entry.DelayTime);

        yield return StartCoroutine(spawnable.SpawnRoutine());
    }

    public void StopStage()
    {
        StopAllCoroutines();
        isSpawnEnd = true;
    }
}
[System.Serializable]
public class StageData
{
    public List<StageSpawnEntry> entries;
}
[System.Serializable]
public class StageSpawnEntry
{
    public MonoBehaviour spawnable;   // must implement ISpawnable
    public float DelayTime;          // delay BEFORE THIS spawn starts
}