using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public int gridSize = 5;
    public float spacing = 1.1f;

    void Start()
    {
        float offset = (gridSize - 1) * spacing / 2f;
        Tile[,] tiles = new Tile[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 pos = new Vector3(x * spacing - offset, y * spacing - offset, 0);
                GameObject tileGO = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
                Tile tile = tileGO.GetComponent<Tile>();
                tile.gridPosition = new Vector2Int(x, y);
                tiles[x, y] = tile;
            }
        }

        LoadLevelLayout(tiles);
    }

    void LoadLevelLayout(Tile[,] tiles)
    {
        int lvl = LevelManager.currentLevel;

        switch (lvl)
        {
            case 1:
                // Blue
                tiles[0, 4].SetDot(Color.blue, true);
                tiles[4, 4].SetDot(Color.blue, false);

                // Red
                tiles[0, 0].SetDot(Color.red, true);
                tiles[4, 0].SetDot(Color.red, false);

                // Green
                tiles[2, 1].SetDot(Color.green, true);
                tiles[2, 3].SetDot(Color.green, false);
                break;

            case 2:
                // Red
                tiles[1, 3].SetDot(Color.red, true);
                tiles[3, 3].SetDot(Color.red, false);

                // Blue
                tiles[0, 1].SetDot(Color.blue, true);
                tiles[4, 2].SetDot(Color.blue, false);

                // Yellow
                tiles[0, 0].SetDot(Color.yellow, true);
                tiles[4, 1].SetDot(Color.yellow, false);

                // Green
                tiles[2, 1].SetDot(Color.green, true);
                tiles[4, 0].SetDot(Color.green, false);
                break;

            case 3:
            // Green
            tiles[0, 4].SetDot(Color.green, true);
            tiles[3, 1].SetDot(Color.green, false);

            // Yellow
            tiles[1, 4].SetDot(Color.yellow, true);
            tiles[3, 4].SetDot(Color.yellow, false);

            // Orange
            tiles[4, 4].SetDot(new Color(1.0f, 0.55f, 0.0f), true);  // Orange
            tiles[2, 2].SetDot(new Color(1.0f, 0.55f, 0.0f), false);

            // Red
            tiles[0, 2].SetDot(Color.red, true);
            tiles[3, 0].SetDot(Color.red, false);

            // Blue
            tiles[3, 2].SetDot(Color.blue, true);
            tiles[4, 0].SetDot(Color.blue, false);
            break;

            default:
                Debug.LogWarning("No layout defined for level: " + lvl);
                break;
        }
    }
}
