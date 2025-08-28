using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDataManager : MonoBehaviour
{
    [SerializeField] private ObjectNameEnum objectName;
    [SerializeField] private ObjectTypeEnum ObjectType;
    [SerializeField] private ObjectGangEnum ObjectGang;
    [SerializeField] private Sprite icon;

    public ObjectNameEnum GetObjectName()
    {
        return objectName;
    }
    public ObjectTypeEnum GetObjectType()
    {
        return ObjectType;
    }  
    public ObjectGangEnum GetObjectGang()
    {
        return ObjectGang;
    }
    public Sprite GetIconSprite()
    {
        return icon;
    }
    private void Awake()
    {
        if (icon == null)
        {
            Debug.LogError(transform.root.name + " you forget to set object data icon");
        }
    }
}
