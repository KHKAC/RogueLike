using UnityEngine;
using UnityEngine.Tilemaps;

public class WallObject : CellObject
{
    public Tile obstacleTile;
    public int maxHealth = 3;

    int healthPoint;
    Tile originalTile;
    public override void Init(Vector2Int iCell)
    {
        base.Init(iCell);
        healthPoint = maxHealth;
        originalTile = GameManager.Instance.boardManager.GetCellTile(cell);
        GameManager.Instance.boardManager.SetCellTile(cell, obstacleTile);
    }

    public override bool PlayerWantsToEnter()
    {
        healthPoint -= 1;
        if(healthPoint > 0) return false;
        GameManager.Instance.boardManager.SetCellTile(cell, originalTile);
        Destroy(gameObject);
        return false;
    }
}
