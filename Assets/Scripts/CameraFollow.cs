using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;  // Player transform to follow
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [Header("Camera Bounds")]
    public Vector2 minPosition;  // bottom-left limit
    public Vector2 maxPosition;  // top-right limit

    void LateUpdate()
    {
        if (target == null)
            return;

        // Desired position
        Vector3 desiredPosition = target.position + offset;

        // Clamp camera inside bounds
        float clampedX = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y);

        Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);

        // Smooth camera movement
        transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
    }
}
