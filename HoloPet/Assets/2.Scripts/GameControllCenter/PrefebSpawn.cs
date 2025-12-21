using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrefebSpawn : MonoBehaviour
{
    [SerializeField] private GameObject PressF;
    [SerializeField] private GameObject PressB;
    [SerializeField] private AFKManager PressC;
    [SerializeField] private GameObject PressA;
    [SerializeField] private GameObject PressM;
    [SerializeField] private GameObject PressW;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Object PortalPrefab;
    [SerializeField] private TextMeshProUGUI slimeCountText;
    [SerializeField] private TextMeshProUGUI botanCountText;
    [SerializeField] private GameObject PressI;
    [SerializeField] private GameObject PressI2;

    void Update()
    {
        if (PressI != null)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                BattleMod battleMg = PressI.GetComponentInChildren<BattleMod>();
                battleMg.SetIsInBattle(true);
            }
        }
        if (PressI2 != null)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                BattleMod battleMg = PressI2.GetComponentInChildren<BattleMod>();
                battleMg.SetIsInBattle(true);
            }
        }

        if (PressF != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                GameObject newObj = Instantiate(PressF, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
            }
        }

        if (PressB != null)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                GameObject newObj = Instantiate(PressB, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
            }
        }
        if (PressC != null)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                PressC.AddCoin(PressC.GetAfkCoinGain()* 600);
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
