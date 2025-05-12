using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
