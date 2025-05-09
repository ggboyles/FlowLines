// displays current level number in text

using UnityEngine;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    void Start()
    {
        levelText.text = "Level " + LevelManager.currentLevel;
    }
}
