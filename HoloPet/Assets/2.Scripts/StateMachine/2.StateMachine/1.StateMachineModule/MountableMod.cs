using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountableMod : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private MountableManager mountableMg;
    public MountableManager MountableMg => mountableMg;

    private void Awake()
    {
        if (mountableMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add mountableMg in MountableMod");
        }
    }
}
