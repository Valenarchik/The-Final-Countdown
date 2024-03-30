using System;
using UnityEngine;

namespace CountDown
{
    public class ProgressBagUpdater: MonoBehaviour
    { 
        private ProgressBar progressBar;

        private void Awake()
        {
            progressBar = GetComponent<ProgressBar>();
        }

        private void Start()
        {
            progressBar.Initialize(GameRoot.Instance.GameMaster.MaxTimeInMinutes);
        }

        private void Update()
        {
            progressBar.SetValue(GameRoot.Instance.GameMaster.MaxTimeInMinutes - GameRoot.Instance.GameMaster.TimeInMinutes);
        }
    }
}