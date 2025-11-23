using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private Object PortalPrefab;
    private List<GameObject> slimeList = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI slimeCountText;
    private List<GameObject> botanList = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI botanCountText;
    [SerializeField] private GameObject PressI;

    void Update()
    {
        if (PressI != null)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                BattleManager battleMg = PressI.GetComponentInChildren<BattleManager>();
                battleMg.SetIsInBattle(true);
            }
        }

        if (PressF != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                GameObject newObj = Instantiate(PressF, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
                slimeList.Add(newObj);
                slimeCountText.text = "Slime Now: " + slimeList.Count;
            }
        }

        if (PressB != null)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                GameObject newObj = Instantiate(PressB, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
                botanList.Add(newObj);
                botanCountText.text = "Player Now: " + botanList.Count;
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
