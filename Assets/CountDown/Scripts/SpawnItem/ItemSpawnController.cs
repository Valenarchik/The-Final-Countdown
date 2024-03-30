using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace CountDown
{
    public class ItemSpawnController : MonoBehaviour
    {
        [SerializeField] private List<Transform> resourceSpawnPoints;
        [SerializeField] private List<Transform> detailSpawnPoints;
        [SerializeField] private List<GameObject> resourcePrefabs;

        [Space(10)] 
        [Header("Settings")]
        [SerializeField] [Range(0,1f)] private int resourcesSpawnChance;
        [SerializeField] private int detailsCopiesCount;
        
        [Space(10)]
        [Header("Details")]
        [SerializeField] private List<GameObject> details;

        private HashSet<Transform> detailSpawnPointsPool;

        private void Awake()
        {
            detailSpawnPointsPool = new HashSet<Transform>(detailSpawnPoints);
        }

        public void SpawnAllResources()
        {
            foreach (var sp in resourceSpawnPoints)
            {
                SpawnResource(sp);
            }
        }

        private void SpawnResource(Transform spawnPoint)
        {
            if (Random.Range(0, 1f) < resourcesSpawnChance)
            {
                var resourcePrefab = GetRandomResource();
                SpawnItem(resourcePrefab, spawnPoint.position);
            }
        }

        private GameObject GetRandomResource()
        {
            return resourcePrefabs[Random.Range(0, resourcePrefabs.Count)];
        }

        public void SpawnAllDetails()
        {
            foreach (var detail in details)
            {
                SpawnDetail(detail);
            }
        }

        private void SpawnDetail(GameObject detailPrefab)
        {
            for (var i = 0; i < detailsCopiesCount; i++)
            {
                var detailSP = GetRandomSpawnPoint();
                detailSpawnPoints.Remove(detailSP);
                SpawnItem(detailPrefab, detailSP.position);
            }
        }

        private Transform GetRandomSpawnPoint()
        {
            return detailSpawnPoints[Random.Range(0, detailSpawnPointsPool.Count)];
        }

        private void SpawnItem(GameObject prefab, Vector3 pos)
        {
            var resourceTransform = Instantiate(prefab).transform;
            resourceTransform.position = pos;
        }
    }
}