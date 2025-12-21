using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private GameObject firstHome;
    private string Path => Application.persistentDataPath + "/save.json";
    public void Save()
    {     
        SaveFile saveFile = new SaveFile();
        foreach (var sm in GameController.Instance.StateMachineListMg.GetAllObjectList())
        {
            var basic = sm.GetComponent<IBasicMod>();

            ObjectSaveData data = new ObjectSaveData
            {
                definitionKey = basic.BasicMod.ObjectDefinition.ObjectID,
                x = sm.transform.position.x,
                y = sm.transform.position.y
            };

            saveFile.objects.Add(data);
        }

        BaseInventory inventory = GameController.Instance.Inventory;
        foreach (var item in inventory.GetItemList())
        {
            saveFile.inventoryItemIDs.Add(item.ObjectID);
        }

        saveFile.baseLevel = GameController.Instance.AFKManager.GetHomeLevel;
        saveFile.waveCleared = GameController.Instance.AFKManager.GetWorldLevel;
        saveFile.coins = GameController.Instance.AFKManager.GetCoin;

        string json = JsonUtility.ToJson(saveFile, true);

        File.WriteAllText(Path, json);

        Debug.Log("Saved to " + Path);
    }

    public void Load()
    {
        if (!File.Exists(Path))
        {
            // First-time startup
            Instantiate(firstHome);
            GameController.Instance.AFKManager.SetData(1, 1, 0);
            Save();
            return;
        }
        string json = File.ReadAllText(Path);
        SaveFile saveFile = JsonUtility.FromJson<SaveFile>(json);

        foreach (var data in saveFile.objects)
        {
            ObjectDefinition def = ObjectDatabase.GetDefinition(data.definitionKey);
            GameObject newObj = Object.Instantiate(def.ObjectPrefab);
            newObj.transform.position = new Vector2(data.x, data.y);
        }

        BaseInventory inventory = GameController.Instance.Inventory;
        inventory.ClearItemList();
        foreach (var id in saveFile.inventoryItemIDs)
        {
            ObjectDefinition def = ObjectDatabase.GetDefinition(id);
            if (def == null)
                continue;

            inventory.AddItemToInventoryList(def);
        }
        inventory.SetUIActive(false);


        GameController.Instance.AFKManager.SetData(saveFile.waveCleared, saveFile.baseLevel, saveFile.coins);
    }

    public void ClearSave()
    {
        if (File.Exists(Path))
        {
            File.Delete(Path);
            Debug.Log("Save cleared.");
        }
    }
}

public static class ObjectDatabase
{
    private static Dictionary<string, ObjectDefinition> dict;

    static ObjectDatabase()
    {
        var definitions = Resources.LoadAll<ObjectDefinition>("ObjectDefinitions");

        dict = new Dictionary<string, ObjectDefinition>();

        foreach (var def in definitions)
        {
            dict[def.ObjectID] = def;
        }
    }

    public static ObjectDefinition GetDefinition(string key)
    {
        if (dict.TryGetValue(key, out var def))
            return def;

        Debug.LogError("ObjectDefinition not found: " + key);
        return null;
    }
}
[System.Serializable]
public class ObjectSaveData
{
    public string definitionKey; // from your ObjectDefinition
    public float x;
    public float y;
}
[System.Serializable]
public class SaveFile
{
    public List<ObjectSaveData> objects = new();
    public List<string> inventoryItemIDs = new();
    public int baseLevel;
    public int waveCleared;
    public int coins;
}
