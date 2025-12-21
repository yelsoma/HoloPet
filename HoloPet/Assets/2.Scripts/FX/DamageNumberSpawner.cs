using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberSpawner : MonoBehaviour
{
    public static DamageNumberSpawner Instance;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private DamageNumber prefab;

    private void Awake()
    {
        Instance = this;
        if(spawnPoint == null)
        {
            Debug.LogError($"{transform.root.name} ¡X no spawnpoint in DamageNumberSpawner.");
        }
    }

    public void SpawnNumber(float damage)
    {
        var dn = Instantiate(prefab,spawnPoint.position, Quaternion.identity);
        dn.Init(damage);
    }
}
