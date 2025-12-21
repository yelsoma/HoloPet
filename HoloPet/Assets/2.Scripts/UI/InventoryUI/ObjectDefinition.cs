using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ObjectCategoryEnum
{
    Character,
    Furniture,
    Item,
    Item_Temp,
    Home,
    Boss
}
public enum ObjectGangEnum
{
    Player, 
    Enemy,
    Neutral
}
[CreateAssetMenu(menuName = "NewObject ID !!!")]
public class ObjectDefinition : ScriptableObject
{
    public string ObjectID;
    public string ObjectName;
    public Sprite ObjectIcon;
    public GameObject ObjectPrefab;
    public ObjectCategoryEnum ObjectCategory;
    public ObjectGangEnum ObjectGangEnum;

}
