using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CountDown
{
    public class Player: MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private string inputId;
        [SerializeField] private KeyCode interactionKey;
        
        [Header("Speed")]
        [SerializeField] private float speed = 20;
        [SerializeField, Range(0,1)] private float speedOnPickItemMultiplayer = 0.8f;
        
        [Header("Attack")]
        [SerializeField] private float attackForce = 100;
        
        [Header("References")]
        [SerializeField] private PlayerFightTrigger fightTrigger;
        
        [Header("Debug")]
        [SerializeField, ReadOnlyInspector] private int score;
        [SerializeField, ReadOnlyInspector] private Item item;
        
        private readonly List<Collider2D> intersectingObjects = new ();
        
        [Header("Events")]
        public UnityEvent<Item> PickUpItemEvent;
        public UnityEvent<Item> DropItemEvent;
        public UnityEvent<Item> PlacedToRocketEvent;
        
        public bool CanDropItem => item != null;
        public bool CanPickUpItem => item == null;
        public Item Item => item;

        public int Score
        {
            get => score;
            set => score = value;
        }
        public IReadOnlyCollection<Collider2D> IntersectingObjects => intersectingObjects;
        
        private Animator animator;
        private Rigidbody2D rb2D;
        
        private float horizontalInput;
        private float verticalInput;

        private Vector2 additionalVelocity;
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
            rb2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            fightTrigger.Player = this;
        }

        void Update()
        {
            //Debug.Log($"{rb2D.velocity} {name}");
            horizontalInput = Input.GetAxis("Horizontal" + inputId);
            verticalInput = Input.GetAxis("Vertical" + inputId);
            additionalVelocity = Vector2.Lerp(additionalVelocity, Vector2.zero, Time.deltaTime);
            
            animator.SetFloat("Horizontal", horizontalInput);
            animator.SetFloat("Vertical", verticalInput);
            animator.SetFloat("Speed",Mathf.Abs(verticalInput)+Mathf.Abs(horizontalInput));
            
            if (Input.GetKeyDown(interactionKey))
            {
                CheckIntersections();
            }
        }

        private void FixedUpdate()
        {
        
            var moveVector = new Vector2(horizontalInput, verticalInput);
            rb2D.MovePosition(rb2D.position + moveVector * speed + additionalVelocity);
        }

        private void DropItem()
        {
            var dropItem = item;
            item = null;

            dropItem.transform.SetParent(null);
            dropItem.ItemState = ItemState.OnGround;
            
            speed /= speedOnPickItemMultiplayer;
            
            DropItemEvent?.Invoke(dropItem);
        }

        private void DropItemInRocket(Rocket rocket)
        {
            rocket.PlaceItem(this, item);
            var boofer = item;
            item = null;
            
            speed /= speedOnPickItemMultiplayer;
            
            PlacedToRocketEvent.Invoke(boofer);
        }

        public bool CanPickUpConcreteItem(Item item)
        {
            return CanPickUpItem
                   && item != null
                   && item.ItemState == ItemState.OnGround;
        }

        private void PickUpItem(Item item)
        {
            this.item = item;
            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
            item.ItemState = ItemState.OnPlayer;
            intersectingObjects.Remove(item.GetComponent<Collider2D>());

            speed *= speedOnPickItemMultiplayer;
            
            PickUpItemEvent?.Invoke(item);
        }

        public void CheckIntersections()
        {
            if (fightTrigger.OtherEntered)
            {
                
                var otherPlayer = GameRoot.Instance.GetOtherPlayer(this);
                var directionVec = (otherPlayer.transform.position - transform.position).normalized;
                otherPlayer.additionalVelocity = directionVec * attackForce;
                
                return;
            }
            
            if (CanPickUpItem)
            {
                foreach (var other in intersectingObjects
                             .OrderBy(c => (transform.position - c.transform.position).magnitude))
                {
                    var pickUpItem = other.GetComponent<Item>();
                    if (pickUpItem != null && CanPickUpConcreteItem(pickUpItem))
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log($"Trigger Enter {other.name}");
            intersectingObjects.Add(other);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            //Debug.Log($"Trigger Exit {other.name}");
            intersectingObjects.Remove(other);
        }
    }
}