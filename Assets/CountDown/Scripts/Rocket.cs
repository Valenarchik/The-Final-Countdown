using UnityEngine;

namespace CountDown
{
    public class Rocket: MonoBehaviour
    {
        [SerializeField] private RocketStatus rocketStatus;
        public RocketStatus RocketStatus => rocketStatus;

        public bool CanPlaceItem(Item item)
        {
            return false;
        }

        public void PlaceItem(Item item)
        {
            
        }
    }

    public enum RocketStatus
    {
        Normal,
        Break
    }
}