using System.Collections;
using UnityEngine;

public class SingleEnemy_Spawn : MonoBehaviour, ISpawnable
{
    [Header("Enemy")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Optional Items")]
    [SerializeField] private GameObject[] itemPrefabs;

    [Header("Spawn Boss Point")]
    [SerializeField] private bool spawnInBossPoint = false;

    public IEnumerator SpawnRoutine()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
            yield break;

        Transform spawnTransform;

        if (spawnInBossPoint)
        {
            spawnTransform = GameController.Instance.StageSpawner.SpawnPointBoss;
        }
        else
        {
            spawnTransform =
                Random.value > 0.5f
                ? GameController.Instance.StageSpawner.SpawnPointLeft
                : GameController.Instance.StageSpawner.SpawnPointRight;
        }

        GameObject enemyPrefab =
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject enemy =
            Instantiate(enemyPrefab, spawnTransform.position, Quaternion.identity);

        // Try to spawn an item only if the list is not empty
        if (itemPrefabs != null && itemPrefabs.Length > 0)
        {
            GameObject itemPrefab =
                itemPrefabs[Random.Range(0, itemPrefabs.Length)];

            GameObject item = Instantiate(itemPrefab);

            var enemyHolder = enemy.GetComponent<IItemHolderMod>();
            var itemMod = item.GetComponent<IItemMod>();
            var itemSM = item.GetComponent<StateMachineBase>();

            if (enemyHolder != null && itemMod != null && itemSM != null)
            {
                itemMod.ItemMod.ItemMg.SetHolderMg(
                    enemyHolder.ItemHolderMod.ItemHolderMg);

                itemSM.ChangeState(itemMod.ItemMod.StateHold);
            }
        }

        yield break;
    }
}