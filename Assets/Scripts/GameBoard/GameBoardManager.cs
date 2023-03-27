using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardManager: MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Transform tiles;
    [SerializeField] private float tileSize;
    public List<Tile> grid;


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


    void GeneratorGameBoard()
    {
        Vector3 middlePoint = new Vector3(width/2,height/2) * tileSize-new Vector3(tileSize/2,tileSize/2);
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                var spawnTile = Instantiate(_tilePrefab,new Vector3(x,y) * tileSize - middlePoint,Quaternion.identity,tiles);
                spawnTile.name = $"Tile{x} {y}";
                
                spawnTile.GetComponent<Tile>().Init((x+y)%2==0);

            }
        }
    }



}