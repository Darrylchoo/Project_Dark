using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Axe,
    Spear,
    Sword
}

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponDatabase : ScriptableObject
{
    public WeaponType weaponType;
    public float durability;
    public float damage;
    public float attackRange;
    public float rarity;
}
