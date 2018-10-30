using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perTileControl : MonoBehaviour {

    allTileControl allTileControl;
    allTileControl.Tile thisTile;
    // Use this for initialization
    void Start () {

        allTileControl = GameObject.Find("GameManager").GetComponent<allTileControl>();
        allTileControl.AddTile(gameObject);
        thisTile = allTileControl.Tiles.Find(obj => obj.TileObject == gameObject);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name != "PlayerCube")
        {
            thisTile.isTouched = true;
            thisTile.TileObject.GetComponent<MeshRenderer>().material.color = Color.red;
            transform.GetComponent<MeshRenderer>().material.color = Color.red;
            other.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            thisTile.isTouched = true;
            thisTile.TileObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
