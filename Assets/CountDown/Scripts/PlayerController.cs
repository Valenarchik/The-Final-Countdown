using System;
using UnityEngine;

namespace CountDown
{
    [RequireComponent(typeof(Player))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private string inputId;
        [SerializeField] private Animator animator;
        private Player player;
        
        public float speed = 20;
        
        private float horizontalInput;
        private float verticalInput;
        
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
            animator.SetFloat("Speed",Mathf.Abs(verticalInput+horizontalInput));
            
            
            transform.Translate(Vector2.up * speed * verticalInput * Time.deltaTime);
            transform.Translate(Vector2.right * speed * horizontalInput * Time.deltaTime);
        }
    }

}
