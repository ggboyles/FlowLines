using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        LevelManager.currentLevel = level;
        SceneManager.LoadScene("SampleScene");
    }
}