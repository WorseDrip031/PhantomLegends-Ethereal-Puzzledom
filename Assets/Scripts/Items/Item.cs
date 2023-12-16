using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Map,
        HealthReplenish,
        HealthIncrease,
        AttackIncrease,
        DefenseIncrease,
        SpeedIncrease,
    }

    public ItemType itemType;
    public string name;
    public float healthModifier;
    public float attackModifier;
    public float defenseModifier;
    public float speedModifier;

}
