using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Building",menuName ="Scriptables/Building")]
public class BuildingSO : ScriptableObject
{
    [SerializeField] private Sprite buildSprite;
    public Sprite BuildSprite { get { return buildSprite; } }
    
    [SerializeField] private string buildSize;
    public string BuildSize { get { return buildSize; } }
    
    [SerializeField] private int buildMaxHealth;
    public int BuildMaxHealth { get { return buildMaxHealth; } } 
    
    [SerializeField] private GameObject build;
    public GameObject Build { get { return build; } }
    
    [SerializeField] private List<GameObject> producibles;
    public List<GameObject> Producibles { get { return producibles; } }





}
