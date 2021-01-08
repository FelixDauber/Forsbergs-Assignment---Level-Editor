using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileEditor : MonoBehaviour
{
    public Tile tile;
    public Text tileNameText;
    public Image background;
    public Color standardColor;
    public Color selectionColor;

    public Slider sliderR, sliderG, sliderB;

    public float Red { get => tile.TileData.color.r; set => UpdateColour(new Color(value, tile.TileData.color.g, tile.TileData.color.b)); }
    public float Green { get => tile.TileData.color.r; set => UpdateColour(new Color(tile.TileData.color.r, value, tile.TileData.color.b)); }
    public float Blue { get => tile.TileData.color.r; set => UpdateColour(new Color(tile.TileData.color.r, tile.TileData.color.g, value)); }

    public string Name
    { 
        get => tile.TileData.name;
        set
        {
            tile.TileData.name = value;
            UpdateName();
        }
    }

    public void SetUp()
    {
        tile.Setup();
        UpdateName();
        background = GetComponent<Image>();
        standardColor = background.color;
        sliderR.value = tile.TileData.color.r;
        sliderG.value = tile.TileData.color.g;
        sliderB.value = tile.TileData.color.b;
    }
    public void SetUp(Tile tile)
    {
        this.tile.TileData = tile.TileData;
        SetUp();
    }
    public void SetCurrentSelectedTile()
    {
        UpdateHighLight(true);
        TileTypes.SetTile(this);
    }
    public void UpdateHighLight(bool isSelected)
    {
        if (isSelected)
        {
            background.color = selectionColor;
        }
        else
        {
            background.color = standardColor;
        }
    }
    public void UpdateName()
    {
        tileNameText.text = tile.TileData.name;
    }
    public void UpdateColour(Color color)
    {
        TileSet.tileSet.UpdateData(tile.TileData, tile.TileData);
        tile.TileData.color = color;
        tile.UpdateColour();
    }
}
