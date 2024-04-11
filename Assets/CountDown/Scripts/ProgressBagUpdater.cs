using System;
using UnityEngine;

namespace CountDown
{
    public class ProgressBagUpdater: MonoBehaviour
    {
        [SerializeField] private GameObject[] progressBarVisualizeElements;
        private ProgressBar progressBar;
        private float delayInMinutes;
        private bool isActive;

        private void Awake()
        {
            progressBar = GetComponent<ProgressBar>();
        }

        private void Start()
        {
            foreach (var element in progressBarVisualizeElements)
                element.SetActive(false);
        }

        public void Activate()
        {
            isActive = true;
            delayInMinutes = GameRoot.Instance.GameMaster.TimeInMinutes;
            progressBar.Initialize(GameRoot.Instance.GameMaster.MaxTimeInMinutes - delayInMinutes);
            foreach (var element in progressBarVisualizeElements)
                element.SetActive(true);
        }

        private void Update()
        {
            if (isActive)
            {
                progressBar.SetValue(GameRoot.Instance.GameMaster.MaxTimeInMinutes
                                     - GameRoot.Instance.GameMaster.TimeInMinutes);
            }
        }
    }
}