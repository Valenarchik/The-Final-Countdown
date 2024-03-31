using System.Collections;
using System.Collections.Generic;
using DataStructures;
using UnityEngine;

namespace CountDown
{
    public class ExplosionController : Singleton<ExplosionController>
    {
        [SerializeField] private List<ParticleSystem> particles;

        public void ActiveExplodes()
        {
            foreach (var ps in particles)
            {
                ps.Play();
            }
        }
    }
}