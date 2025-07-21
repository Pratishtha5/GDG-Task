using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public Text dialogueText;
    public string fullText;
    public float typingSpeed = 0.05f;

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        dialogueText.text = "";
        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
