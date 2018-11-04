using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour {

    public Material prevMaterial;
    public GameObject TerrainCube;
    public class Tile
    {
        public GameObject TileObject { get; set; }
        public bool isTouched { get; set; }
    }
    public static List<Tile> Tiles = new List<Tile>();
    public static List<GameObject> TerrainCubes = new List<GameObject>();

    List<Vector3> Coordinates = new List<Vector3>();
    GameObject Terrains;
    // Use this for initialization
	void Awake () {

        Terrains = GameObject.Find("Terrains");
        InitiateTerrainCoordinate();
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

    public void InitiateTerrainCoordinate()
    {

        Vector3 target = new Vector3(4, 1, -4);
        for(int i=0; i<5; i++)
        {
            Coordinates.Add(target);
            target += new Vector3(-2, 0, 0);
        }
        for(int i=0; i<5; i++)
        {
            for(int j=1; j<5; j++)
            {
                Coordinates.Add(Coordinates[i] + new Vector3(0, 0, 2 * j));
            }
        }
        for(int i=0; i<25; i++)
        {
            for(int j=1; j<5; j++)
            {
                Coordinates.Add(Coordinates[i] + new Vector3(0, 2 * j, 0));

            }
        }
        
    }

    public void GenerateTerrain()
    {
        GameObject Terrains = GameObject.Find("Terrains");

        foreach (Vector3 target in Coordinates)
        {
            if (Random.Range(0,10) == 1 && !TerrainCubes.Exists(obj => obj.transform.position == target))
            {
                GameObject Tcube = Instantiate(TerrainCube, target, Quaternion.identity);
                Tcube.transform.SetParent(Terrains.transform);
                TerrainCubes.Add(Tcube);

                int choice = Random.Range(0, 3);
                Vector3 direction = new Vector3();
                if (choice == 0) //grow toward floor
                    direction = new Vector3(0, -2, 0);

                else if (choice == 1) //grow toward leftWall
                    direction = new Vector3(-2, 0, 0);

                else if (choice == 2) //grow toward leftWall
                     direction = new Vector3(0, 0, 2);

                ExtendtoWall(Tcube, direction);
            }
        }
    }

    void ExtendtoWall(GameObject Tcube, Vector3 direction)
    {
        Vector3 target = Tcube.transform.position + direction;
        
        while(Coordinates.Exists(obj => Equals(target, obj)) && !TerrainCubes.Exists(obj => obj.transform.position == target))
        {
            GameObject NextCube = Instantiate(TerrainCube, target, Quaternion.identity);
            NextCube.transform.SetParent(Terrains.transform);
            TerrainCubes.Add(NextCube);

            target += direction;
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
        foreach (Transform child in Terrains.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(Tile eachTile in Tiles)
        {
            eachTile.TileObject.GetComponent<perTileControl>().isTouched = false;
            eachTile.isTouched = false;
            eachTile.TileObject.GetComponent<MeshRenderer>().material = prevMaterial;
        }
        TerrainCubes.Clear();

        GenerateTerrain();
    }

    public void DebugChangeTileColor()
    {
        foreach(Tile eachTile in Tiles)
        {
            eachTile.TileObject.GetComponent<perTileControl>().isTouched = false;
        }
    }

}
