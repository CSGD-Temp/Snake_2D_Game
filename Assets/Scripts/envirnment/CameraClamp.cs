using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    public Transform target;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private void LateUpdate()
    {
        if (target != null)
        {
            // Get the camera's position
            Vector3 newPosition = transform.position;

            // Clamp the camera's position within the defined bounds
            newPosition.x = Mathf.Clamp(target.position.x, minBounds.x, maxBounds.x);
            newPosition.y = Mathf.Clamp(target.position.y, minBounds.y, maxBounds.y);

            // Update the camera's position
            transform.position = newPosition;
        }
    }
}
