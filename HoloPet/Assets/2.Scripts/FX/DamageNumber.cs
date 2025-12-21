using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float lifeTime = 0.8f;

    private float timer;

    public void Init(float damage)
    {
        text.text = Mathf.RoundToInt(damage).ToString();
        timer = lifeTime;
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0f)
            Destroy(gameObject);
    }
}
