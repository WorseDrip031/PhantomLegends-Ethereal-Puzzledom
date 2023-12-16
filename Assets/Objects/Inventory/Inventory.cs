using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList = new List<Item>();

    public Inventory()
    {
        itemList = new List<Item>();
        AddItem(new Item { itemType = Item.ItemType.Weapon, name = "Knife", healthModifier = 0, attackModifier = 0, defenseModifier = 0, speedModifier = 0 });
        AddItem(new Item { itemType = Item.ItemType.Armor, name = "Cloth", healthModifier = 0, attackModifier = 0, defenseModifier = 0, speedModifier = 0 });
        /*
        AddItem(new Item { itemType = Item.ItemType.Weapon, name = "Wooden Sword", healthModifier = 0, attackModifier = 10, defenseModifier = 0, speedModifier = 0 });
        AddItem(new Item { itemType = Item.ItemType.Weapon, name = "Steel Sword", healthModifier = 0, attackModifier = 25, defenseModifier = 0, speedModifier = -0.25f });
        AddItem(new Item { itemType = Item.ItemType.Weapon, name = "Mithril Sword", healthModifier = 0, attackModifier = 20, defenseModifier = 0, speedModifier = 0.1f });
        AddItem(new Item { itemType = Item.ItemType.Armor, name = "Leather Armor", healthModifier = 0, attackModifier = 0, defenseModifier = 5, speedModifier = 0 });
        AddItem(new Item { itemType = Item.ItemType.Armor, name = "Steel Armor", healthModifier = 0, attackModifier = 0, defenseModifier = 20, speedModifier = -0.5f });
        AddItem(new Item { itemType = Item.ItemType.Armor, name = "Mithril Armor", healthModifier = 10, attackModifier = 0, defenseModifier = 15, speedModifier = 0.25f });
        */
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItems()
    {
        return itemList;
    }

    public Item GetDefaultWeapon()
    {
        return itemList.Find(item => item.name == "Knife");
    } 

    public Item GetDefaultArmor()
    {
        return itemList.Find(item => item.name == "Cloth");
    }

    public Item GetItemByName(string name)
    {
        return itemList.Find(item => item.name == name);
    }
}
