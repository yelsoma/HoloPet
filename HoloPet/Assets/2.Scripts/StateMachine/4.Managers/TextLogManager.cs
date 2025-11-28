using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class TextLogManager : MonoBehaviour
{
    [SerializeField] private Transform iconBubbleTransform;
    [SerializeField] private Transform textLogTransform;
    private float popDuration = 0.25f;
    private float targetScale = 1f;
    [SerializeField] private Image iconImage;
    [SerializeField] private Sprite[] sadAni;
    [SerializeField] private Sprite happyFace;
    [SerializeField] private float sadEmojiframeTime = 0.75f;

    private Coroutine sadAniRoutine;
    private Coroutine bubbleRoutine;

    private void Awake()
    {
        if(textLogTransform == null)
        {
            Debug.LogError(transform.root.name + " you forgot to add head Transform on TextLogMg");
        }
    }
    public void PopUpTargetIcon(Sprite targetIcon, float duration)
    {
        iconImage.sprite = targetIcon;
        
        iconBubbleTransform.gameObject.SetActive(true);
        if (bubbleRoutine == null)
        {
            bubbleRoutine = StartCoroutine(CoShowBubble(duration));
        }
    }
    public void PopUpHappyFace(float duration)
    {
        iconImage.sprite = happyFace;

        iconBubbleTransform.gameObject.SetActive(true);
        if (bubbleRoutine == null)
        {
            bubbleRoutine = StartCoroutine(CoShowBubble(duration));
        }
    }
    public void PopUpSadEmoji(float duration)
    {
        if(sadAniRoutine == null)
        {
            sadAniRoutine = StartCoroutine(CoSadAniStart(duration));
        }

        iconBubbleTransform.gameObject.SetActive(true);
        if (bubbleRoutine == null)
        {
            bubbleRoutine = StartCoroutine(CoShowBubble(duration));
        }
    }
    private IEnumerator CoSadAniStart(float duration)
    {
        float timeNow = 0f;
        int sadEmojiframeNow = 0;
        float frameTimer = 0f;

        while (timeNow < duration)
        {
            timeNow += Time.deltaTime;
            frameTimer += Time.deltaTime;

            if (frameTimer >= sadEmojiframeTime)
            {
                frameTimer = 0f;
                sadEmojiframeNow = (sadEmojiframeNow + 1) % sadAni.Length;
                iconImage.sprite = sadAni[sadEmojiframeNow];
            }

            yield return null;
        }

        sadAniRoutine = null;
    }

    private IEnumerator CoShowBubble(float duration)
    {
        iconBubbleTransform.localScale = Vector3.zero;
        // Pop-out
        float timer = 0f;
        while (timer < popDuration)
        {
            transform.position = textLogTransform.position;
            timer += Time.deltaTime;
            float t = timer / popDuration;
            float scale = Mathf.Sin(t * Mathf.PI * 0.5f);
            iconBubbleTransform.localScale = Vector3.one * Mathf.Lerp(0f, targetScale, scale);
            yield return null;
        }
        iconBubbleTransform.localScale = Vector3.one * targetScale;

        // Stay visible with icon pulsing
        timer = 0f;
        float pulseAmplitude = 0.03f; // how much the icon grows/shrinks
        float pulseSpeed = 2f; // how fast it pulses
        Vector3 iconOriginalScale = Vector3.one;

        while (timer < duration)
        {
            transform.position = textLogTransform.position;
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
            transform.position = textLogTransform.position;
            timer += Time.deltaTime;
            float t = timer / popDuration;
            float scale = Mathf.Sin(t * Mathf.PI * 0.5f);
            iconBubbleTransform.localScale = Vector3.one * Mathf.Lerp(targetScale, 0f, scale);
            yield return null;
        }

        iconBubbleTransform.gameObject.SetActive(false);
        bubbleRoutine = null;
    }
}
