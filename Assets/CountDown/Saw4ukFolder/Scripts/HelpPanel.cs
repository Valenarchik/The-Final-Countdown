using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text keyName;
    [SerializeField] private TMP_Text helpText;
    [SerializeField] private string keyString;
    
    private HelpPanelState currentState;
    private Dictionary<HelpPanelState, string> states;

    public HelpPanelState CurrentState => currentState;

    private void Awake()
    {
        states = new Dictionary<HelpPanelState, string>()
        {
            { HelpPanelState.DemonstaratingItem, "Поднять предмет" },
            { HelpPanelState.Hided, "" },
            { HelpPanelState.DemonstratingDrop, "Выбросить предмет" },
            { HelpPanelState.DemonstratingRocket, "Положить предмет в ракету" },
        };
        currentState = HelpPanelState.Hided;
        gameObject.SetActive(false);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void ReDraw(HelpPanelState helpPanelState)
    {
        if (helpPanelState != HelpPanelState.Hided)
        {
            gameObject.SetActive(true);
            keyName.text = keyString;
            helpText.text = states[helpPanelState];
        }
        else
        {
            gameObject.SetActive(false);
        }
        currentState = helpPanelState;
    }
}

public enum HelpPanelState
{
    Hided,
    DemonstaratingItem,
    DemonstratingDrop,
    DemonstratingRocket
}
