using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Transform healthBarTransform;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fillImage;

    private bool healthBarEnabled = false;

    // Alpha Control 0 ~ 1
    private float fillAlpha = 0.3f;

    private void Awake()
    {
        healthBarTransform.gameObject.SetActive(false);
    }

    public void SetHealtBar(float sliderNormalize)
    {
        //ful health
        if(sliderNormalize >= 1)
        {
            if (healthBarEnabled)
            {
                healthBarTransform.gameObject.SetActive(false);
                healthBarEnabled = false;
            }
            return;
        }
        //no health
        if(sliderNormalize <= 0)
        {
            healthBarTransform.gameObject.SetActive(false);
            healthBarEnabled = false;
            return;
        }
        //not full health
        if(sliderNormalize <= 0.05f)
        {
            sliderNormalize = 0.05f;
        }
        if (!healthBarEnabled)
        {
            healthBarTransform.gameObject.SetActive(true);
            healthBarEnabled = true;           
        }
        healthBarSlider.value = sliderNormalize;

        //fill Color
        Color c = gradient.Evaluate(sliderNormalize);
        c.a = fillAlpha;
        fillImage.color = c;
    }
}
