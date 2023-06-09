using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building,IProducible
{
    [SerializeField] private Transform soldierPosition;
    private int downYPosition=13;

    private void CreateSoldier(int soldierType)
    {

        GameObject soldier = SoldierPool.instance.GetPooledSoldier(soldierType);
        if (transform.position.y > -downYPosition)
        {
            soldier.transform.position = soldierPosition.position;
        }
        else
        {
            soldier.transform.position = transform.TransformPoint(-soldierPosition.localPosition);
        }

    }

    public void Produce(int i)
    {
        CreateSoldier(i);

    }
}
