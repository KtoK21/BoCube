using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perTileControl : MonoBehaviour {

    MapControl MapControl;
    MapControl.Tile thisTile;
    public Material AfterTileMaterial;
    public bool isTouched;
    // Use this for initialization
    void Start() {
        MapControl = GameObject.Find("GameManager").GetComponent<MapControl>();
        thisTile = MapControl.AddTile(gameObject);
        if (transform.parent.name == "Ceiling")
        {
            thisTile.isTouched = true;
            isTouched = true;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name != "PlayerCube")
        {
            thisTile.isTouched = true;
            isTouched = true;
            thisTile.TileObject.GetComponent<MeshRenderer>().material = AfterTileMaterial;
            other.GetComponent<MeshRenderer>().material = AfterTileMaterial;

        }
        else
        {
            thisTile.isTouched = true;
            isTouched = true;
            thisTile.TileObject.GetComponent<MeshRenderer>().material = AfterTileMaterial;
        }
    }
}
