using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CountDown
{
    public class Player: MonoBehaviour
    {
        [SerializeField] private int score;
        [SerializeField] private Item item;
        private List<Collider> intersectingObjects;

        public UnityEvent<Item> PickUpItemEvent;
        public UnityEvent<Item> DropItemEvent;
        
        public bool CanDropItem => item != null;
        public bool CanPickUpItem => item == null;
        public Item Item => item;

        public int Score
        {
            get => score;
            set => score = value;
        }

        public IReadOnlyCollection<Collider> IntersectingObjects => intersectingObjects;

        private void DropItem()
        {
            var dropItem = item;
            item = null;
            
            transform.SetParent(null);
            
            DropItemEvent?.Invoke(dropItem);
        }

        private void DropItemInRocket(Rocket rocket)
        {
            var dropItem = item;
            item = null;
            rocket.PlaceItem(item);
            DropItemEvent?.Invoke(dropItem);
        }

        private void PickUpItem(Item item)
        {
            this.item = item;
            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
            PickUpItemEvent?.Invoke(item);
        }

        public void CheckIntersections()
        {
            if (CanPickUpItem)
            {
                foreach (var other in intersectingObjects
                             .OrderBy(c => (transform.position - c.transform.position).magnitude))
                {
                    var pickUpItem = other.GetComponent<Item>();
                    if (pickUpItem != null)
                    {
                        PickUpItem(pickUpItem);
                        return;
                    }
                }
            }

            if (CanDropItem)
            {
                var rocket = intersectingObjects
                    .Select(x => x.GetComponent<Rocket>())
                    .FirstOrDefault(x => x != null);

                if (rocket != null && rocket.CanPlaceItem(item))
                {
                    DropItemInRocket(rocket);
                }
                else
                {
                    DropItem();
                }
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            intersectingObjects.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            intersectingObjects.Remove(other);
        }
    }
}