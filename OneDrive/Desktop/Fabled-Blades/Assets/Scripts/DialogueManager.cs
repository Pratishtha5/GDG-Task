using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    void Awake()
    {
        instance = this;
        dialogueBox.SetActive(false);
    }

    public static void ShowMessage(string message)
    {
        instance.dialogueText.text = message;
        instance.dialogueBox.SetActive(true);
    }

    public static void HideMessage()
    {
        instance.dialogueBox.SetActive(false);
    }
}
