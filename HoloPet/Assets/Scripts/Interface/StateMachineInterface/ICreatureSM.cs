using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreatureSM
{
    public StateBase StateWander { get; }
    public StateBase StateRandomMove { get; }
}
