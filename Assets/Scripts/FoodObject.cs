using UnityEngine;

public class FoodObject : CellObject
{
    public int amountGranted = 10;
    public override void PlayerEntered()
    {
        Destroy(gameObject);
        Debug.Log("Food increased");
        GameManager.Instance.ChangeFood(amountGranted);
    }

    public void SetGrantedValue(int value)
    {
        amountGranted = value;
    }
}
