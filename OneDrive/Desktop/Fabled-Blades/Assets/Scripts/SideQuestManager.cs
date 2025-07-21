using UnityEngine;

public class SideQuestManager : MonoBehaviour
{
    public static SideQuestManager Instance;

    public bool questAccepted = false;
    public bool crystalFound = false;
    public bool questCompleted = false;
    public QuestUIManager questUI;

    public void ShowQuest()
    {
        questUI.ShowQuest(" Side Quest: Find the Magic Crystal");
    }

    public void HideQuest()
    {
        questUI.HideQuest();
    }


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CollectCrystal()
    {
        crystalFound = true;
    }

    public void CompleteQuest()
    {
        questCompleted = true;
    }
}
