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
        [SerializeField] private RocketEffects effects;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private int currentStage;
        public RocketStatus RocketStatus => rocketStatus;
        

        public event Action<RocketPart> partAdded;
        
        public void Break()
        {
            rocketStatus = RocketStatus.Break;
            effects.ActiveBreak(ChangeSprite);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //GameRoot.Instance.GameMaster.ScipCurrentPhase();
                effects.StartFlight();
            }
        }

        private void ChangeSprite()
        {
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

                if (checkList.Values.All(x => x == 0))
                {
                    GameRoot.Instance.GameMaster.ScipCurrentPhase();
                    effects.StartFlight();
                }

                return;
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