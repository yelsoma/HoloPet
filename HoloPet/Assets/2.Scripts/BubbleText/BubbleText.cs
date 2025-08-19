using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class BubbleText : MonoBehaviour
{
    [SerializeField] private Transform bubblePos;
    [SerializeField] private float popDuration = 0.25f;
    [SerializeField] private float targetScale = 1f; 
    [SerializeField]private Image iconImage;

    private Coroutine bubbleRoutine;

    public void PopUpTargetIcon( Sprite targetIcon ,float duration)
    {
        iconImage.sprite = targetIcon;
        gameObject.SetActive(true);
        if(bubbleRoutine == null)
        {
            bubbleRoutine = StartCoroutine(CoShowBubble(duration));
        }
    }

    private IEnumerator CoShowBubble(float duration)
    {
        transform.localScale = Vector3.zero;

        // Pop-out
        float timer = 0f;
        while (timer < popDuration)
        {
            timer += Time.deltaTime;
            float t = timer / popDuration;
            float scale = Mathf.Sin(t * Mathf.PI * 0.5f);
            transform.localScale = Vector3.one * Mathf.Lerp(0f, targetScale, scale);
            yield return null;
        }
        transform.localScale = Vector3.one * targetScale;

        // Stay visible with icon pulsing
        timer = 0f;
        float pulseAmplitude = 0.03f; // how much the icon grows/shrinks
        float pulseSpeed = 2f; // how fast it pulses
        Vector3 iconOriginalScale = Vector3.one;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float pulse = Mathf.Sin(timer * pulseSpeed * Mathf.PI * 2f) * pulseAmplitude;
            iconImage.transform.localScale = iconOriginalScale * (1f + pulse);
            yield return null;
        }
        iconImage.transform.localScale = iconOriginalScale;

        // Shrink back
        timer = 0f;
        while (timer < popDuration)
        {
            timer += Time.deltaTime;
            float t = timer / popDuration;
            float scale = Mathf.Sin(t * Mathf.PI * 0.5f);
            transform.localScale = Vector3.one * Mathf.Lerp(targetScale, 0f, scale);
            yield return null;
        }

        gameObject.SetActive(false);
        bubbleRoutine = null;
    }
}
