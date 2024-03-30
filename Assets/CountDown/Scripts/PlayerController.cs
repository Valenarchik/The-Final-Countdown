using System;
using UnityEngine;

namespace CountDown
{
    [RequireComponent(typeof(Player))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private string inputId;
        [SerializeField] private float speed = 20;
        [SerializeField] private KeyCode interactionKey;
        
        private Player player;
        
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
            transform.Translate(Vector2.right * speed * horizontalInput * Time.deltaTime);
        }
    }

}
