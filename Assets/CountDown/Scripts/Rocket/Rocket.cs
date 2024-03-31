using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

namespace CountDown
{
    public class Rocket: MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<RocketPartType, int> checkList;
        [SerializeField, ReadOnlyInspector] private RocketStatus rocketStatus;
        [SerializeField] private List<Sprite> stagesSprites;

        private int currentStage;
        public RocketStatus RocketStatus => rocketStatus;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public event Action<RocketPart> partAdded;
        
        public void Break()
        {
            rocketStatus = RocketStatus.Break;
            currentStage = 0;
            spriteRenderer.sprite = stagesSprites[0];
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
                NextStage();
                partAdded?.Invoke(rocketPart);
                Destroy(item.gameObject);
                
                if (checkList.Values.Count(x => x > 0) == 1)
                    GameRoot.Instance.GameMaster.ScipCurrentPhase();
            }
        }

        private void NextStage()
        {
            currentStage++;
            if(currentStage < stagesSprites.Count)
                spriteRenderer.sprite = stagesSprites[currentStage];
        }
    }
}