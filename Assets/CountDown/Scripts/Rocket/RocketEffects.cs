using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CountDown
{
    public class RocketEffects : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        [Header("Break")] 
        [SerializeField] private List<ParticleSystem> breakParticles;
        [SerializeField] private float breakDuration = 3f;
        [SerializeField] private float changeSpriteTime = 2f;
        
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

        private IEnumerator BreakRocket(Action changeSpriteCallback)
        {
            yield return new WaitForSecondsRealtime(changeSpriteTime);
            changeSpriteCallback.Invoke();
        }
    }
}
