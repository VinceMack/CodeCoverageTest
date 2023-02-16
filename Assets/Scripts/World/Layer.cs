using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    private List<Tile> tiles = new List<Tile>();
    [SerializeField] private int layerLength, layerHeight, layerNumber;

    public Tile GetTile(int x, int y)
    {
        return tiles[y*layerLength+x];
    }

    public void AddTile(Tile newTile)
    {
        tiles.Add(newTile);
    }

    public void InitializeLayer(int length, int height, int layerNumber)
    {
        layerLength = length;
        layerHeight = height;
        this.layerNumber = layerNumber;
    }

    public int GetLayerNumber()
    {
        return layerNumber;
    }
}
