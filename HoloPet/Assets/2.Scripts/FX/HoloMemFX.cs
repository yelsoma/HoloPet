using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloMemFX : MonoBehaviour
{
    [SerializeField] private GameObject HeartParticalGameOb;
    [SerializeField] private float HeartParticalTime;
    private Coroutine heartParticalCoroutine;

    public void StartHeartPartical()
    {
        if(heartParticalCoroutine == null)
        {
            heartParticalCoroutine = StartCoroutine(CoHeartParticalStart());
        }
    }

    private IEnumerator CoHeartParticalStart()
    {
        HeartParticalGameOb.SetActive(true);
        yield return new WaitForSeconds(HeartParticalTime);
        HeartParticalGameOb.SetActive(false);
        heartParticalCoroutine = null;
    }
}
