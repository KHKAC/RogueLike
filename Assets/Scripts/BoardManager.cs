using System.Collections.Generic;
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
    public GameObject foodPrefab;

    Tilemap tilemap;
    CellData[,] boardData;
    Grid grid;
    List<Vector2Int> emptyCellList;
    
    public void Init()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        grid = GetComponentInChildren<Grid>();
        emptyCellList = new List<Vector2Int>();
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
                    emptyCellList.Add(new Vector2Int(x, y));
                }
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
        // player.Spawn(this, new Vector2Int(1, 1));
        emptyCellList.Remove(new Vector2Int(1, 1));
        GenerateFood();
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

    void GenerateFood()
    {
        int foodCount = 5;
        for(int i = 0; i < foodCount; i++)
        {
            int randomIndex = Random.Range(0, emptyCellList.Count);
            Vector2Int coord = emptyCellList[randomIndex];

            emptyCellList.RemoveAt(randomIndex);
            CellData data = boardData[coord.x, coord.y];
            GameObject newFood = Instantiate(foodPrefab);
            newFood.transform.position = CellToWorld(coord);
            data.containedObject = newFood;
        }
    }
}
