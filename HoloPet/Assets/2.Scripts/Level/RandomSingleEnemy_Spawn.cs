using System.Collections;
using UnityEngine;

public class RandomSingleEnemy_Spawn : MonoBehaviour, ISpawnable
{
    [Header("Spawn Control")]
    [SerializeField] private int spawnAmount = 5;
    [SerializeField] private float totalSpawnTime = 10f;

    //keep minimumInterval 0.3 intervalRandomOffset max to 1
    private float intervalRandomOffset = 1f;
    private float minimumInterval = 0.3f;

    [Header("Enemy")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Weapon")]
    [Range(0f, 1f)]
    [SerializeField] private float carryWeaponChance = 0.5f;
    [SerializeField] private GameObject[] weaponPrefabs;

    public IEnumerator SpawnRoutine()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
            yield break;

        if (spawnAmount <= 0 || totalSpawnTime <= 0f)
            yield break;

        float baseInterval = totalSpawnTime / spawnAmount;

        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnOne();

            if (i < spawnAmount - 1)
            {
                float offset =
                    Random.Range(-intervalRandomOffset, intervalRandomOffset);

                float wait = baseInterval * (1f + offset);
                wait = Mathf.Max(wait, minimumInterval);

                yield return new WaitForSeconds(wait);
            }
        }
    }

    private void SpawnOne()
    {
        Transform spawnPoint =
            Random.value > 0.5f
            ? GameController.Instance.StageSpawner.SpawnPointLeft
            : GameController.Instance.StageSpawner.SpawnPointRight;

        GameObject enemyPrefab =
            enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject enemy =
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Decide if this enemy carries a weapon
        if (weaponPrefabs == null || weaponPrefabs.Length == 0)
            return;

        if (Random.value > carryWeaponChance)
            return;

        GameObject weaponPrefab =
            weaponPrefabs[Random.Range(0, weaponPrefabs.Length)];

        GameObject weapon = Instantiate(weaponPrefab);

        var holder = enemy.GetComponent<IItemHolderMod>();
        var itemMod = weapon.GetComponent<IItemMod>();
        var itemSM = weapon.GetComponent<StateMachineBase>();

        if (holder == null || itemMod == null || itemSM == null)
            return;

        itemMod.ItemMod.ItemMg.SetHolderMg(
            holder.ItemHolderMod.ItemHolderMg);

        itemSM.ChangeState(itemMod.ItemMod.StateHold);
    }
}
