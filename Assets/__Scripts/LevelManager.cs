using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int currentLevel = 1;
    public static int maxLevel = 3;

    public static void LoadNextLevel()
    {
        currentLevel++;

        if (currentLevel <= maxLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            RestartGame();
        }
    }

    public static void RestartGame()
    {
        currentLevel = 1;
        SceneManager.LoadScene("StartScreen");
    }
}
