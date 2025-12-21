using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] private StateMachineListManager stateMachineListMg;
    public StateMachineListManager StateMachineListMg => stateMachineListMg;

    [SerializeField] private SaveSystem saveSystem;
    public SaveSystem SaveSystem => saveSystem;

    public bool IsBattleActive { get; private set; }

    [SerializeField] private AFKManager afkManager;
    public AFKManager AFKManager => afkManager;

    [SerializeField] private StageSpawner stageSpawner;
    public StageSpawner StageSpawner => stageSpawner;

    [SerializeField] private BaseInventory inventory;
    public BaseInventory Inventory => inventory;

    [SerializeField] private List<ObjectDefinition> deathPlayerList = new();
    public List<ObjectDefinition> DeathPlayerList => deathPlayerList;

    [SerializeField] private GameObject botan;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject spear;
    [SerializeField] private GameObject axe;

    [SerializeField] private GameObject lostUI;
    private void Awake()
    {
        Instance = this;
        stateMachineListMg = new StateMachineListManager();
        
    }
    private void Start()
    {
        MainBoundary.SetBoudery();
        lostUI.SetActive(false);
        SaveSystem.Load();
        SetIsBattleActive(false);
        afkManager.OnWorldChange += AfkManager_OnWorldChange;
    }

    private void AfkManager_OnWorldChange(object sender, EventArgs e)
    {
        if(afkManager.GetWorldLevel == 3)
        {
            Instantiate(spear);
        }
        if(afkManager.GetWorldLevel == 5)
        {
            Instantiate(axe);
        }
        if (afkManager.GetWorldLevel == 8)
        {           
            Instantiate(botan);
        }
        if(afkManager.GetWorldLevel == 12)
        {
            Instantiate(sword);
        }
        if (afkManager.GetWorldLevel == 21)
        {
            Instantiate(botan);
            Instantiate(spear);
        }
        if (afkManager.GetWorldLevel == 31)
        {
            Instantiate(botan);
        }
        if (afkManager.GetWorldLevel == 41)
        {
            Instantiate(botan);
            Instantiate(axe);
            Instantiate(spear);
        }
        if (afkManager.GetWorldLevel == 51)
        {
            Instantiate(botan);
        }
        if (afkManager.GetWorldLevel == 61)
        {
            Instantiate(botan);
            Instantiate(sword);
            Instantiate(sword);
        }
        if (afkManager.GetWorldLevel == 71)
        {
            Instantiate(botan);
            Instantiate(botan);
            Instantiate(botan);
            Instantiate(axe);
            Instantiate(sword);
            Instantiate(spear);
        }
        saveSystem.Save();
    }

    private void Update()
    {   
        if (IsBattleActive)
        {
            if (stageSpawner.GetIsSpawnEnd() && stateMachineListMg.GetEnemyGangList().Count == 0)
            {
                EndBattle();
                afkManager.IncreaseWorldLevel(); 
                ResetBattleField();
                //saveSystem.Save();
                return;
            }
            if (stateMachineListMg.GetPlayerGangList().Count <= 1)
            {
                EndBattle();
                stageSpawner.StopStage();
                lostUI.SetActive(true);
                return;
            }
        }      
    }
    public void StartBattle()
    {
        SetIsBattleActive(true);

        // Update all existing objects
        List<StateMachineBase> all = StateMachineListMg.GetAllObjectList();
        foreach (var sm in all)
        {
            if (sm.TryGetComponent<IBattleMod>(out IBattleMod battleMod))
            {
                battleMod.BattleMod.SetIsInBattle(IsBattleActive);
            }
        }
    }
    public void EndBattle()
    {
        SetIsBattleActive(false);

        // Update all existing objects
        List<StateMachineBase> all = StateMachineListMg.GetAllObjectList();
        foreach (var sm in all)
        {
            if (sm.TryGetComponent<IBattleMod>(out IBattleMod battleMod))
            {
                battleMod.BattleMod.SetIsInBattle(IsBattleActive);
            }
        }
    }
    public void ClearScene()
    {
        List<StateMachineBase> needDeleteSM = StateMachineListMg.GetAllObjectList()
               .Where(sm => sm != stateMachineListMg.GetHomeSM())
               .ToList();

        foreach (StateMachineBase sm in needDeleteSM)
        {
            sm.ChangeState(sm.GetComponent<IBasicMod>().BasicMod.StateDestroy);
        }
    }
    public void StartNextStage()
    {
        StartBattle();
        stageSpawner.StartStage(afkManager.GetWorldLevel-1);
    }
    public void ResetBattleField()
    {
        List<StateMachineBase> needDeleteSM = StateMachineListMg.GetEnemyGangList().ToList();
        foreach (StateMachineBase sm in needDeleteSM)
        {
            sm.ChangeState(sm.GetComponent<IBasicMod>().BasicMod.StateDestroy);
        }
        if(deathPlayerList.Count > 0)
        {
            Debug.Log(deathPlayerList.Count);
            foreach (ObjectDefinition objectDefinition in deathPlayerList)
            {
                Instantiate(objectDefinition.ObjectPrefab);
            }
            deathPlayerList.Clear();
            Debug.Log(deathPlayerList.Count);
        } 
    }
    public void ClearTempItem()
    {
        List<StateMachineBase> needDeleteSM = StateMachineListMg.GetTempItemList().ToList();
        foreach (StateMachineBase sm in needDeleteSM)
        {
            sm.ChangeState(sm.GetComponent<IBasicMod>().BasicMod.StateDestroy);
        }
    }
    public void StoreItem()
    {
        StateMachineBase homeSM = stateMachineListMg.GetHomeSM();
        BasicMod basicMod = homeSM.GetComponent<IBasicMod>().BasicMod;
        homeSM.ChangeState(basicMod.StateClicked);
    }
    private void SetIsBattleActive(bool isBattle)
    {
        IsBattleActive = isBattle;
    }
}
