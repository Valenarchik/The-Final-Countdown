using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

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
        [SerializeField] private float inputLockInSeconds = 1;
        [SerializeField] private float attackCooldownInSeconds = 5;
        [SerializeField] private float dropForce = 20;
        [SerializeField] private SpriteRenderer ifCantFightImage;
        public bool CanAttack => !attackCooldown && fightTrigger.OtherEntered && Item == null;
        
        [Header("References")]
        [SerializeField] private PlayerFightTrigger fightTrigger;
        [SerializeField] private ItemAnimator itemAnimator;
        
        [Header("Debug")]
        [SerializeField, ReadOnlyInspector] private int score;
        [SerializeField, ReadOnlyInspector] private Item item;
        
        private readonly List<Collider2D> intersectingObjects = new ();
        
        private bool inputLocked;
        private Coroutine inputLockCoroutine;

        private bool attackCooldown;
        private Coroutine attackCooldownCoroutine;
        
        [Header("Events")]
        public UnityEvent<Item> PickUpItemEvent;
        public UnityEvent<Item> DropItemEvent;
        public UnityEvent<Item> PlacedToRocketEvent;
        
        public bool CanDropItem => item != null;
        public bool CanPickUpItem => item == null;

        public bool IsMoving => horizontalInput > 0.05f || verticalInput > 0.05f;
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

        public Rigidbody2D Rb2D => rb2D;

        public Vector2 AdditionalVelocity
        {
            get => additionalVelocity;
            set => additionalVelocity = value;
        }

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
            horizontalInput = Input.GetAxis("Horizontal" + inputId) * (inputLocked ? 0 : 1);
            verticalInput = Input.GetAxis("Vertical" + inputId) * (inputLocked ? 0 : 1);
            
            additionalVelocity = Vector2.Lerp(additionalVelocity, Vector2.zero, Time.deltaTime);
            
            animator.SetFloat("Horizontal", horizontalInput);
            animator.SetFloat("Vertical", verticalInput);
            animator.SetFloat("Speed",Mathf.Abs(verticalInput)+Mathf.Abs(horizontalInput));
            
            if (inputLocked) return;
            if (Input.GetKeyDown(interactionKey))
                CheckIntersections();
        }

        public void Fall()
        {
            animator.SetTrigger("Fall");
        }
        
        private void FixedUpdate()
        {
            var moveVector = new Vector2(horizontalInput, verticalInput);
            rb2D.MovePosition(rb2D.position + moveVector * speed + additionalVelocity);
            ifCantFightImage.enabled = attackCooldown;
        }

        private void DropItem()
        {
            item.gameObject.SetActive(true);
            var dropItem = item;
            item = null;

            dropItem.transform.SetParent(null);
            dropItem.ItemState = ItemState.OnGround;
            
            speed /= speedOnPickItemMultiplayer;
            
            intersectingObjects.Add(dropItem.GetComponent<Collider2D>());
            DropItemEvent?.Invoke(dropItem);
            itemAnimator.AnimatePlaceItem(dropItem.transform.position);
            animator.SetBool("HasBox",false);
            
        }

        private void DropItemInRocket(Rocket rocket)
        {
            rocket.PlaceItem(this, item);
            var boofer = item;
            Score += item.Cost;
            item = null;
            
            speed /= speedOnPickItemMultiplayer;
            
            PlacedToRocketEvent.Invoke(boofer);
            animator.SetBool("HasBox",false);
        }

        public bool CanPickUpConcreteItem(Item item)
        {
            return CanPickUpItem
                   && item != null
                   && item.ItemState == ItemState.OnGround;
        }

        private void PickUpItem(Item item)
        {
            var position = item.transform.position;
            this.item = item;
            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
            item.ItemState = ItemState.OnPlayer;
            intersectingObjects.Remove(item.GetComponent<Collider2D>());
            speed *= speedOnPickItemMultiplayer;
            PickUpItemEvent?.Invoke(item);
            itemAnimator.AnimatePickItemUp(position);
            animator.SetBool("HasBox", true);
        }
        

        public void CheckIntersections()
        {
            if (CheckAttack()) return;
            if (CheckPickUp()) return;
            CheckDrop();
        }

        private void CheckDrop()
        {
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

        private bool CheckPickUp()
        {
            if (CanPickUpItem)
            {
                foreach (var other in intersectingObjects
                             .OrderBy(c => (transform.position - c.transform.position).magnitude))
                {
                    var pickUpItem = other.GetComponent<Item>();
                    if (pickUpItem != null && CanPickUpConcreteItem(pickUpItem))
                    {
                        PickUpItem(pickUpItem);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckAttack()
        {
            if (CanAttack)
            {
                var otherPlayer = GameRoot.Instance.GetOtherPlayer(this);
                var directionVec = (otherPlayer.transform.position - transform.position).normalized;
                otherPlayer.additionalVelocity = directionVec * attackForce;
                otherPlayer.LockInput(inputLockInSeconds);
                if (otherPlayer.Item != null)
                {
                    var itemDropped = otherPlayer.Item;
                    otherPlayer.DropItem();
                    var randomVector = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized;
                    itemDropped.Rb2D.MovePosition(randomVector * dropForce);
                }
                CooldownAttack(attackCooldownInSeconds);
                return true;
            }

            return false;
        }

        // дублирование кода?
        private void CooldownAttack(float timeInSeconds)
        {
            if (attackCooldownCoroutine != null)
                StopCoroutine(attackCooldownCoroutine);
            
            attackCooldownCoroutine = StartCoroutine(CooldownACoroutine());
            IEnumerator CooldownACoroutine()
            {
                attackCooldown = true;
                yield return new WaitForSeconds(timeInSeconds);
                attackCooldown = false;
                attackCooldownCoroutine = null;
            }
        }

        private void LockInput(float timeInSeconds)
        {
            if (inputLockCoroutine != null)
                StopCoroutine(inputLockCoroutine);
            
            inputLockCoroutine = StartCoroutine(LockCoroutine());
            IEnumerator LockCoroutine()
            {
                inputLocked = true;
                yield return new WaitForSeconds(timeInSeconds);
                inputLocked = false;
                inputLockCoroutine = null;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!intersectingObjects.Contains(other))
                intersectingObjects.Add(other);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (intersectingObjects.Contains(other))
                intersectingObjects.Remove(other);
        }
    }
}