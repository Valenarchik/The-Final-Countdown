using System;
using System.Collections;
using System.Collections.Generic;
using CountDown;
using UnityEngine;
using UnityEngine.UI;

public class RocketPartPanel : MonoBehaviour
{
    [SerializeField] private Rocket rocket;

    [SerializeField] private Sprite yesSprite;
    [SerializeField] private Image BattarySprite;
    [SerializeField] private Image WiresSprite;
    [SerializeField] private Image ListsSprite;
    [SerializeField] private Image SteelSprite;
    

    private void Awake()
    {
        rocket.partAdded += RocketOnpartAdded;
    }

    private void RocketOnpartAdded(RocketPart obj)
    {
        switch (obj.RocketPartType)
        {
            case RocketPartType.Wires:
                WiresSprite.sprite = yesSprite;
                break;
            case RocketPartType.Battery:
                BattarySprite.sprite = yesSprite;
                break;
            case RocketPartType.Frame:
                ListsSprite.sprite = yesSprite;
                break;
            case RocketPartType.Sheathing:
                SteelSprite.sprite = yesSprite;
                break;
        }
    }
}
