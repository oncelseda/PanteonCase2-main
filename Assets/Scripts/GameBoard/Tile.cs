using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _darkColor;
    
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;
    public bool isWalkable = true;

    public Tile cameFromTile;


    public void Init(bool isChangeTileColor)
    {
        gameObject.GetComponent<SpriteRenderer>().color = isChangeTileColor ? _darkColor : _baseColor;
    }


    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }






}
