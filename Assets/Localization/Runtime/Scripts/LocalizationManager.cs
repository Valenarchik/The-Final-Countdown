using System;
using UnityEngine;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance { get; private set; }

        private static string lang;
        public static string Lang
        {
            get => lang;
            set
            {
                lang = value ?? throw new ArgumentNullException();
                SwitchLangEvent?.Invoke();
            }
        }

        [SerializeField] private LocalizationSettings localizationSettings;
        public LocalizationSettings Settings => localizationSettings;

        public static event Action SwitchLangEvent;
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetLanguage(LangName langName)
        {
            Lang = LangMethods.LangName(langName);
        }

        private void OnValidate()
        {
            Instance = this;
        }
    }
}
