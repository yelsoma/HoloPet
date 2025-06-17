using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDataManager : MonoBehaviour
{
    [SerializeField] private ObjectNameEnum objectName;
    [SerializeField] private ObjectTypeEnum ObjectType;
    [SerializeField] private ObjectGangEnum ObjectGang;

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
}
