using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateMachineListManager 
{   
    [SerializeField] private List<StateMachineBase> allObjectsList = new();
    [SerializeField] private List<StateMachineBase> playerGangList = new();
    [SerializeField] private List<StateMachineBase> enemyGangList = new();
    [SerializeField] private List<StateMachineBase> tempItemList = new();
    [SerializeField] private StateMachineBase homeSM;

    public List<StateMachineBase> GetAllObjectList() => allObjectsList;
    public List<StateMachineBase> GetPlayerGangList() => playerGangList;
    public List<StateMachineBase> GetEnemyGangList() => enemyGangList;
    public List<StateMachineBase> GetTempItemList() => tempItemList;
    public StateMachineBase GetHomeSM() => homeSM;

    public void AddObjectTolist(StateMachineBase sm)
    {
        allObjectsList.Add(sm);
        if(sm.TryGetComponent<IBasicMod>(out IBasicMod iBasicMod))
        {
            var def = iBasicMod.BasicMod.ObjectDefinition;

            switch (def.ObjectGangEnum)
            {
                case ObjectGangEnum.Player:
                    playerGangList.Add(sm);
                    break;
                case ObjectGangEnum.Enemy:
                    enemyGangList.Add(sm);
                    break;
            }
            switch (def.ObjectCategory)
            {
                case ObjectCategoryEnum.Home:
                    homeSM = sm;
                    break;
                case ObjectCategoryEnum.Item_Temp:
                    tempItemList.Add(sm);
                    break;
            }
        }
        else
        {
            Debug.Log("no basicMod in " + sm.transform.name);
        }
    }
    public void RemoveObjectFromList(StateMachineBase stateMachine)
    {
        allObjectsList.Remove(stateMachine);
        if (stateMachine.TryGetComponent<IBasicMod>(out IBasicMod iBasicMod))
        {
            var def = iBasicMod.BasicMod.ObjectDefinition;
            switch (def.ObjectGangEnum)
            {
                case ObjectGangEnum.Player:
                    playerGangList.Remove(stateMachine);
                    break;
                case ObjectGangEnum.Enemy:
                    enemyGangList.Remove(stateMachine);
                    break;
            }
            switch (def.ObjectCategory)
            {
                case ObjectCategoryEnum.Home:
                    homeSM = stateMachine;
                    break;
                case ObjectCategoryEnum.Item_Temp:
                    tempItemList.Remove(stateMachine);
                    break;
            }
        }
        else
        {
            Debug.Log("no basicMod in " + stateMachine.transform.name);
        }
    }
}
