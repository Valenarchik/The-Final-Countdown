using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CountDown
{
    public class SpawnPoint : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color =Color.red;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }
    }
}