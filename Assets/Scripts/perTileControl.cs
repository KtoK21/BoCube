using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perTileControl : MonoBehaviour {

    MapControl MapControl;
    MapControl.Tile thisTile;
    public Material AfterTileMaterial;
    public bool isTouched;
    // Use this for initialization
    void Awake() {
        MapControl = GameObject.Find("GameManager").GetComponent<MapControl>();
        thisTile = MapControl.AddTile(gameObject);
        /*       if (transform.parent.name == "Ceiling")
               {
                   thisTile.isTouched = true;
                   isTouched = true;
               }
          */
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        string targetName = other.transform.parent.name;

        thisTile.isTouched = true;
        isTouched = true;

        if (targetName == "Ceiling" || targetName == "LeftBorder" || targetName == "RightBorder")
            return;

        thisTile.TileObject.GetComponent<MeshRenderer>().material = AfterTileMaterial;

       
        //It should be like this when finish. Upper one is for test (to see color changing)

  /*      if (other.transform.name != "PlayerCube")
        {

            thisTile.isTouched = true;
            isTouched = true;
        }
        else
        {
            thisTile.isTouched = true;
            isTouched = true;
            thisTile.TileObject.GetComponent<MeshRenderer>().material = AfterTileMaterial;
        }
  */
    }

    private void OnDestroy()
    {
        MapControl.Tiles.Remove(thisTile);   
        
    }
}
