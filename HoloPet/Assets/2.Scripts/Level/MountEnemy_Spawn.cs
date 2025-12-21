using System.Collections;
using UnityEngine;

public class MountEnemy_Spawn : MonoBehaviour, ISpawnable
{
    [Header("Mount (the one being mounted)")]
    [SerializeField] private GameObject[] mountEnemyPrefabs;
    [SerializeField] private GameObject[] mountItemPrefabs;

    [Header("Rider (the one mounting)")]
    [SerializeField] private GameObject[] riderEnemyPrefabs;
    [SerializeField] private GameObject[] riderItemPrefabs;

    [Header("Spawn Boss Point")]
    [SerializeField] private bool spawnInBossPoint = false;

    public IEnumerator SpawnRoutine()
    {
        if (mountEnemyPrefabs == null || mountEnemyPrefabs.Length == 0)
            yield break;

        if (riderEnemyPrefabs == null || riderEnemyPrefabs.Length == 0)
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

        // Spawn mount
        GameObject mountPrefab =
            mountEnemyPrefabs[Random.Range(0, mountEnemyPrefabs.Length)];

        GameObject mountEnemy =
            Instantiate(mountPrefab, spawnTransform.position, Quaternion.identity);

        // Spawn rider slightly above
        GameObject riderPrefab =
            riderEnemyPrefabs[Random.Range(0, riderEnemyPrefabs.Length)];

        Vector3 riderPos = spawnTransform.position + Vector3.up * 0.5f;

        GameObject riderEnemy =
            Instantiate(riderPrefab, riderPos, Quaternion.identity);

        // Try mount
        TryMount(riderEnemy, mountEnemy);

        // Give items separately
        TryGiveItem(mountEnemy, mountItemPrefabs);
        TryGiveItem(riderEnemy, riderItemPrefabs);

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

    private void TryGiveItem(GameObject target, GameObject[] itemList)
    {
        if (itemList == null || itemList.Length == 0)
            return;

        var holderMod = target.GetComponent<IItemHolderMod>();
        if (holderMod == null)
            return;

        GameObject itemPrefab =
            itemList[Random.Range(0, itemList.Length)];

        GameObject item = Instantiate(itemPrefab);

        var itemMod = item.GetComponent<IItemMod>();
        var itemSM = item.GetComponent<StateMachineBase>();

        if (itemMod == null || itemSM == null)
            return;

        itemMod.ItemMod.ItemMg.SetHolderMg(
            holderMod.ItemHolderMod.ItemHolderMg);

        itemSM.ChangeState(itemMod.ItemMod.StateHold);
    }
}
