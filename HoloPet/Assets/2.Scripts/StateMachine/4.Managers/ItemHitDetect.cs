using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemHitDetect : MonoBehaviour
{
    public  EventHandler<HitEventArgs> OnTriggerHitBox;
    public class HitEventArgs : EventArgs
    {
        public Collider2D collider;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerHitBox?.Invoke(this, new HitEventArgs { collider = collision });
    }
}
