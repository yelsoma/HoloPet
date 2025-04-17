using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputVr2 : MonoBehaviour
{
    public event EventHandler <OnDragEventArgs> OnDrag;
    public event EventHandler OnRelease;
    public event EventHandler OnClick;
    public class OnDragEventArgs : EventArgs
    {
        public Vector2 mousePos;
    }
    public void Click()
    {
        OnClick?.Invoke(this, EventArgs.Empty);
    }

    public void Drag(Vector2 mouseVector2)
    {
        OnDrag?.Invoke(this, new OnDragEventArgs { mousePos = mouseVector2 });
    }

    public void Release()
    {
        OnRelease?.Invoke(this, EventArgs.Empty);
    }
}
