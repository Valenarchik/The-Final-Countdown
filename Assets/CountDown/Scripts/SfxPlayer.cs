using System.Collections.Generic;
using UnityEngine;

namespace CountDown
{
    public class SfxPlayer: MonoBehaviour
    {
        [SerializeField] private List<SFXData> sfxList;

        public void Play(int index)
        {
            SfxManager.Instance.Play(sfxList[index]);
        }
    }
}