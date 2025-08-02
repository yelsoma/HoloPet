#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
//Tools
public partial class StateMachineMaker : MonoBehaviour
{
    private void ClearConsole()
    {
        var logEntries = Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        var clearMethod = logEntries?.GetMethod("Clear", BindingFlags.Static | BindingFlags.Public);
        clearMethod?.Invoke(null, null);
    }

    private void CreateFolderIfMissing()
    {
        Transform mgTf = transform.Find("Managers");
        if (mgTf == null)
        {
            GameObject child = new GameObject("Managers");
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            managerFolder = child;
            Log("Created child object: Managers");
        }
        else
        {
            managerFolder = mgTf.gameObject;
        }

        Transform statesTf = transform.Find("States");
        if (statesTf == null)
        {
            GameObject child = new GameObject("States");
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            statesFolder = child;
            Log("Created child object: States");
        }
        else
        {
            statesFolder = statesTf.gameObject;
        }
    }

    private GameObject FindPrefabByName(string prefabName)
    {
        string[] guids = AssetDatabase.FindAssets($"{prefabName} t:Prefab");

        if (guids.Length == 0)
        {
            Log($"No prefab found with name containing '{prefabName}'.", warn: true);
            return null;
        }

        if (guids.Length > 1)
        {
            Log($"Multiple prefabs found whose names contain '{prefabName}':", warn: true);
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Log($" ¡÷ {path}", warn: true);
            }
            return null;
        }

        string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
        if (prefab == null || prefab.name != prefabName)
        {
            Log($"Found prefab name mismatch or load failure: expected '{prefabName}', found '{prefab?.name}'.", warn: true);
            return null;
        }

        return prefab;
    }

    private void Log(string message, bool warn = false)
    {
        if (RGB)
        {
            string color = RainbowColors[rainbowIndex % RainbowColors.Length];
            rainbowIndex++;
            string formatted = $"<color={color}>{message}</color>";
            if (warn)
                Debug.LogWarning(formatted);
            else
                Debug.Log(formatted);
        }
        else
        {
            if (warn)
                Debug.LogWarning($"<color=#FF8080>{message}</color>");
            else
                Debug.Log(message);
        }
    }
}
#endif