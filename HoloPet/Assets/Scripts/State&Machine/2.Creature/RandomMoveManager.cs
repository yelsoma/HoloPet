using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveManager : MonoBehaviour
{
    private IRandomMoveSM randomMoveSM;

    private void Awake()
    {
        randomMoveSM = GetComponentInParent<IRandomMoveSM>();
        if (randomMoveSM == null)
        {
            Debug.LogError($"{transform} ¡X no IRandomMove found in SM.");
        }       
    }
    public StateBase GetRandomState()
    {
        StateBase randomState;
        if(randomMoveSM.RandomStates.Length > 0)
        {
            int randomMoveInt = UnityEngine.Random.Range(0, randomMoveSM.RandomStates.Length);
            randomState = randomMoveSM.RandomStates[randomMoveInt];
            return randomState;
        }
        else
        {            
            Debug.LogError($"{transform} ¡X nothig found in RandomStates.");
            return null;
        }
    }
}
