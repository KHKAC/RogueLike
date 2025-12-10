using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public BoardManager boardManager;
    public PlayerController playerController;
    
    public TurnManager TurnManager { get; private set; }
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        TurnManager = new TurnManager();
        boardManager.Init();
        playerController.Spawn(boardManager, new Vector2Int(1, 1));
    }

    void Update()
    {
        
    }
}
