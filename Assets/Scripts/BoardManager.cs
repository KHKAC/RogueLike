using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    public class CellData
    {
        public bool passable;
        public GameObject containedObject;
    }

    public int width;
    public int height;
    public Tile[] groundTiles;
    public Tile[] wallTiles;
    public PlayerController player;

    Tilemap tilemap;
    CellData[,] boardData;
    Grid grid;
    
    public void Init()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        grid = GetComponentInChildren<Grid>();
        boardData = new CellData[width, height];
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                Tile tile;
                boardData[x, y] = new CellData();
                if(x == 0 || y == 0 || x == width -1 || y == height -1)
                {
                    // tile = wallTiles[Random.Range(0, wallTiles.Length)];
                    tile = GetRandomTile(wallTiles);
                    boardData[x,y].passable = false;
                }
                else
                {
                    // tile = groundTiles[Random.Range(0, groundTiles.Length)];
                    tile = GetRandomTile(groundTiles);
                    boardData[x,y].passable = true;
                }
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
        // player.Spawn(this, new Vector2Int(1, 1));
    }

    Tile GetRandomTile(Tile[] tiles)
    {
        return tiles[Random.Range(0, tiles.Length)];
    }

    
    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    public CellData GetCellData(Vector2Int cellIndex)
    {
        if(cellIndex.x < 0 || cellIndex.x > width || cellIndex.y < 0 || cellIndex.y > height)
        {
            return null;
        }
        
        return boardData[cellIndex.x, cellIndex.y];
    }
}
