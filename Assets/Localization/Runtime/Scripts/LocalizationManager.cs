using System;
using UnityEngine;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance { get; private set; }
        public static LocalizationSettings LocalizationSettings => Instance?.localizationSettings;
        public static string Lang;
        
        [SerializeField] private LocalizationSettings localizationSettings;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(this);
        }

        public static event Action SwitchLangEvent;
    }
}
