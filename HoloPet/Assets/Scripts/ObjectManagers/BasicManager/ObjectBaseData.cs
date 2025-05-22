using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBaseData : MonoBehaviour
{
    [SerializeField] private ObjectNameEnum objectName;
    [SerializeField] private ObjectTypeEnum ObjectType;

    public ObjectNameEnum GetObjectName()
    {
        return objectName;
    }
    public ObjectTypeEnum GetObjectType()
    {
        return ObjectType;
    }  
}
