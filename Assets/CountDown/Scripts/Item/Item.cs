using System;
using UnityEngine;

namespace CountDown
{
    public class Item: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer onGroundSpriteRenderer;
        [SerializeField] private SpriteRenderer onPlayerSpriteRenderer;
        
        [SerializeField] private string itemName;
        [SerializeField] private ItemState itemState;
        
        public string ItemName => itemName;

        public ItemState ItemState
        {
            get => itemState;
            set
            {
                itemState = value;
                ItemStateChanged(value);
            }
        }

        protected virtual void ItemStateChanged(ItemState state)
        {
            switch (state)
            {
                case ItemState.OnGround:
                    onPlayerSpriteRenderer.enabled = false;
                    onGroundSpriteRenderer.enabled = true;
                    break;
                case ItemState.OnPlayer:
                    onPlayerSpriteRenderer.enabled = true;
                    onGroundSpriteRenderer.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        protected virtual void Start()
        {
            ItemState = ItemState.OnGround;
        }
    }

    public enum ItemState
    {
        OnGround,
        OnPlayer
    }
}