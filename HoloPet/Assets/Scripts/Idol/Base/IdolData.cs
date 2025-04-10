using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolData : MonoBehaviour
{
    [SerializeField] private bool haveHp;
    [SerializeField] private int hpStart;
    [SerializeField] private string idolName;
    private int Hp;

    private void Start()
    {
        Hp = hpStart;
    }

    // Hp modify
    public void HpChange(int plusHp)
    {
        Hp += plusHp;
    }
    public int GetHpNow()
    {
        return Hp;
    }
    public bool GetHaveHp()
    {
        return haveHp;
    }
    public void HpReset()
    {
        Hp = hpStart;
    }
    public string GetIdolName()
    {
        return idolName;
    }
}
