using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text keyName;
    [SerializeField] private TMP_Text helpText;

    public void ReDraw(string keyName, string helpText)
    {
        this.keyName.text = keyName;
        this.helpText.text = helpText;
    }
}
