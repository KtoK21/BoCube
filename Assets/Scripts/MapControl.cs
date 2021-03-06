﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapControl : MonoBehaviour {

    public Material prevMaterial;
    public GameObject TerrainCube;
    public GameObject PlayerCube;

    public class Tile
    {
        public GameObject TileObject { get; set; }
        public bool isTouched { get; set; }
    }
    public static List<Tile> Tiles = new List<Tile>();
    public static List<GameObject> TerrainCubes = new List<GameObject>();
    public static bool isInitiating = true;

    List<Vector3> Coordinates = new List<Vector3>();


    // Use this for initialization
	void Awake () {

        GenerateTerrain();
        InitiatePlayerCube();
        isInitiating = false;

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
        int Seed = 62;
        //int Seed = 101;
        //Debug.Log(Seed);
        Coordinates.Add(TerrainCubes[Seed].transform.position);
        DestroyandRemoveCube(TerrainCubes[Seed]);

        int Count = 0;


        do
        {
            Debug.Log(Count);
            Vector3 target = Coordinates[Count];
            for (int i = 0; i < 3; i++)
            {
                
                Vector3 direction = new Vector3();

                if (Coordinates.Count % 3 == 0) //grow toward floor
                    direction = new Vector3(0, -2, 0);

                else if (Coordinates.Count % 3 == 1) //grow toward leftWall
                    direction = new Vector3(-2, 0, 0);

                else if (Coordinates.Count % 3 == 2) //grow toward rightWall
                    direction = new Vector3(0, 0, 2);

                //   Debug.Log("it is " + Coordinates.Count + "and " + direction);
                //ExtendtoWall(target, direction);

                int Length = Random.Range(3, 5);
                for (int j = 0; j < Length; j++)
                {
                    target += direction;
                    if (TerrainCubes.Exists(obj => obj.transform.position == target))
                    {
                        //   Debug.Log(Coordinates.Count);
                        Coordinates.Add(target);

                        DestroyandRemoveCube(TerrainCubes.Find(obj => obj.transform.position == target));
                    }
                }

            }

        Count++;

        } while (Coordinates.Count > Count);

        Debug.Log(Count);
    }

    void ExtendtoWall(Vector3 target, Vector3 direction)
    {
        int Length = Random.Range(2, 5);
        for(int i = 0; i< Length; i++)
        {
            target += direction;
            if (TerrainCubes.Exists(obj => obj.transform.position == target))
            {
             //   Debug.Log(Coordinates.Count);
                Coordinates.Add(target);

                DestroyandRemoveCube(TerrainCubes.Find(obj => obj.transform.position == target));
            }
        }
    }

    void InitiatePlayerCube()
    {
        int target = Random.Range(0, Coordinates.Count);
        Instantiate(PlayerCube, Coordinates[target], Quaternion.identity);
    }

    void DestroyandRemoveCube(GameObject target)
    {
        TerrainCubes.Remove(target);
        Destroy(target);

    }
    
    /*
    public void GenerateTerrain()
    {
        GameObject Terrains = GameObject.Find("Terrains");

        foreach (Vector3 target in Coordinates)
        {
            if (Random.Range(0,30) == 1 && !TerrainCubes.Exists(obj => obj.transform.position == target))
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
    
    void InitiatePlayerCube()
    {
        foreach (Vector3 target in Coordinates)
        {
            if (Random.Range(0, 5) == 1 && !TerrainCubes.Exists(obj => obj.transform.position == target))
            {
                GameObject playercube = Instantiate(PlayerCube, target, Quaternion.identity);
                return;
            }
        }
    }
    */
    /// <summary>
    /// Debug functions for testing below
    /// </summary>

    public void DebugAlltileCheck()
    {
        foreach(Tile eachTile in Tiles)
        {
            eachTile.isTouched = true;
        }
    }

    public void DebugRegenerateTerrain()
    {
        /*
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
        */
        SceneManager.LoadScene("Main");
        }

    public void DebugChangeTileColor()
    {
        foreach(Tile eachTile in Tiles)
        {
            eachTile.TileObject.GetComponent<perTileControl>().isTouched = false;
        }
    }

}
