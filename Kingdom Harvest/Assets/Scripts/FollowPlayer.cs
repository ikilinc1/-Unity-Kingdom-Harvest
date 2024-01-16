using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; // Reference to the player's Transform

    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset to adjust the camera position

    void LateUpdate ()
    {
        if (target != null)
        {
            // Update the camera position to follow the player
            transform.position = target.position + offset;
        }
    }
}