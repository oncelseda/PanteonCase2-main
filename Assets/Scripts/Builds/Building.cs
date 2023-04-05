using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public abstract class Building : Damageable
{
    protected BuildingSO buildingInfo;
    


    
    
    public virtual void Initialize(BuildingSO buildingSO)
    {
        buildingInfo = buildingSO;
        currentHealth = buildingSO.BuildMaxHealth;
        healthbar.SetHealth(currentHealth, buildingSO.BuildMaxHealth);


    }


    protected virtual void OnMouseDown()
    {
        InformationPanelUI.instance.SetBuildInfo(buildingInfo,this);
    }



}


