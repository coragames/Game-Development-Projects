using System.Collections.Generic;
//using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;


public class BoardManager : MonoBehaviour
{
    public class CellData
    {
        public bool passable;
        public CellObject containedFoodObject;
    }

    // Private Variables
    private CellData[,] _boardData;
    private Tilemap _tilemap;
    private Grid _grid;
    private List<Vector2Int> _emptyCellList;

    // Public– Variables
    public int width;
    public int height;
    public Tile[] groundTiles;
    public Tile[] wallTiles;
    public PlayerController Player;
    public FoodObject foodPrefab;
    public WallObject wallPrefab;
    public ExitCellObject exitCellPrefab;
    public Enemy enemyPrefab;
    
    

    // Initialize the level
    public void Init()
    {
        // Initialize list to contain all the available empty cells on the board
        _emptyCellList = new List<Vector2Int>();

        _grid = GetComponentInChildren<Grid>();

        //Initialize the m_BoardData array with board width/height so it’s size is enough to store the data of all the cells
        _boardData = new CellData[width, height];
         
        _tilemap = GetComponentInChildren<Tilemap>();
        // This is to randomly create tiles for the board of size width x height values
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                int tileNumber = Random.Range(0, groundTiles.Length);
                _tilemap.SetTile(new Vector3Int(x, y, 0), groundTiles[tileNumber]);
            }
        }

        // This is to create board walls and to check if the tiles are side/corner tiles
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Tile tile;
                _boardData[x, y] = new CellData();

                // To check if the tiles on the board are side/corner tiles
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    tile = wallTiles[Random.Range(0, wallTiles.Length)];
                    _boardData[x, y].passable = false;
                }                
                else
                {
                    tile  = groundTiles[Random.Range(0, groundTiles.Length)];
                    _boardData[x, y].passable = true;

                    //this is a passable empty cell, add it to the list!
                    _emptyCellList.Add(new Vector2Int(x, y));
                }
                _tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }

        // It calls the Spawn method on PlayerController script with a given cell (1,1) the first lower-left cell that is not a wall       
        Player.Spawn(this, new Vector2Int(1,1));

        //remove the starting point of the player as it's not empty and player is there
        _emptyCellList.Remove(new Vector2Int(1, 1));

        // It assigns the corner tile to the exit tile
        Vector2Int endCoord = new Vector2Int(width - 2, height - 2);
        AddObject(Instantiate(exitCellPrefab), endCoord);
        _emptyCellList.Remove(endCoord);

        // Generate the Food on the board
        GenerateFood();

        // Generate the Walls on the board
        GenerateWall();

    }

    // It takes a Vector2Int cell index and returns the world position of the center for that cell
    public Vector3 CellToWorld(Vector2Int cellIndex)
    {
            return _grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    // returns the information saved on the array m_BoardData of that cell
    public CellData GetCellData(Vector2Int cellIndex)
    {
        if (cellIndex.x < 0 || cellIndex.x >= width || cellIndex.y < 0 || cellIndex.y >= height)
        {
            return null;
        }

        return _boardData[cellIndex.x, cellIndex.y];
    }


    // To generate the food items on the gameboard
    void GenerateFood()
    {
        int foodCount = 5;

        for (int i = 0; i < foodCount; ++i)
        {
            int randomIndex = Random.Range(0, _emptyCellList.Count);
            Vector2Int coord = _emptyCellList[randomIndex];
            
            _emptyCellList.RemoveAt(randomIndex);
                     
            FoodObject newFood = Instantiate(foodPrefab);
            AddObject(newFood, coord);

            //CellData data = _boardData[coord.x, coord.y];

            //newFood.transform.position = CellToWorld(coord);
            //data.containedFoodObject = newFood;
        }
    }

    // To generate the obstacle walls on the gameboard
    void GenerateWall()
    {
        int wallCount = Random.Range(6, 10);

        for (int i = 0; i < wallCount; ++i)
        {
            int randomIndex = Random.Range(0, _emptyCellList.Count);
            Vector2Int coord = _emptyCellList[randomIndex];

            _emptyCellList.RemoveAt(randomIndex);
                        
            WallObject newWall = Instantiate(wallPrefab);
            AddObject(newWall, coord);
            
            //CellData data = _boardData[coord.x, coord.y];
            

            //init the wall
            //newWall.Init(coord);

            //newWall.transform.position = CellToWorld(coord);

            //data.containedFoodObject = newWall;
        }
    }

    public void SetCellTile(Vector2Int cellIndex, Tile tile)
    {
        _tilemap.SetTile(new Vector3Int(cellIndex.x, cellIndex.y, 0), tile);
    }

    public Tile GetCellTile(Vector2Int cellIndex)
    {
        return _tilemap.GetTile<Tile>(new Vector3Int(cellIndex.x, cellIndex.y, 0));
    }

    void AddObject(CellObject obj, Vector2Int coord)
    {
        CellData data = _boardData[coord.x, coord.y];
        obj.transform.position = CellToWorld(coord);
        data.containedFoodObject = obj;
        obj.Init(coord);
    }

    // Clears the board for the Next Level
    public void Clean()
    {
        //no board data, so exit early, nothing to clean
        if(_boardData == null)
        {
            return;
        }            

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                var cellData = _boardData[x, y];

                if (cellData.containedFoodObject != null)
                {
                    //Destroy the GameObject 
                    Destroy(cellData.containedFoodObject.gameObject);
                }

                SetCellTile(new Vector2Int(x,y), null);
            }
        }
    }
}
