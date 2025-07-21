using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    public float zoomSpeed = 2f;
    public float zoomedSize = 3f;       // Zoom in size
    public float defaultSize = 5f;      // Normal size

    void Awake()
    {
        cam = Camera.main;
    }

    public void ZoomIn()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomTo(zoomedSize));
    }

    public void ZoomOut()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomTo(defaultSize));
    }

    IEnumerator ZoomTo(float targetSize)
    {
        while (Mathf.Abs(cam.orthographicSize - targetSize) > 0.05f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
            yield return null;
        }
        cam.orthographicSize = targetSize;
    }
}
