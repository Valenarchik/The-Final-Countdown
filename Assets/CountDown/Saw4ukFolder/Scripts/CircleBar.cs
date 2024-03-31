using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleBar : MonoBehaviour
{
    public Image circleBar;
    private float maxValue;
    private float value;
    private Image bar;
    
    void Awake()
    {
        bar = circleBar;
    }

    public void SetDefault(float max)
    {
        maxValue = max;
        value = max;
        bar.fillAmount = 1;
    }

    public void SetSettings(float max, float current)
    {
        maxValue = max;
        value = current;
        bar.fillAmount = current/max;
    }

    public void AdjustCurrentValue(float adjust)
    {
        value += adjust;
        if(value < 0) value = 0;
        if(value > maxValue) value = maxValue;
        bar.fillAmount = value/maxValue;
    }
}
