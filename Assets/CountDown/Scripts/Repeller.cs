using CountDown;
using UnityEngine;

public class Repeller : MonoBehaviour
{
    [SerializeField] private float power;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Player>(out var player))
        {
            Vector2 distanceVector =(other.transform.position - transform.position);
            var direction = distanceVector.normalized;
            var distance = distanceVector.magnitude;

            player.AdditionalVelocity += power * direction * distance * distance;
        }
    }

    public void DestroyGM()
    {
        Destroy(gameObject);
    }
}
