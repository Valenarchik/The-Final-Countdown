using Localization;
using UnityEngine;
using YG;

public class LanguageChanger : MonoBehaviour
{
    private void Awake()
    {
        LocalizationManager.lang = YandexGame.lang;
    }

    private void OnEnable()
    {
        YandexGame.SwitchLangEvent += OnSwitchLangEvent;
    }

    private void OnDisable()
    {
        YandexGame.SwitchLangEvent -= OnSwitchLangEvent;
    }

    private void OnSwitchLangEvent(string lang)
    {
        LocalizationManager.Lang = lang;
    }

    [EditorButton]
    public void RU()
    {
        LocalizationManager.LangName = LangName.ru;
    }

    [EditorButton]
    private void EN()
    {
        LocalizationManager.LangName = LangName.en;
    }
    
    [EditorButton]
    private void TR()
    {
        LocalizationManager.LangName = LangName.tr;
    }
}
