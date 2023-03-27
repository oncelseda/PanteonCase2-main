using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _darkColor;
    
    private int x;
    private int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public Tile cameFromTile;



    public override string ToString()
    {
        return x + "," + y;
    }




    public void Init(bool isChangeTileColor)
    {
        gameObject.GetComponent<SpriteRenderer>().color = isChangeTileColor ? _darkColor : _baseColor;
    }









}
