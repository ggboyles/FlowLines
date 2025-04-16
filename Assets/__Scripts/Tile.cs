using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject dotOverlay;
    public Vector2Int gridPosition;
    public SpriteRenderer sr;

    public bool isStart = false;
    public bool isEnd = false;
    public Color pathColor;
    public bool IsFilled => isStart || isEnd || sr.color != Color.white;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.black; // <-----

        if (dotOverlay != null)
        {
            dotOverlay.SetActive(false);
        }
    }

    public void SetDot(Color color, bool start)
    {
        Debug.Log("Dot activated on tile: " + gridPosition);

        pathColor = color;
        isStart = start;
        isEnd = !start;
        sr.color = Color.black; // <-----

        if (dotOverlay != null)
        {
            dotOverlay.SetActive(true);
            SpriteRenderer dotSR = dotOverlay.GetComponent<SpriteRenderer>();
            dotSR.color = color;
        }
    }

    public void SetPath(Color color)
    {
        if (!isStart && !isEnd)
            sr.color = color;
    }

    public void Clear()
    {
        if (!isStart && !isEnd)
            sr.color = Color.black; // <----
    }
}
