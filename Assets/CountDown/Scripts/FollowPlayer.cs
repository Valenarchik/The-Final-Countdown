using UnityEngine;

namespace CountDown
{
    public class FollowPlayer: MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float moveSmooth;
        [SerializeField] private float speedMultiplierValue;
        [SerializeField] private float horizontalBorder;
        [SerializeField] private float verticalBorder;
        
        private Vector3 zOffset;

        //private Vector2? targetPos;
        void Start()
        {
            zOffset = Vector3.forward * (transform.position - player.position).z;
            transform.position = player.position + zOffset;
        }

        public void SetTarget(Transform newTarget)
        {
            player = newTarget;
            zOffset = Vector3.forward * (transform.position - player.position).z;
            transform.position = player.position + zOffset;
        }
        
        void LateUpdate()
        {
            if (player != null)
            {
                var diff = transform.position - player.position;
                var speedMultiplier = 1f;

                if (Mathf.Abs(diff.x) > horizontalBorder || Mathf.Abs(diff.y) > verticalBorder)
                {
                    //Debug.Log(diff);
                    speedMultiplier = speedMultiplierValue;
                }


                transform.position = Vector3.Lerp(transform.position, player.position + zOffset,
                        Time.deltaTime * moveSmooth * speedMultiplier);
            }
        }
    }
}