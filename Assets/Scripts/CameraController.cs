using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset = new(0, 0, -10);
    [SerializeField] private Vector2 bounds;

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        
        if (bounds != Vector2.zero)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, -bounds.x, bounds.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, -bounds.y, bounds.y);
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}