using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.3f;

    private Vector3 originalPos;
    private float currentDuration = 0f;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (currentDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeMagnitude;
            currentDuration -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = originalPos;
        }
    }

    public void Shake()
    {
        currentDuration = shakeDuration;
    }
}
