using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "New Skill")]
public class Skill : ScriptableObject
{
    public Sprite icon;
    public string name;
    public string description;
    [SerializeField] public SkillType type;
    public enum SkillType
    {
        Projectile,
        Area,
        Effect,
    }
    public GameObject projectilePrefab;
    public GameObject damageBoxPrefab;
    public float baseDamage;
    public float range;
    public float speed;
    public float effectRadius;
    public float castTime;
    public float cooldown;
    public float duration;
    public int cost;
    public bool mustStayForCast = true;
}
