using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject destinationObject;
    [SerializeField] private GameObject circleCenter;
    [SerializeField] private float minDistance;

    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private float radius;
    void Start()
    {
        radius = Vector2.Distance(circleCenter.transform.position, transform.position);
    }
    
    void LateUpdate()
    {
        var distanceVector = destinationObject.transform.position - circleCenter.transform.position;
        var direction = (distanceVector).normalized;

        spriteRenderer.enabled = !(distanceVector.magnitude < minDistance);
        
        transform.position = circleCenter.transform.position + direction * radius;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }
}
