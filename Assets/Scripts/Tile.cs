using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileData _tiledata = new TileData();
    public TileData TileData
    {
        get => _tiledata;
        set
        {
            _tiledata = value;
            UpdateColour();
        }
    }
    Image image;

    public void Setup()
    {
        UpdateColour();
    }
    private void OnValidate()
    {
        UpdateColour();
    }
    public void UpdateColour()
    {
        if(image == null)
        {
            image = GetComponent<Image>();
        }
        image.color = TileData.color;
    }
    public void SetTileToSelected()
    {
        if (TileTypes.currentTile != null)
        {
            this.TileData = TileTypes.currentTile.TileData;
            UpdateColour();
        }
    }
}
[System.Serializable]
public class TileData
{
    public string name = "Grass";
    public Color color;
}
