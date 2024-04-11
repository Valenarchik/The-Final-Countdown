using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CountDown;
using UnityEngine;
using UnityEngine.Serialization;

public class RadarTimer : MonoBehaviour
{
    [SerializeField] private ResourcesArrow[] resourcesArrow;

    [SerializeField] private float timerOnDetailsModifier;
    [SerializeField] private float timerOnCapsuleModifier;
    [SerializeField] private SFXData sound;
    [SerializeField] private Animator animator;
    
    [SerializeField] private float period = 15;
    [SerializeField] private float delay;
    private bool coroutineIsActive = true;

    private ResourceTypeForArrow currentState;

    private void Start()
    {
        animator.enabled = false;
        StartCoroutine(WaitingForDelay());
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
        period *= timerOnDetailsModifier;
    }

    public void ChooseCapsule()
    {
        currentState = ResourceTypeForArrow.Capsule;
        period *= timerOnCapsuleModifier;
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
        SfxManager.Instance.Play(sound);
        yield return new WaitForSeconds(period);
        coroutineIsActive = false;
    }

    private IEnumerator WaitingForDelay()
    {
        yield return new WaitForSeconds(delay);
        animator.enabled = true;
        StartCoroutine(WaitingForFirstTimerCoroutine());
    }

    private IEnumerator WaitingForFirstTimerCoroutine()
    {
        yield return new WaitForSeconds(period);
        coroutineIsActive = false;
    }
}
