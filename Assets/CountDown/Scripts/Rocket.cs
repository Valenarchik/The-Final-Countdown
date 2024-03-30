using UnityEngine;

namespace CountDown
{
    public class Rocket: MonoBehaviour
    {
        [SerializeField] private RocketStatus rocketStatus;
        public RocketStatus RocketStatus => rocketStatus;
        
    }

    public enum RocketStatus
    {
        Normal,
        Break
    }
}