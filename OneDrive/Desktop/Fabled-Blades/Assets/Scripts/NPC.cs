using UnityEngine;

public class NPC : MonoBehaviour
{
    public string message = "Hello, hero! Can you help me?";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.ShowMessage(message);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.HideMessage();
        }
    }
}
