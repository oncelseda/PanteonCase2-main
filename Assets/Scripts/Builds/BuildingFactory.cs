using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : MonoBehaviour
{
   
    public static BuildingFactory instance;
    public GameBoard gameBoard;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public Building CreateBuild(BuildingSO bSO)
    {
        Building b = null;

        b = Instantiate(bSO.Build).GetComponent<Building>();
        b.Initialize(bSO);
        b.gameObject.AddComponent<MoveObject>().Initialize(b.GetComponent<SpriteRenderer>(),true);


        return b;
    }



}
