using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIDocument UIDoc;
    private Label _foodLabel;
    private VisualElement _gameOverPanel;
    private Label _gameOverMessage;
    private int _currentLevel = 1;
    public BoardManager boardManager;
    public PlayerController playerController;
    public TurnManager _turnManager {get; private set;}
    public static GameManager instance {get; private set;}
        
    private int _foodAmount = 100;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        NewLevel();

        // For displaying food amount left on UIDoc
        _foodLabel = UIDoc.rootVisualElement.Q<Label>("FoodLabel");
        _foodLabel.text = "Food : " + _foodAmount;

        // To Invoke callback system
        _turnManager = new TurnManager();
        _turnManager.OnTick += OnTurnHappen;

        // UI for displaying Game Over message
        _gameOverPanel = UIDoc.rootVisualElement.Q<VisualElement>("GameOverPanel");
        _gameOverMessage = _gameOverPanel.Q<Label>("GameOverMessage");

        _gameOverPanel.style.visibility = Visibility.Hidden;

        StartNewGame();
    }

    // Starts a new game once Game is over
    public void StartNewGame()
    {
        _gameOverPanel.style.visibility = Visibility.Hidden;
  
        _currentLevel = 1;
        _foodAmount = 20;
        _foodLabel.text = "Food : " + _foodAmount;
        
        boardManager.Clean();
        boardManager.Init();
        
        playerController.Init();
        playerController.Spawn(boardManager, new Vector2Int(1,1));
    }

    void OnTurnHappen()
    {
        {
            ChangeFood(-1);
        }     
    }

    public void ChangeFood(int amount)
    {
        _foodAmount += amount;
        //Debug.Log("Current amount of food : " + _foodAmount);
        _foodLabel.text = "Food : " + _foodAmount;

        // To check if food count is zero or not
        if (_foodAmount <= 0)
        {
            playerController.GameOver();
            _gameOverPanel.style.visibility = Visibility.Visible;
            _gameOverMessage.text = "Game Over!!!\n\nSurvived " + _currentLevel + " days";
        }
    }  

    // Creates a Next Level
    public void NewLevel()
    {
        boardManager.Clean();
        boardManager.Init();
        playerController.Spawn(boardManager, new Vector2Int(1,1));

        _currentLevel++;
    } 

}
