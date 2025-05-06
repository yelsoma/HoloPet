using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMountingAbility 
{
    public void SetIsMounting(bool isMounting);
    public bool GetIsMounting();
    public bool TrySetMount(IMountable mount);
    public IMountable GetMount();
}
