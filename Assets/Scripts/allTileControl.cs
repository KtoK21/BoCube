using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allTileControl : MonoBehaviour {

    public class Tile
    {
        public GameObject TileObject { get; set; }
        public bool isTouched { get; set; }
    }
    public static List<Tile> Tiles = new List<Tile>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Tile eachTile in Tiles)
        {
            if (!eachTile.isTouched)
                goto False;
        }
        StaticVariable.IsGameFinished = true;
        return;

        False:
            return;
	}
    public void AddTile(GameObject tile)
    {
        Tile newTile = new Tile();
        newTile.TileObject = tile;
        newTile.isTouched = false;
        Tiles.Add(newTile);
    }

    public void DebugAlltileCheck()
    {
        foreach(Tile eachTile in Tiles)
        {
            eachTile.isTouched = true;
        }
    }

}
