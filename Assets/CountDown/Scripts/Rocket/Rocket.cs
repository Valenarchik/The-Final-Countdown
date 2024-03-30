using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace CountDown
{
    public class Rocket: MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<RocketPartType, int> checkList;
        [SerializeField, ReadOnlyInspector] private RocketStatus rocketStatus;
        public RocketStatus RocketStatus => rocketStatus;

        public void Break()
        {
            rocketStatus = RocketStatus.Break;
        }
        
        public bool CanPlaceItem(Item item)
        {
            if (item is Resource resource)
            {
                return true;
            }

            if (item is RocketPart rocketPart)
            {
                return checkList.TryGetValue(rocketPart.RocketPartType, out var count)
                       && count > 0;
            }
            return false;
        }

        public void PlaceItem(Player player, Item item)
        {
            if (item is Resource resource)
            {
                Destroy(item.gameObject);
                return;
            }

            if (item is RocketPart rocketPart)
            {
                checkList[rocketPart.RocketPartType]--;
                Destroy(item.gameObject);
                return;
            }
        }
    }
}