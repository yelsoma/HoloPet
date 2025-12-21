using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private BaseInventory inventory;
    [SerializeField] private Transform itemSlot;
    [SerializeField] private Transform slotContainer;
    [SerializeField] private float itemSlotCellSize =125;
    [SerializeField] private Button_UI saveAndExitButton;
    [SerializeField] private Button_UI clearSaveButton;
    [SerializeField] private Button_UI closePageButton;


    [SerializeField] private Button_UI levelUpPage;
    [SerializeField] private GameObject levelUpPageGO;
    [SerializeField] private Button_UI inventoryPage;
    [SerializeField] private GameObject inventoryPageGO;
    [SerializeField] private Button_UI worldPage;
    [SerializeField] private GameObject worldPageGO;

    [SerializeField] private Button_UI levelUpButton;
    [SerializeField] private Button_UI startBattleButton;
    [SerializeField] private Button_UI storeItemButton;
   
    [SerializeField] private TMP_Text levelNow;
    [SerializeField] private TMP_Text worldNow;
    [SerializeField] private TMP_Text levelUpNeedCoin;
    [SerializeField] private TMP_Text coinNow;
    [SerializeField] private TMP_Text economy;
    [SerializeField] private TMP_Text atkMutiplyer;
    [SerializeField] private TMP_Text hpMutiplyer;
    [SerializeField] private TMP_Text noPlayerOnScene;
    [SerializeField] private TMP_Text nothingInBox;
    private bool noPlayerTextOne;

    private void Start()
    {
        GameController.Instance.AFKManager.OnCoinGain += AFKManager_OnDataUpdate;
        GameController.Instance.Inventory.OnItemListChange += Inventory_OnItemListChange;
        GameController.Instance.AFKManager.OnWorldChange += AFKManager_OnWorldChange;
        UpdateLevelData();
        RefreshInventoryItem();
        UpdateWorldData();
        saveAndExitButton.ClickFunc = () =>
        {
            GameController.Instance.SaveSystem.Save();
            Application.Quit();
        };
        clearSaveButton.ClickFunc = () =>
        {
            GameController.Instance.ClearScene();
            GameController.Instance.SaveSystem.ClearSave();
            GameController.Instance.AFKManager.SetData(1, 1, 0);
            GameController.Instance.Inventory.InitializeInventory();
            GameController.Instance.SaveSystem.Save();
            RefreshInventoryItem();
            UpdateWorldData();
            UpdateLevelData();
        };
        levelUpPage.ClickFunc = () =>
        {
            levelUpPageGO.SetActive(true);
            inventoryPageGO.SetActive(false);
            worldPageGO.SetActive(false);
            SetMultiplier();
        };
        inventoryPage.ClickFunc = () =>
        {
            SetToInventoryPage();
        };
        worldPage.ClickFunc = () =>
        {
            levelUpPageGO.SetActive(false);
            inventoryPageGO.SetActive(false);
            worldPageGO.SetActive(true);
            SetNoPlayerText(false);
        };
        levelUpButton.ClickFunc = () =>
        {
            GameController.Instance.AFKManager.TryLevelUpHome();
            UpdateLevelData();
            SetMultiplier();
        };
        startBattleButton.ClickFunc = () =>
        {                      
            if(GameController.Instance.StateMachineListMg.GetPlayerGangList().Count > 1)
            {
                GameController.Instance.StartNextStage();
                gameObject.SetActive(false);
                SetNoPlayerText(false);
            }
            else
            {
                SetNoPlayerText(true);
            }
        };
        closePageButton.ClickFunc = () =>
        {
            inventory.SetUIActive(false);
        };
        storeItemButton.ClickFunc = () =>
        {
            GameController.Instance.StoreItem();
        };
    }

    private void SetNoPlayerText( bool active )
    {
        if (active)
        {
            if (noPlayerTextOne)
            {
                noPlayerOnScene.text = "You need to Summon someone first.";
                noPlayerTextOne = false;
            }
            else
            {
                noPlayerOnScene.text = "Everyone is resting in the Box.";
                noPlayerTextOne = true;
            }            
        }
        else
        {
            noPlayerOnScene.text = "";
        }
    }

    private void AFKManager_OnWorldChange(object sender, System.EventArgs e)
    {
        UpdateWorldData();
        UpdateLevelData();
    }

    private void UpdateWorldData()
    {
        int worldLevel = GameController.Instance.AFKManager.GetWorldLevel;
        int economyNow = GameController.Instance.AFKManager.GetAfkCoinGain();
        worldNow.text =  "World "+ worldLevel;
        economy.text = "Gain (" + economyNow  + ") Coin every Sec";
    }

    private void Inventory_OnItemListChange(object sender, System.EventArgs e)
    {
        RefreshInventoryItem();
    }

    private void AFKManager_OnDataUpdate(object sender, System.EventArgs e)
    {
        UpdateLevelData();
    }

    public void RefreshInventoryItem()
    {
        foreach (Transform itemSlotTransform in slotContainer)
        {
            if (itemSlotTransform == itemSlot) continue;
            Destroy(itemSlotTransform.gameObject);
        }
        int x = -4;
        int y = 2;
        foreach( var itemData in inventory.GetItemList())
        {          
            RectTransform itenSlotRectTransform = Instantiate(itemSlot ,slotContainer).GetComponent<RectTransform>();
            itenSlotRectTransform.gameObject.SetActive(true);
            itenSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => 
            {
                Instantiate(itemData.ObjectPrefab, Vector3.zero, Quaternion.identity);
                inventory.RemoveItemFromInventoryList(itemData);
            };
            itenSlotRectTransform.anchoredPosition = new Vector2(x* itemSlotCellSize,y* itemSlotCellSize);
            Image image = itenSlotRectTransform.Find("ItemImage").GetComponent<Image>();
            image.sprite = itemData.ObjectIcon;
            x++;
            if(x > 4)
            {
                x = -4;
                y --;
            }
        }
        if (inventory.GetItemList().Count == 0)
        {
            nothingInBox.text = "Nothing here ...";
        }
        else
        {
            nothingInBox.text = "";
        }
    }
    public void UpdateLevelData()
    {
        int coinPerSec = GameController.Instance.AFKManager.GetAfkCoinGain();
        int coin = GameController.Instance.AFKManager.GetCoin;
        coinNow.text = "Coin : " + coin + "  (+" + coinPerSec + ")";
        int level = GameController.Instance.AFKManager.GetHomeLevel;
        levelNow.text = "Level : " + level;
        levelUpNeedCoin.text = "need ("+ coin+ "/" + GameController.Instance.AFKManager.GetHomeLevelCost(GameController.Instance.AFKManager.GetHomeLevel).ToString() + ")";
    }
    public void SetToInventoryPage()
    {
        levelUpPageGO.SetActive(false);
        inventoryPageGO.SetActive(true);
        worldPageGO.SetActive(false);
    }
    private void SetMultiplier()
    {
        int atkFloat = (int)(100 * Mathf.Pow(1.08f, GameController.Instance.AFKManager.GetHomeLevel - 1));
        atkMutiplyer.text = "Attack (" + atkFloat + ") %";
        int hpFloat = (int)(100 * Mathf.Pow(1.09f, GameController.Instance.AFKManager.GetHomeLevel - 1));
        hpMutiplyer.text = "Health (" + hpFloat+ ") %";
    }
}
