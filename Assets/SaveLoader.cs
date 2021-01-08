using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SaveLoader : MonoBehaviour
{
    public Button buttonPrefab;
    private void OnEnable()
    {
        foreach (var item in transform)
        {
            Destroy((GameObject)item);
        }
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Maps");
        foreach (var asset in textAssets)
        {
            MakeButton(asset);
        }
    }

    void MakeButton(TextAsset asset)
    {
        Button newButton = Instantiate(buttonPrefab, transform);
        newButton.GetComponentInChildren<Text>().text = asset.name;
        newButton.onClick.AddListener(delegate { TileSet.tileSet.LoadMap(AssetDatabase.GetAssetPath(asset)); });
    }
}
