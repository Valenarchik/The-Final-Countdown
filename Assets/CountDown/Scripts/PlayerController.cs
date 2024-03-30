using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CountDown
{
    [RequireComponent(typeof(Player))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private string inputId;
        [SerializeField] private Animator animator;
        [SerializeField] private float speed = 20;
        [SerializeField] private KeyCode interactionKey;
        
        private Player player;
        
        private float horizontalInput;
        private float verticalInput;

        public KeyCode InteractionKey => interactionKey;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            player = GetComponent<Player>();
        }
        void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal" + inputId);
            verticalInput = Input.GetAxis("Vertical" + inputId);

            animator.SetFloat("Horizontal", horizontalInput);
            animator.SetFloat("Vertical", verticalInput);
            animator.SetFloat("Speed",Mathf.Abs(new Vector2(verticalInput, horizontalInput).sqrMagnitude));
            
            
            transform.Translate(Vector2.up * speed * verticalInput * Time.deltaTime);
            transform.Translate(Vector2.right * speed * horizontalInput * Time.deltaTime);

            if (Input.GetKeyDown(interactionKey))
            {
               player.CheckIntersections();
            }
        }
    }

}
