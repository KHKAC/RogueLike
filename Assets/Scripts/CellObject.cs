using UnityEngine;

public class CellObject : MonoBehaviour
{
    protected Vector2Int cell;

    public virtual void Init(Vector2Int iCell)
    {
        cell = iCell;
    }

    public virtual void PlayerEntered()
    {
        
    }

    public virtual bool PlayerWantsToEnter()
    {
        return true;
    }
}
