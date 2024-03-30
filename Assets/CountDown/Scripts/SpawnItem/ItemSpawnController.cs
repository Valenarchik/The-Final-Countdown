using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CountDown
{
    public class ItemSpawnController : MonoBehaviour
    {
        [SerializeField] private List<Transform> resourceSpawnPoints;
        [SerializeField] private List<Transform> detailSpawnPoints;
        [SerializeField] private List<GameObject> resourcePrefabs;

        [Space(10)] 
        [Header("Settings")] 
        [SerializeField] private float resourceSpawnChance;
        
        [Space(10)]
        [Header("Details")]
        [SerializeField] private GameObject detail1;
        [SerializeField] private GameObject detail2;
        [SerializeField] private GameObject detail3;
        [SerializeField] private GameObject detail4;

        private HashSet<Transform> resourceSpawnPointsPool;
        private HashSet<Transform> detailSpawnPointsPool;

        private void Awake()
        {
            resourceSpawnPointsPool = new HashSet<Transform>(resourceSpawnPoints);
            detailSpawnPointsPool = new HashSet<Transform>(detailSpawnPointsPool);
        }

        public void SpawnAllResources()
        {
            
        }

        private void SpawnResource()
        {
            
        }

        public void SpawnAllDetails()
        {
            
        }

        private void SpawnDetail()
        {
            
        }
    }
}