using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatManager : MonoBehaviour
{
    [SerializeField] private float attack;
    [SerializeField] private float attackSpeed;

    public float GetAttack()
    {
        return attack;
    }
    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
}
