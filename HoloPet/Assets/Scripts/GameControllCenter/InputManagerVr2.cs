using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerVr2 : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Vector2 mouseWorldPosition;
    private Vector2 mouseInatialPos;
    private Collider2D[] mousePointColliders = new Collider2D[10];
    private IClickable selectedClickable;
    private float clickTime;
    private bool overClickTime;
    private bool dragOverPos;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetSelectedOb();
            if (selectedClickable != null)
            {
                GetMouseInatialPos();
                clickTime = 0f;
                dragOverPos = false;
            }

        }
        if (Input.GetMouseButton(0))
        {
            if (selectedClickable != null)
            {
                if (ClickTimeOver() || dragOverPos)
                {
                    selectedClickable.Drag(GetMouseWorldPosition());
                }                      
                if (!dragOverPos)
                {
                    if (Vector2.Distance(mouseInatialPos, GetMouseWorldPosition()) >= 0.3f)
                    {
                        dragOverPos = true;
                    }
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {

            if (selectedClickable != null)
            {
                if (!ClickTimeOver() && dragOverPos == false)
                {
                    selectedClickable.Click();
                }
                selectedClickable.Release();
            }
            ClearSelectedOb();
            ClearMousePointColliders();
        }
    }
    private Vector2 GetMouseWorldPosition()
    {
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return mouseWorldPosition;
    }
    private Vector2 GetMouseInatialPos()
    {
        mouseInatialPos = mouseWorldPosition;
        return mouseInatialPos;
    }
    private Collider2D[] GetMousePositionCollider2Ds(Vector2 mouseWorldPosition)
    {
        mousePointColliders = Physics2D.OverlapPointAll(mouseWorldPosition);
        return mousePointColliders;
    }
    private void ClearMousePointColliders()
    {
        Array.Clear(mousePointColliders, 0, mousePointColliders.Length);
    }
    private void SetSelectedOb()
    {
        int layerNow = -32767;
        foreach (Collider2D collider2D in GetMousePositionCollider2Ds(GetMouseWorldPosition()))
        {
            // get IClickable , check is now clickable
            if (collider2D.transform.TryGetComponent(out IClickable clickable) && clickable.GetIsNowClickable())
            {
                //get ILayerManager , set the top layer one to selectedClickable
                if (collider2D.transform.TryGetComponent(out ILayerManager layerManager) && layerManager.GetObjectMainLayer() >= layerNow)
                {
                    layerNow = layerManager.GetObjectMainLayer();
                    selectedClickable = clickable;
                }
            }
        }
    }
    private void ClearSelectedOb()
    {
        selectedClickable = null;
    }
    private bool ClickTimeOver()
    {
        if (clickTime <= 0.3f)
        {
            overClickTime = false;
            clickTime += Time.deltaTime;
            return overClickTime;
        }
        overClickTime = true;
        return overClickTime;
    }
}
