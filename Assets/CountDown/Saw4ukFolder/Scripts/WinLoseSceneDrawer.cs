using System;
using System.Collections;
using System.Collections.Generic;
using CountDown;
using TMPro;
using UnityEngine;

public class WinLoseSceneDrawer : MonoBehaviour
{
    [SerializeField] private GameObject pers1;
    [SerializeField] private GameObject pers2;

    [SerializeField] private GameObject rip1;
    [SerializeField] private GameObject rip2;

    [SerializeField] private TMP_Text winText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;

    private void OnEnable()
    {
        scoreText.text = WinOrLoseScene.Score.ToString();
        timeText.text = WinOrLoseScene.TimeInMinutes.ToString("0:00");
        
        if (!WinOrLoseScene.IsWin)
        {
            winText.text = "Вы оба проиграли";
            rip1.gameObject.SetActive(true);
            rip2.gameObject.SetActive(true);
            pers1.gameObject.SetActive(false);
            pers2.gameObject.SetActive(false);
        }
        else
        {
            if (WinOrLoseScene.PlayerId == 0)
            {
                rip1.gameObject.SetActive(false);
                rip2.gameObject.SetActive(true);
                pers1.gameObject.SetActive(true);
                pers2.gameObject.SetActive(false);
                winText.text = "Победил игрок слева";
            }
            else if (WinOrLoseScene.PlayerId == 1)
            {
                rip1.gameObject.SetActive(true);
                rip2.gameObject.SetActive(false);
                pers1.gameObject.SetActive(false);
                pers2.gameObject.SetActive(true);
                winText.text = "Победил игрок справа";
            }
        }
    }
}
