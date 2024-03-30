using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CountDown
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private float maxValue;
        [SerializeField] private RectTransform timeBar;
        [SerializeField] private RectTransform timeLooseBar;

        [SerializeField] private float epsilon;

        [SerializeField] private float timeBarSmooth;
        [SerializeField] private float timeLooseBarSmooth;
        
        
        private float currentValue;

        private float? targetXScale;

        public void Initialize(float inMaxValue)
        {
            maxValue = inMaxValue;
            currentValue = maxValue;
        }

        public void SetValue(float value)
        {
            currentValue = value;
            targetXScale = value / maxValue;
        }

        private void LateUpdate()
        {
            if (targetXScale.HasValue)
            {
                var currentBarScale = timeBar.localScale;
                var barIsFinish = false;
                if (Mathf.Abs(timeBar.localScale.x - targetXScale.Value) < epsilon)
                {
                    timeBar.localScale = new Vector3(targetXScale.Value, currentBarScale.y, currentBarScale.z);
                    barIsFinish = true;
                }
                else
                {
                    timeBar.localScale = Vector3.Lerp(currentBarScale,
                        new Vector3(targetXScale.Value, currentBarScale.y, currentBarScale.z),
                        timeBarSmooth * Time.deltaTime);
                }
                
                var currentBarLooseScale = timeLooseBar.localScale;
                var barLooseIsFinish = false;
                if (Mathf.Abs(timeLooseBar.localScale.x - targetXScale.Value) < epsilon)
                {
                    timeLooseBar.localScale = new Vector3(targetXScale.Value, currentBarLooseScale.y, currentBarLooseScale.z);
                    barLooseIsFinish = true;
                }
                else
                {
                    timeLooseBar.localScale = Vector3.Lerp(currentBarLooseScale,
                        new Vector3(targetXScale.Value, currentBarLooseScale.y, currentBarLooseScale.z),
                        timeLooseBarSmooth * Time.deltaTime);
                }

                if (barIsFinish && barLooseIsFinish)
                    targetXScale = null;
            }
        }
    }
}