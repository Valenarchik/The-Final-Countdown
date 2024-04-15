using System;
using UnityEngine;

namespace Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        private static LocalizationManager Instance;
        public static string lang;
        public static string Lang
        {
            get => lang;
            set
            {
                lang = value ?? throw new ArgumentNullException();
                SwitchLangEvent?.Invoke();
            }
        }

        public LocalizationSettings settings;
        public static LocalizationSettings Settings => Instance?.settings;

        public static event Action SwitchLangEvent;
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public static void SetLanguage(LangName langName)
        {
            Lang = LangMethods.LangName(langName);
        }

        private void OnValidate()
        {
            Instance = this;
        }
    }
}
