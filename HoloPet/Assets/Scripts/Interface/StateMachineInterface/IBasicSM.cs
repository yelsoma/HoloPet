using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBasicSM
{
    public BoundaryManager BoundaryManager { get; }
    public FaceDirection FaceDirection { get; }
    public Movement Movement { get; }
    public RaycastManager RaycastManager { get; }
    public ObjectBaseData BaseData { get; }
}
