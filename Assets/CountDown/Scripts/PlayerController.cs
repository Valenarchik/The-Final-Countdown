using System;
using UnityEngine;

namespace CountDown
{
    [RequireComponent(typeof(Player))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private string inputId;
        private Player player;
        
        public float speed = 20;
        public float rotateSpeed = 45;
        
        private float horizontalInput;
        private float forewordInput;
        
        private void Awake()
        {
            player = GetComponent<Player>();
        }
        void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal" + inputId);
            forewordInput = Input.GetAxis("Vertical" + inputId);
            
            transform.Translate(Vector2.up * speed * forewordInput * Time.deltaTime);
            transform.Rotate(Vector2.right * rotateSpeed * horizontalInput * Time.deltaTime);
        }
    }

}
