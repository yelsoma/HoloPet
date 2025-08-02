using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpManager : MonoBehaviour
{
    [SerializeField] private int hpStart;
    private int hp;
    public event EventHandler OnHpZero;

    public void ResetHp()
    {
        hp = hpStart;
    }
    public void HpModify(int modifyInt)
    {
        hp += modifyInt;
        if(hp <= 0)
        {
            OnHpZero?.Invoke(this, EventArgs.Empty);
        }
    }
}
