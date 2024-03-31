using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CountDown;
using UnityEngine;

public class RadarTimer : MonoBehaviour
{
    [SerializeField] private ResourcesArrow[] resourcesArrow;

    [SerializeField] private float timerOnDetailsModifier;
    [SerializeField] private float timerOnCapsuleModifier;
    [SerializeField] private SFXData sound;
    
    private float seconds = 15;
    private bool coroutineIsActive = true;

    private ResourceTypeForArrow currentState;

    private void Start()
    {
        StartCoroutine(WaitingForFirstTimerCoroutine());
    }

    private void Update()
    {
        if (!coroutineIsActive)
        {
            StartCoroutine(RadarCoroutine());
        }
    }

    public void ChooseDetails()
    {
        currentState = ResourceTypeForArrow.Detail;
        seconds *= timerOnDetailsModifier;
    }

    public void ChooseCapsule()
    {
        currentState = ResourceTypeForArrow.Capsule;
        seconds *= timerOnCapsuleModifier;
    }
    
    
    private IEnumerator RadarCoroutine()
    {
        if (resourcesArrow.Length == 0)
            yield break;
        
        coroutineIsActive = true;
        foreach (var arrow in resourcesArrow)
        {
            arrow.Show(currentState);
        }
        yield return new WaitForSeconds(seconds);
        coroutineIsActive = false;
        SfxManager.Instance.Play(sound);
    }

    private IEnumerator WaitingForFirstTimerCoroutine()
    {
        yield return new WaitForSeconds(seconds);
        coroutineIsActive = false;
    }
}
