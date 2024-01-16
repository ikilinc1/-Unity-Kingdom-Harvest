using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 2.0f;
    public float minZoom = 2.0f;
    public float maxZoom = 10.0f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Adjust the orthographic size based on the scroll input
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom);
    }
}