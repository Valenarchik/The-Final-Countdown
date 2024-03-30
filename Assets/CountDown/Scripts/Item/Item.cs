using System;
using TMPro;
using UnityEngine;

namespace CountDown
{
    public class Item: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer onGroundSpriteRenderer;
        [SerializeField] private SpriteRenderer onPlayerSpriteRenderer;

        [SerializeField] private TMP_Text costText;
        [SerializeField] private int cost;
        [SerializeField] private ItemState itemState;
        
        public int Cost => cost;

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
                    gameObject.SetActive(true);
                    break;
                case ItemState.OnPlayer:
                    onPlayerSpriteRenderer.enabled = true;
                    onGroundSpriteRenderer.enabled = false;
                    gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }


        private void Awake()
        {
            costText.text = cost.ToString();
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