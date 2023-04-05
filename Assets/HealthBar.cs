using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset;


    public void SetHealth(int health,int maxHealth)
    {
        
        slider.value = health;
        slider.maxValue = maxHealth;

     
    }

    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);

    }

}
