using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tittle : MonoBehaviour
{
    [Header("Assign 3 SpriteRenderers")]
    public SpriteRenderer sprite1;
    public SpriteRenderer sprite2;
    public SpriteRenderer sprite3;

    [Header("Animation Settings")]
    public float fadeDuration = 1f;
    public float moveDuration = 1f;
    public float moveHeight = 2f; // distance from middle to top/bottom
    public float overshoot = 0.3f; // how much it goes below middle at the end

    private void Start()
    {
        // start invisible
        SetAlpha(sprite1, 0f);
        SetAlpha(sprite2, 0f);
        SetAlpha(sprite3, 0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            StartCoroutine(FadeAndBounce(sprite1));
        if (Input.GetKeyDown(KeyCode.Alpha2))
            StartCoroutine(FadeAndBounce(sprite2));
        if (Input.GetKeyDown(KeyCode.Alpha3))
            StartCoroutine(FadeAndBounce(sprite3));
    }

    private System.Collections.IEnumerator FadeAndBounce(SpriteRenderer sr)
    {
        if (sr == null) yield break;

        Vector3 middlePos = sr.transform.position;
        Vector3 topPos = middlePos + Vector3.up * moveHeight;
        Vector3 bottomPos = middlePos - Vector3.up * moveHeight;
        Vector3 overshootPos = middlePos - Vector3.up * overshoot;

        // 1) Fade in while moving from top ¡÷ bottom
        float elapsed = 0f;
        Color c = sr.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);

            // fade in
            c.a = t;
            sr.color = c;

            // move top ¡÷ bottom
            sr.transform.position = Vector3.Lerp(topPos, bottomPos, t);

            yield return null;
        }

        // 2) Move bottom ¡÷ middle
        elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / moveDuration);
            sr.transform.position = Vector3.Lerp(bottomPos, middlePos, t);
            yield return null;
        }

        // 3) Overshoot downward
        elapsed = 0f;
        while (elapsed < moveDuration * 0.5f)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / (moveDuration * 0.5f));
            sr.transform.position = Vector3.Lerp(middlePos, overshootPos, t);
            yield return null;
        }

        // 4) Settle back to middle
        elapsed = 0f;
        while (elapsed < moveDuration * 0.5f)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / (moveDuration * 0.5f));
            sr.transform.position = Vector3.Lerp(overshootPos, middlePos, t);
            yield return null;
        }

        // final state
        c.a = 1f;
        sr.color = c;
        sr.transform.position = middlePos;
    }

    private void SetAlpha(SpriteRenderer sr, float alpha)
    {
        if (sr == null) return;
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }
}
