using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Soldier",menuName ="Scriptables/Soldier" +
    "")]
public class SoldierSO : ScriptableObject
{

    [SerializeField] private int soldierMaxHealth;
    public int SoldierMaxHealth { get { return soldierMaxHealth; } }  
    
    [SerializeField] private int damage;
    public int Damage { get { return damage; } }  
    
    [SerializeField] private float speed;
    public float Speed { get { return speed; } } 
    
    [SerializeField] private float attackCooldown;
    public float AttackCooldown { get { return attackCooldown; } }  
    


    
    
   





}
