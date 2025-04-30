using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public event EventHandler <OnDragEventArgs> OnDrag;
    public event EventHandler OnRelease;
    public event EventHandler OnClick;
    public Vector2 mouseVector2;
    public HoloMemEventManager eventManager;
    private IInteractable interactable;
    public class OnDragEventArgs : EventArgs
    {
        public Vector2 mousePos;
    }
    public void Click()
    {
        eventManager.KnockUp();
       // OnClick?.Invoke(this, EventArgs.Empty);
    }

    public void Drag(Vector2 mouseVector2)
    {
        eventManager.Drag();
        this.mouseVector2 = mouseVector2;
        //OnDrag?.Invoke(this, new OnDragEventArgs { mousePos = mouseVector2 });
    }

    public void Release()
    {
        eventManager.ReleaseEvent();
        //OnRelease?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMouseVetor2()
    {
        return mouseVector2;
    }
}
