using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashTime = 0.2f;

    private SpriteRenderer[] spriteRenderers;
    private Material[] materials;

    private Coroutine damageFlashCo;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        Init();
    }

    private void Init()
    {
        materials = new Material[spriteRenderers.Length];
        for(int i = 0; i < materials.Length; i++)
        {
            materials[i] = spriteRenderers[i].material;
        }
    }

    private IEnumerator DamageFlasher()
    {
        SetFlashColor();
        float nowFlashAmount = 0f;
        float nowFlashTime = 0f;
        while(nowFlashTime < flashTime)
        {
            nowFlashTime += Time.deltaTime;
            nowFlashAmount = Mathf.Lerp(1f, 0f, nowFlashTime / flashTime);
            SetFlashAmount(nowFlashAmount);

            yield return null;
        }
    }

    private void SetFlashColor()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetColor("_FlashColor", flashColor);
        }
    }

    private void SetFlashAmount(float amount)
    {
        for(int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat("_FlashAmount", amount);
        }
    }

    public void StartDamageFlash()
    {
        damageFlashCo = StartCoroutine(DamageFlasher());
    }
}
