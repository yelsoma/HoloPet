using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXMod : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private HeartFX heartFX;
    public HeartFX HeartFX => heartFX;
    [SerializeField] private DamageFlash flashFX;
    public DamageFlash FlashFX => flashFX;
    [SerializeField] private DamageNumberSpawner numberSpawner;
    public DamageNumberSpawner NumberSpawner => numberSpawner;

    private void Awake()
    {
        if (heartFX == null)
        {
            Debug.LogError(transform.root.name + "forget to add heartFX in FXMod");
        }
        if(flashFX == null)
        {
            Debug.LogError(transform.root.name + "forget to add flashFX in FXMod");
        }
        if(numberSpawner == null)
        {
            Debug.LogError(transform.root.name + "forget to add numberSpawner in FXMod");
        }
    }
}
