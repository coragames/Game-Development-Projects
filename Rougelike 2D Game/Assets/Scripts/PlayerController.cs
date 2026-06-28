using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private BoardManager _board;
    public Vector2Int _cellPosition;
    private bool _isGameOver;
    private Animator _animator;
    private bool _isMoving;
    private Vector3 _moveTarget;

    public float moveSpeed = 5.0f;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int newCellTarget = _cellPosition;
        bool hasMoved = false;

        if(Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y += 1;
            hasMoved = true;
        }
        else if(Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            newCellTarget.y -= 1;
            hasMoved = true;
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x += 1;
            hasMoved = true;
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            newCellTarget.x -= 1;
            hasMoved = true;
        }

        if(hasMoved)
        {
            //check if the new position is passable, then move there if it is.            
            BoardManager.CellData cellData = _board.GetCellData(newCellTarget);

            if(cellData != null && cellData.passable)
            {
                // Moves the player to the new cell on the board
                GameManager.instance._turnManager.Tick();
                if (cellData.containedFoodObject == null)
                {
                    MoveTo(newCellTarget, _isMoving);
                }

                else if (cellData.containedFoodObject.PlayerWantsToEnter())
                {
                    MoveTo(newCellTarget, _isMoving);
                
                    //Call PlayerEntered AFTER moving the player! Otherwise not in cell yet                       
                    cellData.containedFoodObject.PlayerEntered();
                }
            }
        }     

        if (_isGameOver)
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                GameManager.instance.StartNewGame();
            }
            
            return;
        }

        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _moveTarget, moveSpeed * Time.deltaTime);

            if (transform.position == _moveTarget)
            {
                _isMoving = false;
                _animator.SetBool("Moving", false);
                var cellData = _board.GetCellData(_cellPosition);

                if (cellData.containedFoodObject != null)
                {
                    cellData.containedFoodObject.PlayerEntered();
                }
            }

            return;
        }

        if(hasMoved)
        {
            //check if the new position is passable, then move there if it is.
            BoardManager.CellData cellData = _board.GetCellData(newCellTarget);

            if(cellData != null && cellData.passable)
            {
                GameManager.instance._turnManager.Tick();

                if (cellData.containedFoodObject == null)
                {
                    MoveTo(newCellTarget, _isMoving);
                }
                else if (cellData.containedFoodObject.PlayerWantsToEnter())
                {
                    MoveTo(newCellTarget, _isMoving);
                }
            }
        }
    }

    public void Spawn(BoardManager boardManager, Vector2Int cell)
    {
        _cellPosition = cell;
        _board = boardManager;        

        //let's move to the right position...
        transform.position = _board.CellToWorld(cell);
    }

    public void MoveTo(Vector2Int cell, bool immediate)
    {
        _cellPosition = cell;

        if (immediate)
        {
            _isMoving = false;
            transform.position = _board.CellToWorld(_cellPosition);
        }
        else
        {
            _isMoving = true;
            _moveTarget = _board.CellToWorld(_cellPosition);
        }
        
        _animator.SetBool("Moving", _isMoving);
    }

    // Checks if Game is over or not
    public void GameOver()
    {
        _isGameOver = true;
    }

    public void Init()
    {
        _isMoving = false;
        _isGameOver = false;
    }    

}
