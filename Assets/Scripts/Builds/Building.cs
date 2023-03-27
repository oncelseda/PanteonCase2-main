using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public abstract class Building:MonoBehaviour
{
    protected BuildingSO buildingInfo;
    protected int currentBuildHealth;
    
    
    
    
    public virtual void Initialize(BuildingSO buildingSO)
    {
        buildingInfo = buildingSO;
        currentBuildHealth = buildingSO.BuildMaxHealth;
    }


    protected virtual void OnMouseDown()
    {
        InformationPanelUI.instance.SetBuildInfo(buildingInfo,this);
    }

    

}


