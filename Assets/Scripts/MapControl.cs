using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour {

    public class Tile
    {
        public GameObject TileObject { get; set; }
        public bool isTouched { get; set; }
    }
    public static List<Tile> Tiles = new List<Tile>();
    public static List<GameObject> TerrainCubes = new List<GameObject>();
	// Use this for initialization
	void Start () {
        GenerateTerrain();
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
    public Tile AddTile(GameObject tile)
    {
        Tile newTile = new Tile();
        newTile.TileObject = tile;
        newTile.isTouched = false;
        Tiles.Add(newTile);
        return newTile;
    }

    public void AddTerrainCube(GameObject terraincube)
    {
        TerrainCubes.Add(terraincube);
    }
    //for each position among 125 kinds(5x5x5 in map), start from base(base map like floor, leftwall, rightwall), 50% chance to instantiate terrain cube. If yes, 50% chance to extend. Do this for 75 tile(every tile in floor, lestwall, rightwall). If target position already has terrain cube, pass.

    public void GenerateTerrain()
    {
        foreach(GameObject Tcube in TerrainCubes)
        {
            if (Random.Range(0,10) == 1)
            {
                Tcube.SetActive(true);
                int choice = Random.Range(0, 3);
                Vector3 direction = new Vector3();
                if (choice == 0) //grow toward floor
                    direction = new Vector3(0, -2, 0);

                else if (choice == 1) //grow toward leftWall
                    direction = new Vector3(-2, 0, 0);

                else if (choice == 2) //grow toward leftWall
                     direction = new Vector3(0, 0, 2);

                Generate(Tcube, direction);
            }
        }
    }

    void Generate(GameObject Tcube, Vector3 direction)
    {
        GameObject NextCube = TerrainCubes.Find(obj => obj.transform.position == Tcube.transform.position + direction);
        while(NextCube != null)
        {
            if (NextCube.activeSelf)
                Debug.Log("!!!");
            NextCube.SetActive(true);
            NextCube = TerrainCubes.Find(obj => obj.transform.position == NextCube.transform.position + direction);

        }
    }


    public void DebugAlltileCheck()
    {
        foreach(Tile eachTile in Tiles)
        {
            eachTile.isTouched = true;
        }
    }

    public void DebugRegenerateTerrain()
    {
        foreach (GameObject Tcube in TerrainCubes)
        {
            Tcube.SetActive(false);
        }
        GenerateTerrain();
    }

}
