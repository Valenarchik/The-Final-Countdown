using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CountDown
{
    public class RocketEffects : MonoBehaviour
    {
        private const string ACTIVE_KEY = "Active";
        [SerializeField] private Animator animator;
        
        [Header("Break")] 
        [SerializeField] private List<ParticleSystem> breakParticles;
        [SerializeField] private float breakDuration = 3f;
        [SerializeField] private float changeSpriteTime = 2f;

        [Space(10)] 
        [Header("Flight")] 
        [SerializeField] private List<ParticleSystem> flightParticles;
        [SerializeField] private float flightSpeed;
        [SerializeField] private float maxFlightSpeed;
        [SerializeField] private float flightAcceleration;

        [SerializeField] private float minParticleSpeed;
        [SerializeField] private float particleAccelerationRatio;

        private bool flightStart;

        public void ActiveBreak(Action changeSpriteCallback)
        {
            StartCoroutine(BreakRocket(changeSpriteCallback));
            foreach (var ps in breakParticles)
            {
                var main = ps.main;
                main.duration = breakDuration;
                
                ps.Play();
            }
        }

        public void StartFlight()
        {
            animator.SetBool(ACTIVE_KEY, true);
            foreach (var ps in flightParticles)
            {
                ps.Play();
            }

            flightStart = true;
        }

        private void Update()
        {
            if (flightStart)
            {
                foreach (var ps in flightParticles)
                {
                    var main = ps.main;
                    main.startSpeed = flightSpeed * particleAccelerationRatio;
                }
                transform.position += Vector3.up * (flightSpeed * Time.deltaTime);
                flightSpeed += flightAcceleration;
            }
        }

        private IEnumerator BreakRocket(Action changeSpriteCallback)
        {
            yield return new WaitForSecondsRealtime(changeSpriteTime);
            changeSpriteCallback.Invoke();
        }
    }
}
