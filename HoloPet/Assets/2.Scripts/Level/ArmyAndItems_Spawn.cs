using UnityEngine;
using System.Collections;

public class ArmyAndItems_Spawn : MonoBehaviour, ISpawnable
{
    [Header("Prefab Settings")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] itemPrefabs;

    [Header("Weapon Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float carryWeaponChance = 0.5f;

    [Header("Spawn Settings")]
    [SerializeField] private int spawnCount = 3;
    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private float xOffsetStep = 0.5f;

    public IEnumerator SpawnRoutine()
    {
        bool spawnFromLeft = Random.value > 0.5f;

        Transform spawnTransform =
            spawnFromLeft
            ? GameController.Instance.StageSpawner.SpawnPointLeftTop
            : GameController.Instance.StageSpawner.SpawnPointRightTop;

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnOne(spawnTransform, i, spawnFromLeft);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnOne(Transform spawnPoint, int index, bool spawnFromLeft)
    {
        if (enemyPrefabs.Length == 0)
            return;

        GameObject enemyPrefab =
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Vector3 pos = spawnPoint.position;

        int centerIndex = spawnCount / 2;
        float offset = (centerIndex - index) * xOffsetStep;

        if (!spawnFromLeft)
            offset = -offset;

        pos.x += offset;

        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);

        // Decide if this enemy carries a weapon
        if (itemPrefabs.Length == 0)
            return;

        if (Random.value > carryWeaponChance)
            return;

        GameObject itemPrefab =
            itemPrefabs[Random.Range(0, itemPrefabs.Length)];

        GameObject item = Instantiate(itemPrefab);

        var enemyHolder = enemy.GetComponent<IItemHolderMod>();
        var itemMod = item.GetComponent<IItemMod>();
        var itemSM = item.GetComponent<StateMachineBase>();

        if (enemyHolder == null || itemMod == null || itemSM == null)
            return;

        itemMod.ItemMod.ItemMg.SetHolderMg(
            enemyHolder.ItemHolderMod.ItemHolderMg);

        itemSM.ChangeState(itemMod.ItemMod.StateHold);
    }
}
