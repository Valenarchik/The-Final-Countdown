using System;
using System.Collections;
using System.Collections.Generic;
using CountDown;
using UnityEngine;

public class OnEnableSound : MonoBehaviour
{
    [SerializeField] private SFXData sound;
    
    private void OnEnable()
    {
       SfxManager.Instance.Play(sound);
    }
}
