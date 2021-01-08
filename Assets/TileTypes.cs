using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTypes : MonoBehaviour
{
    public TileEditor tilePrefab;
    public TileEditor[] tiles;
    TileSet tileset;

    public static TileEditor currentTileEditor;
    public static Tile currentTile;

    private void OnValidate()
    {
        //Refresh...
    }

    private void Start()
    {
        RefreshTiles();
        AddWaterTile();
    }

    public static void SetTile(TileEditor tileEditor)
    {
        if(tileEditor == currentTileEditor)
        {
            return;
        }
        if(currentTileEditor != null)
            currentTileEditor.UpdateHighLight(false);
        currentTileEditor = tileEditor;
        currentTile = tileEditor.tile;
    }

    [ContextMenu("RefreshTiles")]
    public void RefreshTiles()
    {
        ClearTiles();
        AddTiles();
    }
    void AddTiles()
    {
        Tile[] uniqueTiles = TileSet.tileSet.GetUniqueTiles();
        tiles = new TileEditor[uniqueTiles.Length];
        for (int i = 0; i < uniqueTiles.Length; i++)
        {
            tiles[i] = Instantiate(tilePrefab, transform);
            tiles[i].SetUp(uniqueTiles[i]);
        }
    }
    void ClearTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
                DestroyImmediate(tiles[i].gameObject);
        }
        //tiles = new Tile[0];
    }
    bool TilesContains(string name)
    {
        foreach (var tile in tiles)
        {
            if (tile.tile.TileData.name == name)
            {
                return true;
            }
        }
        return true;
    }

    public void NewTile(string name)
    {
        if (name == "" && !TilesContains(name))
        {
            return;
        }
        TileEditor newTile = Instantiate(tilePrefab, transform);
        TileEditor[] newTiles = new TileEditor[tiles.Length + 1];
        for (int i = 0; i < tiles.Length; i++)
        {
            newTiles[i] = tiles[i];
        }
        newTiles[newTiles.Length - 1] = newTile;
        newTile.tile.TileData.name = name;
        newTile.SetUp();
    }
    public TileEditor NewTile_WReturn(string name)
    {
        if (name == "" && !TilesContains(name))
        {
            return null;
        }
        TileEditor newTile = Instantiate(tilePrefab, transform);
        TileEditor[] newTiles = new TileEditor[tiles.Length + 1];
        for (int i = 0; i < tiles.Length; i++)
        {
            newTiles[i] = tiles[i];
        }
        newTiles[newTiles.Length - 1] = newTile;
        newTile.tile.TileData.name = name;
        newTile.SetUp();
        return newTile;
    }

    public void AddWaterTile()
    {
        NewTile_WReturn("Water").UpdateColour(Color.blue);
    }
}
