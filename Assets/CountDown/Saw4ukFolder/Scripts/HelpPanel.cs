using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
    [SerializeField] private GameObject ButtonObject;
    [SerializeField] private GameObject DemonstratingItem;
    [SerializeField] private GameObject DemonstratingDrop;
    [SerializeField] private GameObject DemonstratingRocket;
    
    private HelpPanelState currentState;
    public HelpPanelState CurrentState => currentState;

    private void Awake()
    {
        currentState = HelpPanelState.Hided;
        ReDraw(currentState);
    }

    public void ReDraw(HelpPanelState helpPanelState)
    {
        ReStoreDefaults();
        switch (helpPanelState)
        {
            case HelpPanelState.Hided:
                break;
            case HelpPanelState.DemonstaratingItem:
                ButtonObject.SetActive(true);
                DemonstratingItem.SetActive(true);
                break;
            case HelpPanelState.DemonstratingDrop:
                ButtonObject.SetActive(true);
                DemonstratingDrop.SetActive(true);
                break;
            case HelpPanelState.DemonstratingRocket:
                ButtonObject.SetActive(true);
                DemonstratingRocket.SetActive(true);
                break;
        }

        currentState = helpPanelState;
    }

    private void ReStoreDefaults()
    {
        ButtonObject.SetActive(false);
        DemonstratingRocket.SetActive(false);
        DemonstratingItem.SetActive(false);
        DemonstratingDrop.SetActive(false);
    }
}

public enum HelpPanelState
{
    Hided,
    DemonstaratingItem,
    DemonstratingDrop,
    DemonstratingRocket
}
