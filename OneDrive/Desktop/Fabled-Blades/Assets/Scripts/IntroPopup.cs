using UnityEngine;

public class IntroPopup : MonoBehaviour
{
    public GameObject introPanel;

    void Start()
    {
        introPanel.SetActive(true);
        Time.timeScale = 0f; // Pause game while reading
    }

    public void CloseIntro()
    {
        introPanel.SetActive(false);
        Time.timeScale = 1f; // Resume game
    }
}
