using UnityEngine;
using TMPro;

public class ColorCycle : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public float speed = 1.0f;

    void Update()
    {
        float r = Mathf.Sin(Time.time * speed) * 0.5f + 0.5f;
        float g = Mathf.Sin(Time.time * speed + 2f) * 0.5f + 0.5f;
        float b = Mathf.Sin(Time.time * speed + 4f) * 0.5f + 0.5f;

        titleText.color = new Color(r, g, b);
    }
}
