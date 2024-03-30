using System;
using UnityEngine;
using UnityEngine.Events;

namespace CountDown
{
    public class Player: MonoBehaviour
    {
        [SerializeField] private Item item;
        public Item Item => item;

        public UnityEvent<Item> PickUpItemEvent;
        public UnityEvent<Item> DropItemEvent; 
        public bool CanDropItem => item != null;
        public bool CanPickUpItem => item == null;

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
    }
}