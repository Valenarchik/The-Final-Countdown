using System;
using TMPro;
using UnityEngine;

namespace CountDown
{
    public class Item: MonoBehaviour
    {
        private Rigidbody2D rb2D;
        
        [SerializeField] private SpriteRenderer onGroundSpriteRenderer;
        [SerializeField] private SpriteRenderer onPlayerSpriteRenderer;

        [SerializeField] private TMP_Text costText;
        [SerializeField] private int cost;
        [SerializeField] private ItemState itemState;

        public Rigidbody2D Rb2D => rb2D;
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
            rb2D = GetComponent<Rigidbody2D>();
            if(cost == 0)
                costText.gameObject.SetActive(false);
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