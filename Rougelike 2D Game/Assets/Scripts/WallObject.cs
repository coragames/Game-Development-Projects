using UnityEngine;
using UnityEngine.Tilemaps;

public class WallObject : CellObject
{
   public Tile obstacleTile;
   public int maxHealth = 3;

   private int _healthPoint;
   private Tile _originalTile;
  
   public override void Init(Vector2Int cell)
   {
        //This will call the Init function from the class it inherits from (CellObject)
       base.Init(cell);

       _healthPoint = maxHealth;
       _originalTile = GameManager.instance.boardManager.GetCellTile(cell);

       GameManager.instance.boardManager.SetCellTile(cell, obstacleTile);
   }

   public override bool PlayerWantsToEnter()
   {
        _healthPoint -= 1;
        
        if (_healthPoint > 0)
        {
            return false;
        }

        GameManager.instance.boardManager.SetCellTile(m_Cell, _originalTile);
        Destroy(gameObject);
        return true;
   }
}