using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float maxHealth;
    public float health;
    public float attack;
    public float defense;
    public float speed;
    public int xp;
    public int playerLevel;
    public int availableStatPoints;
    public string activeWeapon;
    public string activeArmor;
    public string[] inventoryItems;

    public PlayerData(Player player)
    {
        float[] floatArray = player.getFloatPlayerInfo();
        int[] intArray = player.getIntPlayerInfo();
        string[] stringArray = player.getStringPlayerInfo();

        maxHealth = floatArray[0];
        health = floatArray[1];
        attack = floatArray[2];
        defense = floatArray[3];
        speed = floatArray[4];

        xp = intArray[0];
        playerLevel = intArray[1];
        availableStatPoints = intArray[2];

        activeWeapon = stringArray[0];
        activeArmor = stringArray[1];

        inventoryItems = new string[stringArray.Length - 2];
        for (int i = 2; i < stringArray.Length; i++)
        {
            inventoryItems[i-2] = stringArray[i];
        }
    }
}
