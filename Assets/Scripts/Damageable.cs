using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]

public class Damageable : MonoBehaviour
{
    public int currentHealth { get; protected set; }
    public HealthBar healthbar;


    public bool IsInNeighbour(Vector3 worldPosition)

    {


        Bounds neighbourBounds = GetComponent<Collider2D>().bounds;
        neighbourBounds.extents += Vector3.one;



        return neighbourBounds.ClosestPoint(worldPosition) == worldPosition;
    }
    public void TakeDamage(int damage,Damageable enemy)
    {
        currentHealth -= damage;
        healthbar.slider.value = currentHealth;
       
        if (currentHealth == 0)
        {
            OnDead();
        }
        else if(TryGetComponent(out ICounterAttack onAttack))
        {
            onAttack.onAttacked(enemy);
        }
    }

    protected virtual void OnDead()
    {
        Destroy(gameObject);
    }

}
