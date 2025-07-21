using UnityEngine;

public class AreaTransition : MonoBehaviour
{
    public LoadingManager loadingManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            loadingManager.TriggerLoading();
            // Optionally: teleport player after load
            // other.transform.position = new Vector3(x, y, z);
        }
    }
}
