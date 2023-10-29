using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newtileclass", menuName = "Tile Atlas")]
public class TileAtlas : ScriptableObject
{
    public TileClass Stone;
    public TileClass Dirt;
    public TileClass Grass;
    public TileClass Log;
    public TileClass Leaf;

    public TileClass coal;
    public TileClass iron;
    public TileClass gold;
    public TileClass diamond;
}
