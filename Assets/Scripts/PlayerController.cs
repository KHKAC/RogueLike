using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    BoardManager board;
    Vector2Int cellPosition;
    
    public void Spawn(BoardManager boardManager, Vector2Int cell)
    {
        board = boardManager;
        MoveTo(cell);
    }

    public void MoveTo(Vector2Int cell)
    {
        cellPosition = cell;
        transform.position = board.CellToWorld(cell);
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector2Int newCellTarget = cellPosition;
        bool hasMoved = false;
        if(Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y++;
            hasMoved = true;
        }
        else if(Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y--;
            hasMoved = true;
        }
        else if(Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x++;
            hasMoved = true;
        }
        else if(Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x--;
            hasMoved = true;
        }

        if(hasMoved)
        {
            // 새 포지션이 passable일 때, 움직이도록
            BoardManager.CellData cellData = board.GetCellData(newCellTarget);
            if(cellData != null && cellData.passable)
            {
                GameManager.Instance.TurnManager.Tick();
                MoveTo(newCellTarget);
                if(cellData.containedObject != null)
                {
                    cellData.containedObject.PlayerEntered();
                }
            }
        }
    }
}
