using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBasicSM
{
    // Managers
    public BoundaryManager BoundaryMg{ get; }
    public FaceDirectionManager FaceDirectionMg { get; }
    public PhysicsManager PhysicsMg { get; }
    public RaycastManager RaycastMg { get; }
    public BaseDataManager BaseDataMg { get; }
    public ClickableManager ClickableMg { get;}
    public ILayerManager LayerMg { get; }
    public ObjectStatManager ObjectStatMg { get; }

    // States
    public StateBase StateIdle { get; }
    public StateBase StateInAir { get; }
    public StateBase StateGrabbed { get; }
    public StateBase StateClicked { get; }
    public StateBase StateReleased { get; }
    public StateBase StateSpawn { get; }
    public StateBase StateDestroy { get; }
}
