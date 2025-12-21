using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountPancake_Spawn : MonoBehaviour ,ISpawnable
{
    [Header("Tower Settings")]
    [SerializeField] private int enemyCount = 3;

    [Header("Enemies")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Weapons")]
    [SerializeField] private GameObject[] weaponPrefabs;

    [Header("Item Chance")]
    [Range(0f, 1f)]
    [SerializeField] private float holdItemChance = 0.7f;

    public IEnumerator SpawnRoutine()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
            yield break;

        if (enemyCount <= 0)
            yield break;

        Transform spawnTransform =
            Random.value > 0.5f
            ? GameController.Instance.StageSpawner.SpawnPointLeftTop
            : GameController.Instance.StageSpawner.SpawnPointRightTop;

        GameObject previousEnemy = null;

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyPrefab =
                enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            Vector3 spawnPos =
                spawnTransform.position + Vector3.up * (i * 0.5f);

            GameObject enemy =
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            // Mount on the previous enemy
            if (previousEnemy != null)
            {
                TryMount(enemy, previousEnemy);
            }

            // Try to give weapon with chance
            TryGiveWeapon(enemy);

            previousEnemy = enemy;
        }

        yield break;
    }

    private void TryMount(GameObject rider, GameObject mount)
    {
        var mountableMod = mount.GetComponent<IMountableMod>();
        var mountingAbilityMod = rider.GetComponent<IMountingAbilityMod>();
        var riderSM = rider.GetComponent<StateMachineBase>();

        if (mountableMod == null || mountingAbilityMod == null || riderSM == null)
            return;

        MountableManager mountableMg =
            mountableMod.MountableMod.MountableMg;

        MountingAbilityManager mountingAbilityMg =
            mountingAbilityMod.MountingAbilityMod.MountingAbilityMg;

        if (mountingAbilityMg.TrySetMount(mountableMg))
        {
            riderSM.ChangeState(
                mountingAbilityMod.MountingAbilityMod.StateMounting);
        }
    }

    private void TryGiveWeapon(GameObject target)
    {
        if (weaponPrefabs == null || weaponPrefabs.Length == 0)
            return;

        if (Random.value > holdItemChance)
            return;

        var holderMod = target.GetComponent<IItemHolderMod>();
        if (holderMod == null)
            return;

        GameObject weaponPrefab =
            weaponPrefabs[Random.Range(0, weaponPrefabs.Length)];

        GameObject weapon = Instantiate(weaponPrefab);

        var itemMod = weapon.GetComponent<IItemMod>();
        var itemSM = weapon.GetComponent<StateMachineBase>();

        if (itemMod == null || itemSM == null)
            return;

        itemMod.ItemMod.ItemMg.SetHolderMg(
            holderMod.ItemHolderMod.ItemHolderMg);

        itemSM.ChangeState(itemMod.ItemMod.StateHold);
    }
}
