using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDriveSM 
{
    public StateBase StateDrive { get; }
    public StateBase StateDirveMax { get; }
    public StateBase StateDirveJump { get; }
    public StateBase StateClickedNor { get; }
}
