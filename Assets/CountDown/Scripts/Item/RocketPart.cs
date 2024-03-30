using UnityEngine;

namespace CountDown
{
    public class RocketPart : Item
    {
        [SerializeField] private RocketPartType rocketPartType;
        public RocketPartType RocketPartType => rocketPartType;
    }
}