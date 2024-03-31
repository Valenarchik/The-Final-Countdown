using System;
using UnityEngine;

namespace CountDown
{
    [CreateAssetMenu(fileName = "Two String Event", menuName = "Events/Bool")]
    public class BoolEventChannel : ScriptableObject
    {
        public event Action<bool> Raised;

        public void RaiseEvent(bool value)
        {
            Raised?.Invoke(value);
        }
    }

}
