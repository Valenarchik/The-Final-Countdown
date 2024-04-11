using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CountDown
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private SpawnPointType type;
        
        private void OnDrawGizmos()
        {
            switch (type)
            {
                case SpawnPointType.Resource:
                    Gizmos.color =Color.green;
                    break;
                case SpawnPointType.Detail:
                    Gizmos.color =Color.magenta;
                    break;
                case SpawnPointType.Capsule:
                    Gizmos.color =Color.red;
                    break;
                default:
                    Gizmos.color = Color.blue;
                    break;
            }
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }
}