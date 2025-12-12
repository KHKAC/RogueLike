using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    public class CellData
    {
        public bool passable;
        public CellObject containedObject;
    }

    public int width;
    public int height;
    public Tile[] groundTiles;
    public Tile[] wallTiles;
    public PlayerController player;
    public FoodObject foodPrefab;
    // public GameObject[] foodPrefabs;
    public Sprite[] foodSprites;
    [Tooltip("최대 음식 수 포함")]
    public int foodMaxCnt = 7;
    public int foodMinCnt = 4;

    Tilemap tilemap;
    CellData[,] boardData;
    Grid grid;
    List<Vector2Int> emptyCellList;
    int[] foodGranted = new int[]{ 5, 6, 7, 10, 11, 12};
    
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
        // int foodCount = 5;
        int foodCount = Random.Range(foodMinCnt, foodMaxCnt + 1);
        Debug.Log($"Food : {foodCount}");
        for(int i = 0; i < foodCount; i++)
        {
            int randomIndex = Random.Range(0, emptyCellList.Count);
            Vector2Int coord = emptyCellList[randomIndex];

            emptyCellList.RemoveAt(randomIndex);
            CellData data = boardData[coord.x, coord.y];

            int foodType = Random.Range(0, foodSprites.Length);
            foodPrefab.GetComponent<SpriteRenderer>().sprite = foodSprites[foodType];
            foodPrefab.SetGrantedValue(foodGranted[foodType]);
            FoodObject newFood = Instantiate(foodPrefab);
            
            newFood.transform.position = CellToWorld(coord);
            data.containedObject = newFood;
        }
    }
}
