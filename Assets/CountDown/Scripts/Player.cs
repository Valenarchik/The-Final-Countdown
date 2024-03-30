using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace CountDown
{
    public class Player: MonoBehaviour
    {
        [SerializeField] private string inputId;
        [SerializeField] private Animator animator;
        [SerializeField] private float speed = 20;
        [SerializeField] private KeyCode interactionKey;
        
        [SerializeField, ReadOnlyInspector] private int score;
        [SerializeField, ReadOnlyInspector] private Item item;
        
        private readonly List<Collider2D> intersectingObjects = new ();
        
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
        public IReadOnlyCollection<Collider2D> IntersectingObjects => intersectingObjects;
        
        private Rigidbody2D rb2D;
        
        private float horizontalInput;
        private float verticalInput;
        
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
            rb2D = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal" + inputId);
            verticalInput = Input.GetAxis("Vertical" + inputId);
            
            animator.SetFloat("Horizontal", horizontalInput);
            animator.SetFloat("Vertical", verticalInput);
            animator.SetFloat("Speed",Mathf.Abs(verticalInput+horizontalInput));

            var moveVector = new Vector2(horizontalInput, verticalInput);
            
            transform.Translate(moveVector * speed * Time.deltaTime);

            if (Input.GetKeyDown(interactionKey))
            {
                CheckIntersections();
            }
        }
        
        private void DropItem()
        {
            var dropItem = item;
            item = null;

            dropItem.transform.SetParent(null);
            dropItem.ItemState = ItemState.OnGround;
            DropItemEvent?.Invoke(dropItem);
        }

        private void DropItemInRocket(Rocket rocket)
        {
            rocket.PlaceItem(this, item);
            item = null;
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
            PickUpItemEvent?.Invoke(item);
        }

        public void CheckIntersections()
        {
            //Debug.Log("CheckIntersections");
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