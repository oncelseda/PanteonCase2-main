using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSlotUI : MonoBehaviour
{
    [SerializeField] BuildingSO buildSO;
    

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = buildSO.BuildSprite;
    }
    public void CreateBuild()
    {
        BuildingFactory.instance.CreateBuild(buildSO);
    }
    
   




}
