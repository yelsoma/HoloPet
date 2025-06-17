using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct RandomMoveChanceStruct 
{
    [SerializeField] private StateBase randomState;
    [SerializeField] private float randomChance;

    public StateBase RandomState => randomState;
    public float RandomChance => randomChance;
}
