using UnityEngine;

public class CrystalPickup : MonoBehaviour
{
    public AudioClip collect;
    private PlayerAudio playerAudio;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && SideQuestManager.Instance.questAccepted && !SideQuestManager.Instance.crystalFound)
        {
            playerAudio = other.GetComponent<PlayerAudio>();

            if (playerAudio != null)
            {
                playerAudio.PlayCollectSound();
            }

            SideQuestManager.Instance.CollectCrystal();
            Destroy(gameObject); // Remove the crystal from the scene
        }
    }
}
