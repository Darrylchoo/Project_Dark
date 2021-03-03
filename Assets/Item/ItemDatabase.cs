using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Battery,
    HealthPack,
    Jetpack
}

[CreateAssetMenu(menuName = "Item")]
public class ItemDatabase : ScriptableObject
{
    public ItemType itemType;
    public float time;
    public int amount;
    public int lives;
}
