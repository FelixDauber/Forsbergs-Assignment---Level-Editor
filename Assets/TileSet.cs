using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSet : MonoBehaviour
{
    public Tile tilePrefab;
    public Tile[] tiles;
    public int width = 10, height = 10;
    public static TileSet tileSet;
    public Vector2 size = new Vector2(10, 10);
    public Vector2 actualSize;
    public string saveFileName;
    public string SaveFileName { set => saveFileName = value; }

    public string Width { set => size.x = int.Parse(value); }
    public string Height { set => size.y = int.Parse(value); }

    public float offset = 101;

    private void Awake()
    {
        tileSet = this;
        actualSize = size;
        ResetMap();
    }

    [ContextMenu("GenerateMap")]
    public void GenerateBaseMap()
    {
        ClearMap();
        tiles = new Tile[height * width];
        for (int i = 0; i < height * width; i++)
        {
            tiles[i] = Instantiate(tilePrefab);
            tiles[i].transform.SetParent(transform);
        }
        ReOrderMap();
    }

    [ContextMenu("ReOrderMap")]
    void ReOrderMap()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GetTileOnPosition(x, y).transform.position = new Vector3((x + 0.5f) * offset, (y + 0.5f) * offset, transform.position.z) + transform.position;
                GetTileOnPosition(x, y).transform.SetParent(transform);
                GetTileOnPosition(x, y).Setup();
            }
        }
    }

    [ContextMenu("ClearMap")]
    void ClearMap()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i] != null)
                DestroyImmediate(tiles[i].gameObject);
        }
        tiles = new Tile[0];
    }

    Tile GetTileOnPosition(int x, int y)
    {
        return tiles[GetTileID(x, y)];
    }
    int GetTileID(int x, int y)
    {
        return (y * width) + x;
    }

    public Tile[] GetUniqueTiles()
    {
        List<Tile> uniqueTiles = new List<Tile>();
        foreach (Tile tile in tiles)
        {
            if(FindSameTiles(uniqueTiles.ToArray(), tile).Length == 0)
            {
                uniqueTiles.Add(tile);
            }
        }
        return uniqueTiles.ToArray();
    }

    Tile[] FindSameTiles(Tile[] tileArray, Tile tile)
    {
        List<Tile> returnTiles = new List<Tile>();
        foreach (Tile tileItem in tileArray)
        {
            if(tileItem.TileData.name == tile.TileData.name && tileItem.TileData.color == tile.TileData.color)
                returnTiles.Add(tileItem);
        }
        return returnTiles.ToArray();
    }

    [ContextMenu("SaveMap")]
    public void SaveMap()
    {
        string tileJSON = JsonUtility.ToJson(size) + "\n";
        foreach (Tile tile in tiles)
        {
            tileJSON += JsonUtility.ToJson(tile.TileData) + "\n";
        }
        System.IO.File.WriteAllText("Assets/Resources/Maps/" + saveFileName + ".json", tileJSON);
    }

    [ContextMenu("LoadMap")]
    public void LoadMap()
    {
        ClearMap();
        string[] lines = System.IO.File.ReadAllLines("Assets/PotionData.json");
        size = JsonUtility.FromJson<Vector2>(lines[0]);
        actualSize = size;
        GenerateBaseMap();
        for (int i = 0; i < size.x * size.y; i++)
        {
            tiles[i].TileData = JsonUtility.FromJson<TileData>(lines[i+1]);
        }
    }

    public void LoadMap(string filePath)
    {
        ClearMap();
        string[] lines = System.IO.File.ReadAllLines(filePath);
        size = JsonUtility.FromJson<Vector2>(lines[0]);
        actualSize = size;
        GenerateBaseMap();
        for (int i = 0; i < size.x * size.y; i++)
        {
            tiles[i].TileData = JsonUtility.FromJson<TileData>(lines[i + 1]);
        }
    }

    public void UpdateData(TileData original, TileData newData)
    {
        foreach (var tile in tiles)
        {
            if(tile.TileData.name == original.name)
            {
                tile.TileData = newData;
                tile.UpdateColour();
            }
        }
    }

    public void ResetMap()
    {
        width = 10;
        height = 10;
        GenerateBaseMap();
    }
}
