using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard: MonoBehaviour
{
    public int width, height;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Transform tiles;
    [SerializeField] private float tileSize;
    public Tile [,] grid { get; private set; }
    
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (tiles == null)
            return;
        Gizmos.matrix = tiles.localToWorldMatrix;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(width, height)*tileSize);
    }

#endif
    private void Start()
    {
        GeneratorGameBoard();
    }


    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        Vector3 middlePoint = new Vector3(width / 2, height / 2) * tileSize - new Vector3(tileSize / 2, tileSize / 2);
        Vector3 zeroPoint = transform.position - middlePoint;
        Vector3 diff = worldPosition - zeroPoint;
        x = (int)diff.x;
        y = (int)diff.y;
        
       
    }
    
    void GeneratorGameBoard()
    {
        grid = new Tile[width, height];
        Vector3 middlePoint = new Vector3(width/2,height/2) * tileSize-new Vector3(tileSize/2,tileSize/2);
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                var spawnTile = Instantiate(_tilePrefab,new Vector3(x,y) * tileSize - middlePoint,Quaternion.identity,tiles);
                spawnTile.name = $"Tile{x} {y}";
                grid[x,y]=spawnTile;
                spawnTile.x = x;
                spawnTile.y = y;
                
                spawnTile.GetComponent<Tile>().Init((x+y)%2==0);

            }
        }
    }



}