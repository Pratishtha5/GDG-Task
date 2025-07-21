using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Mountain"); // replace with your actual first scene
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!"); // Only works in build, not in Editor
    }

    public void ShowCredits()
    {
        Debug.Log("Show credits panel or load credits scene");
    }
}
