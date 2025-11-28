using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemFXMod : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private HoloMemFX holoMemFX;
    public HoloMemFX HoloMemFX => holoMemFX;

    private void Awake()
    {
        if (holoMemFX == null)
        {
            Debug.LogError(transform.root.name + "forget to add holoMemFX in HoloMemFXMod");
        }
    }
}
