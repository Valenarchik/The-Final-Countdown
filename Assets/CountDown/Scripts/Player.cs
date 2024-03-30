using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CountDown
{
    public class Player: MonoBehaviour
    {
        [SerializeField] private Item item;
        private List<Collider> intersectingObjects;

        public UnityEvent<Item> PickUpItemEvent;
        public UnityEvent<Item> DropItemEvent;
        
        public bool CanDropItem => item != null;
        public bool CanPickUpItem => item == null;
        public Item Item => item;
        public IReadOnlyCollection<Collider> IntersectingObjects => intersectingObjects;

        public Item DropItem()
        {
            if (!CanDropItem)
                return null;
            
            var dropItem = this.item;
            item = null;
            DropItemEvent?.Invoke(dropItem);
            return dropItem;
        }

        public void PickUpItem(Item item)
        {
            if (item == null)
                return;
            
            if (!CanPickUpItem)
                return;
            
            this.item = item;
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
                        break;
                    }
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