using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadarTimer : MonoBehaviour
{
    [SerializeField] private ResourcesArrow[] resourcesArrow;
    private int seconds = 15;
    private bool coroutineIsActive = true;

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

    private IEnumerator RadarCoroutine()
    {
        coroutineIsActive = true;
        foreach (var arrow in resourcesArrow)
        {
            arrow.Show();
        }
        yield return new WaitForSeconds(seconds);
        coroutineIsActive = false;
    }

    private IEnumerator WaitingForFirstTimerCoroutine()
    {
        yield return new WaitForSeconds(seconds);
        coroutineIsActive = false;
    }
}
