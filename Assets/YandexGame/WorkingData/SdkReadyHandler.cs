using System;
using UnityEngine;
using UnityEngine.Events;

namespace YG
{
    public class SdkReadyHandler: MonoBehaviour
    {
        [field: SerializeField] public UnityEvent OnReady { get; private set; }

        private void OnEnable()
        {
            if (YandexGame.SDKEnabled)
            {
                OnReady.Invoke();
            }
            else
            { 
                YandexGame.GetDataEvent += OnGetDataEvent;
            }
        }
        
        private void OnDisable()
        {
            YandexGame.GetDataEvent -= OnGetDataEvent;
        }

        private void OnGetDataEvent()
        {
            OnReady?.Invoke();
        }
    }
}