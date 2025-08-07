using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbilityManager : MonoBehaviour
{
    private List<AttackableManager> attackableManagers = new List<AttackableManager>();

    public bool TrySetAttackables(RaycastHit2D[] raycastHit2Ds)
    {
        ClearAttackables();
        if (raycastHit2Ds.Length > 0)
        {
            foreach(RaycastHit2D raycastHit2D in raycastHit2Ds)
            {
                IAttackableSM hitAttackableSM = raycastHit2D.transform.GetComponent<IAttackableSM>();
                if (hitAttackableSM != null && hitAttackableSM.AttackableMg.GetIsAttackableState())
                {
                    attackableManagers.Add(hitAttackableSM.AttackableMg);                   
                }
            }
        }
        if (attackableManagers.Count > 0)
        {
            return true;
        }
        return false;
    }

    public AttackableManager[] GetAttackables()
    {
        return attackableManagers.ToArray();
    }

    public void SetAttackablesKnockBackRight( bool isKnockRight , float knockBackPower)
    {
        foreach (AttackableManager attackableManager in attackableManagers)
        {
            attackableManager.KnockBackRight(isKnockRight,knockBackPower);
        }
    }
    public void ModifyAttackablesHp( int i)
    {
        foreach (AttackableManager attackableManager in attackableManagers)
        {
            attackableManager.HpModify(i);
        }
    }
    public void ClearAttackables()
    {
        attackableManagers.Clear();
    }
}
