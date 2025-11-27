using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMod : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private InteractableManager interactableMg;
    public InteractableManager InteractableMg => interactableMg;

    private void Awake()
    {
        if (interactableMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add interactableMg in InteractableMod");
        }
    }
}
