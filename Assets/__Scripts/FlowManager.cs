using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour
{
    public Camera mainCam;
    public GameObject victoryPopup;
    public AudioClip connectSound;
    public AudioClip clearSound;

    private AudioSource audioSource;
    private Tile currentStart = null;
    private Color currentColor;
    private List<Tile> currentPath = new();
    private Color? overwrittenColor = null;
    private Dictionary<Color, bool> connectedPaths = new();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Tile tile = GetTileUnderMouse();
            if (tile != null && (tile.isStart || tile.isEnd))
            {
                foreach (Tile t in FindObjectsByType<Tile>(FindObjectsSortMode.None))
                {
                    if (!t.isStart && !t.isEnd && t.sr.color == tile.pathColor)
                        t.Clear();
                }

                currentStart = tile;
                currentColor = tile.pathColor;

                tile.SetPath(currentColor);

                currentPath.Clear();
                currentPath.Add(tile);
                overwrittenColor = null;
            }
        }
        else if (Input.GetMouseButton(0) && currentStart != null)
        {
            Tile tile = GetTileUnderMouse();
            if (tile != null && IsAdjacent(tile, currentPath[^1]))
            {
                if (!currentPath.Contains(tile))
                {
                    if (!tile.isStart && !tile.isEnd)
                    {
                        if (tile.sr.color != Color.black && tile.sr.color != currentColor)
                        {
                            if (overwrittenColor == null)
                                overwrittenColor = tile.sr.color;
                        }

                        tile.SetPath(currentColor);
                        currentPath.Add(tile);
                    }
                    else if ((tile.isStart || tile.isEnd) && tile.pathColor == currentColor)
                    {
                        tile.SetPath(currentColor);
                        currentPath.Add(tile);
                    }
                }
                else if (currentPath.Count >= 2 && tile == currentPath[^2])
                {
                    Tile last = currentPath[^1];
                    last.Clear();
                    currentPath.RemoveAt(currentPath.Count - 1);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && currentPath.Count > 0)
        {
            if (overwrittenColor.HasValue)
            {
                foreach (Tile t in FindObjectsByType<Tile>(FindObjectsSortMode.None))
                {
                    if (!t.isStart && !t.isEnd && t.sr.color == overwrittenColor.Value)
                        t.Clear();
                }
            }

            if (currentPath.Count > 1)
            {
                Tile last = currentPath[^1];
                if ((last.isStart || last.isEnd) && last.pathColor == currentColor)
                {
                    connectedPaths[currentColor] = true;
                    Debug.Log("Connected path color: " + currentColor);

                    if (connectSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(connectSound);
                    }
                }
            }

            CheckVictory();
            currentStart = null;
            currentPath.Clear();
            overwrittenColor = null;
        }
    }

    void CheckVictory()
    {
        Tile[] allTiles = FindObjectsByType<Tile>(FindObjectsSortMode.None);
        foreach (Tile tile in allTiles)
        {
            if (!tile.isStart && !tile.isEnd && tile.sr.color == Color.black)
                return;
        }

        foreach (Tile tile in allTiles)
        {
            if ((tile.isStart || tile.isEnd) && !connectedPaths.ContainsKey(tile.pathColor))
                return;
        }

        Debug.Log("Puzzle Solved!");
        if (victoryPopup != null)
        {
            victoryPopup.SetActive(true);
        }
    }

    Tile GetTileUnderMouse()
    {
        Vector3 worldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        return hit.collider ? hit.collider.GetComponent<Tile>() : null;
    }

    bool IsAdjacent(Tile a, Tile b)
    {
        Vector2Int diff = a.gridPosition - b.gridPosition;
        return (Mathf.Abs(diff.x) == 1 && diff.y == 0) || (Mathf.Abs(diff.y) == 1 && diff.x == 0);
    }

    // used for backButton
    public void ClearPaths()
{
    Tile[] allTiles = FindObjectsByType<Tile>(FindObjectsSortMode.None);

    foreach (Tile tile in allTiles)
    {
        if (!tile.isStart && !tile.isEnd && tile.sr.color != Color.black)
        {
            tile.Clear();
        }

        // plays clear sound
        if (clearSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clearSound, 0.05f); // had to turn volume down

        }
    }

    // Also reset path tracking
    connectedPaths.Clear();
}

}
