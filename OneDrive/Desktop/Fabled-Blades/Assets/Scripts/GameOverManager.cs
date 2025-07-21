using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;
    public GameObject gameOverCanvas;

    void Awake()
    {
        instance = this;
        gameOverCanvas.SetActive(false);
    }

    public static void TriggerGameOver()
    {
        instance.gameOverCanvas.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload scene
    }
}
