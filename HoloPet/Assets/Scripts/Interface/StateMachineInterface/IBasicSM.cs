using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBasicSM
{
    // Managers
    public BoundaryManager BoundaryManager { get; }
    public FaceDirection FaceDirection { get; }
    public Movement Movement { get; }
    public RaycastManager RaycastManager { get; }
    public ObjectBaseData BaseData { get; }
    public ClickableManager ClickableManager { get;}
    public ILayerManager LayerManager { get; }

    // States
    public StateBase StateIdle { get; }
    public StateBase StateInAir { get; }
    public StateBase StateGrabbed { get; }
    public StateBase StateClicked { get; }
    public StateBase StateSpawn { get; }
}
