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
        [Header("SpawnPoints")]
        [SerializeField] private List<Transform> resourceSpawnPoints;
        [SerializeField] private List<Transform> detailSpawnPoints;
        [SerializeField] private List<Transform> capsuleSpawnPoints;
        
        [Space(10)]
        [Header("Prefabs")]
        [SerializeField] private List<GameObject> resourcePrefabs;
        [SerializeField] private List<GameObject> detailsPrefabs;
        [SerializeField] private GameObject capsulePrefab;

        [Space(10)] 
        [Header("Settings")]
        [SerializeField] [Range(0,1f)] private float resourcesSpawnChance;
        [SerializeField] private int detailsCopiesCount;

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
            foreach (var detail in detailsPrefabs)
            {
                SpawnDetail(detail);
            }
        }

        private void SpawnDetail(GameObject detailPrefab)
        {
            if(detailSpawnPoints.Count == 0)
                return;
            for (var i = 0; i < detailsCopiesCount; i++)
            {
                var detailSP = GetRandomSpawnPoint(detailSpawnPoints);
                detailSpawnPoints.Remove(detailSP);
                SpawnItem(detailPrefab, detailSP.position);
            }
        }

        public void SpawnCapsule()
        {
            if(capsuleSpawnPoints.Count == 0)
                return;
            var capsuleSP = GetRandomSpawnPoint(capsuleSpawnPoints);
            SpawnItem(capsulePrefab, capsuleSP.position);
        }

        private Transform GetRandomSpawnPoint(List<Transform> pool)
        {
            if (pool.Count == 0)
                return null;
            return pool[Random.Range(0, pool.Count)];
        }

        private void SpawnItem(GameObject prefab, Vector3 pos)
        {
            var resourceTransform = Instantiate(prefab).transform;
            resourceTransform.position = pos;
        }
    }
}