using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "New Building")]
public class Building : ScriptableObject
{
    public string name;
    public Sprite icon;
    public GameObject prefab;
    [SerializeField] public Type type;
    public int woodCost;
    public string description;
    
    public enum Type
    {
        Building,
        Area,
        Defensive,
    }
}
