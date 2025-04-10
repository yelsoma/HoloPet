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
    private MouseInputVr2 selectedOb;
    private float clickTime;
    private bool overClickTime;
    private bool overClickPos;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetSelectedOb();
            if (selectedOb != null)
            {
                GetMouseInatialPos();
                clickTime = 0f;
                overClickPos = false;
            }

        }
        if (Input.GetMouseButton(0))
        {
            if (selectedOb != null)
            {
                if (ClickTimeOver() || overClickPos)
                {
                    selectedOb.Drag(GetMouseWorldPosition());
                }
                if (!overClickPos)
                {
                    if (Vector2.Distance(mouseInatialPos, GetMouseWorldPosition()) >= 0.3f)
                    {
                        overClickPos = true;
                    }
                }
                if (overClickPos)
                {
                    selectedOb.Drag(GetMouseWorldPosition());
                }

            }

        }
        if (Input.GetMouseButtonUp(0))
        {

            if (selectedOb != null)
            {
                if (!ClickTimeOver())
                {
                    selectedOb.Click();
                    selectedOb.Release();
                }
                selectedOb.Release();
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
            if(collider2D.transform.TryGetComponent(out MouseInputVr2 mouseInput)&& collider2D.transform.TryGetComponent(out ILayerManager layerManager))
            {
                if(layerManager.GetLayerNow() >= layerNow)
                {
                    layerNow = layerManager.GetLayerNow();
                    selectedOb = mouseInput;
                }
            }
        }
    }
    private void ClearSelectedOb()
    {
        selectedOb = null;
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
