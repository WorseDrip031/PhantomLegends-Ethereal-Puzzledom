using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] ExpText expText;

    private Canvas canvas;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }

    public Item GetItem() { return item; }

    public void Consume()
    {
        ExpText spawnedExpText = Instantiate(expText);

        if (item.itemType == Item.ItemType.Weapon || item.itemType == Item.ItemType.Armor)
        {
            spawnedExpText.setText(item.name + " Acquired");
        }
        if (item.itemType == Item.ItemType.HealthReplenish)
        {
            spawnedExpText.setText("Health Replenished");
        }
        if (item.itemType == Item.ItemType.HealthIncrease)
        {
            spawnedExpText.setText("+5 Health");
        }
        if (item.itemType == Item.ItemType.AttackIncrease)
        {
            spawnedExpText.setText("+2 Attack");
        }
        if (item.itemType == Item.ItemType.DefenseIncrease)
        {
            spawnedExpText.setText("+1 Defense");
        }
        if (item.itemType == Item.ItemType.SpeedIncrease)
        {
            spawnedExpText.setText("+0.1 Speed");
        }
        if (item.itemType == Item.ItemType.Map)
        {
            spawnedExpText.setText("Map Revealed");
        }

        RectTransform textTransform = spawnedExpText.GetComponent<RectTransform>();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        textTransform.SetParent(canvas.transform);

        Destroy(gameObject);
    }
}
