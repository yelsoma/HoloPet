using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRandomMoveSM
{
    public StateBase[] RandomStates { get; }
    public RandomMoveManager randomMoveManager { get; }
}
