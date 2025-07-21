using UnityEngine;
using System.Collections;

public class LevelUpUI : MonoBehaviour
{
    public GameObject levelUpPanel;
    public float displayTime = 2f;

    void Start()
    {
        levelUpPanel.SetActive(false);
    }

    public void ShowLevelUpScreen()
    {
        StartCoroutine(ShowTemporarily());
    }

    IEnumerator ShowTemporarily()
    {
        levelUpPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(displayTime);
        levelUpPanel.SetActive(false);
    }
}
