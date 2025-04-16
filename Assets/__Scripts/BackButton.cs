using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void OnBackButtonPressed()
    {
        LevelManager.RestartGame();
    }
}
