using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefebSpawn : MonoBehaviour
{
    [SerializeField] private GameObject PressF;
    [SerializeField] private GameObject PressB;
    [SerializeField] private GameObject PressC;
    [SerializeField] private GameObject PressA;
    [SerializeField] private GameObject PressM;
    [SerializeField] private GameObject PressW;
    [SerializeField] private Camera mainCamera;
    void Update()
    {
        if(PressF != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(PressF, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
            }
        }

        if (PressB != null)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(PressB, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
            }
        }
        if (PressC != null)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(PressC, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
            }
        }
        if (PressA != null)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(PressA, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
            }
        }
        if (PressM != null)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(PressM, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
            }
        }
        if (PressW != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(PressW, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
            }
        }
    }
}
