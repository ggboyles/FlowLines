using UnityEngine;
using UnityEngine.UI;

public class VictoryHandler : MonoBehaviour
{
    public Button nextLevelButton;

    void Start()
    {
        nextLevelButton.onClick.AddListener(LevelManager.LoadNextLevel);
    }

    public void ReturnToStart()
    {
        LevelManager.RestartGame(); 
    }
}
