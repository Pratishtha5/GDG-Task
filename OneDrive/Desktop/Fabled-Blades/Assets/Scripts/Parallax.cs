using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxFactor;
    private Transform cam;
    private Vector3 previousCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    void Update()
    {
        Vector3 delta = cam.position - previousCamPos;
        transform.position += delta * parallaxFactor;
        previousCamPos = cam.position;
    }
}
