using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCubeControl : MonoBehaviour {

    MapControl MapControl;

	// Use this for initialization
	void Start () {
        MapControl = GameObject.Find("GameManager").GetComponent<MapControl>();
        foreach(Transform child in transform)
        {
            MapControl.AddTerrainCube(child.gameObject);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
