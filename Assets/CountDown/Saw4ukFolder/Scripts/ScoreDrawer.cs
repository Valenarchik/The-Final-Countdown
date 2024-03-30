using System;
using System.Collections;
using System.Collections.Generic;
using CountDown;
using TMPro;
using UnityEngine;

public class ScoreDrawer : MonoBehaviour
{
    [SerializeField] private Player firstPlayer;
    [SerializeField] private Player secondPlayer;
    [SerializeField] private TMP_Text scoreAmount;
    
    private void FixedUpdate()
    {
        scoreAmount.text = (firstPlayer.Score + secondPlayer.Score).ToString();
    }
}
