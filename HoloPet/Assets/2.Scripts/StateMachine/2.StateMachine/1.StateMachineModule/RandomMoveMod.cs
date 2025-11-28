using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveMod : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private RandomMoveManager randomMoveMg;
    public RandomMoveManager RandomMoveMg => randomMoveMg;

    private void Awake()
    {
        if (randomMoveMg == null)
        {
            Debug.LogError(transform.root.name + "forget to add randomMoveMg in RandomMoveMod");
        }
    }
}
