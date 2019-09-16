using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tree", menuName = "Tree")]
public class Tree : ScriptableObject
{
    public string name;
    public string description;
    public int health;
}
