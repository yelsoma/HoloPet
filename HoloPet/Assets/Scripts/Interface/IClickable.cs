using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    public void Click();
    public void Drag(Vector2 mouseVector2);
    public void Release();
    public bool GetIsNowClickable();
}
