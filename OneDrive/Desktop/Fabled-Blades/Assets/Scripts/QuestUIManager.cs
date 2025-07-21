using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    public GameObject questPanel;
    public Text questText;

    void Start()
    {
        questPanel.SetActive(false);
    }

    public void ShowQuest(string text)
    {
        questText.text = text;
        questPanel.SetActive(true);
    }

    public void HideQuest()
    {
        questPanel.SetActive(false);
    }
}
