using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class TileMouseOver : MonoBehaviour {
    private Tilemap tilemap;

    private Tile normalTile;
    public Tile mouseOnTile;

    private Ray ray;
    private Vector3Int posMouseOnGrid;
    private Vector3Int savePosMouseOnGrid;
    private bool first = true;
    void Start()
    {
        
        tilemap = gameObject.GetComponent<Tilemap>();
        savePosMouseOnGrid = new Vector3Int(0, 0, 0);
        
    }
    
    private void OnMouseOver()
    {
       
    }
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        posMouseOnGrid = tilemap.WorldToCell(new Vector3(ray.origin.x, ray.origin.y, 0));
        if (first&& (tilemap.HasTile(posMouseOnGrid)))
        {
            normalTile = (Tile)tilemap.GetTile(posMouseOnGrid);
            first = false;
        }
        
        if ((savePosMouseOnGrid != posMouseOnGrid)&&!first)
        {
            if (tilemap.HasTile(savePosMouseOnGrid))
            {                
                tilemap.SetTile(savePosMouseOnGrid, normalTile);
            }
            savePosMouseOnGrid = posMouseOnGrid;
            if (tilemap.HasTile(posMouseOnGrid))
            {
                normalTile = (Tile)tilemap.GetTile(posMouseOnGrid);
                tilemap.SetTile(posMouseOnGrid, mouseOnTile);
            }
        }

        
    }
}
