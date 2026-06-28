using UnityEngine;
using UnityEngine.Tilemaps;

public class ExitCellObject : CellObject
{
    public Tile endTile;

    public override void Init(Vector2Int coord)
    {
        base.Init(coord);
        GameManager.instance.boardManager.SetCellTile(coord, endTile);
    }

    public override void PlayerEntered()
    {
        //Debug.Log("Reached the exit cell");
        GameManager.instance.NewLevel();        
    }
}
