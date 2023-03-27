using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPool : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> pooledSoldier;
        public GameObject soldierPrefab;
        public int soldierPoolSize;

    }


    [SerializeField] private Pool[] pools = null;
    public static SoldierPool instance;


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


        for (int i = 0; i < pools.Length; i++)
        {

            pools[i].pooledSoldier = new Queue<GameObject>();

            for (int j = 0; j < pools[i].soldierPoolSize; j++)
            {
                GameObject soldier = Instantiate(pools[i].soldierPrefab);
                soldier.SetActive(false);

                pools[i].pooledSoldier.Enqueue(soldier);
            }


        }

    }

    public GameObject GetPooledSoldier(int soldierType)
    {
        GameObject soldier = pools[soldierType].pooledSoldier.Dequeue();
        soldier.SetActive(true);
        pools[soldierType].pooledSoldier.Enqueue(soldier);


        return soldier;
    }


}




