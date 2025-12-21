using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveMod : MonoBehaviour
{
    [SerializeField] private StateBase stateDrive;
    [SerializeField] private StateBase stateDirveMax;
    [SerializeField] private StateBase stateDirveJump;
    [SerializeField] private StateBase stateClickedNor;

    public StateBase StateDrive => stateDrive;
    public StateBase StateDirveMax => stateDirveMax;
    public StateBase StateDirveJump => stateDirveJump;
    public StateBase StateClickedNor => stateClickedNor;
}
