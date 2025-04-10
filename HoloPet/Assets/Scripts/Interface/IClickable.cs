using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public interface IClickable 
{
    public void Click();
    public void Drag(Vector2 mouseVector2);
    public void Release();
}
