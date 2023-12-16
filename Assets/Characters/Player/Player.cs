using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float MaxHealth;
    [SerializeField] float Health;
    [SerializeField] float Attack;
    [SerializeField] float Defense;
    [SerializeField] float Speed;
    [SerializeField] int XP;

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] HealthBar healthBar;
    [SerializeField] GameObject mapLineOfSight;
    [SerializeField] EndOfLevel endOfLevel;
    [SerializeField] YouDiedControl youDiedControl;
    [SerializeField] ExpText expText;

    [SerializeField] WorldItem WoodenSword;
    [SerializeField] WorldItem SteelSword;
    [SerializeField] WorldItem MithrilSword;
    [SerializeField] WorldItem LeatherArmor;
    [SerializeField] WorldItem SteelArmor;
    [SerializeField] WorldItem MithrilArmor;

    private bool isAlive = true;
    private int playerLevel = 1;
    private int availableStatPoints = 0;
    private int[] xpNeededForLevelUp = {0, 100, 350, 400};
    public Inventory inventory;
    private Item activeWeapon;
    private Item activeArmor;
    private bool isMapLineOfSightActive = false;
    private float mapTTL;
    private Canvas canvas;

    private void Awake()
    {
        inventory = new Inventory();
        activeWeapon = inventory.GetDefaultWeapon();
        activeArmor = inventory.GetDefaultArmor();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }

    void Start()
    {
        int currentLevel = endOfLevel.GetCurrentLevel();
        if (currentLevel == 1)
        {
            string path = Application.persistentDataPath + "/level1.plep";
            if (!File.Exists(path))
            {
                SaveSystem.SavePlayer(this, currentLevel);
            }
        }
        else
        {
            PlayerData data = SaveSystem.LoadPlayer(currentLevel);
            MaxHealth = data.maxHealth;
            Health = MaxHealth;
            Attack = data.attack;
            Defense = data.defense;
            Speed = data.speed;
            XP = data.xp;
            playerLevel = data.playerLevel;
            availableStatPoints = data.availableStatPoints;
            foreach (string itemName in data.inventoryItems)
            {
                if (itemName == "Wooden Sword")
                {
                    inventory.AddItem(WoodenSword.GetItem());
                }
                if (itemName == "Steel Sword")
                {
                    inventory.AddItem(SteelSword.GetItem());
                }
                if (itemName == "Mithril Sword")
                {
                    inventory.AddItem(MithrilSword.GetItem());
                }
                if (itemName == "Leather Armor")
                {
                    inventory.AddItem(LeatherArmor.GetItem());
                }
                if (itemName == "Steel Armor")
                {
                    inventory.AddItem(SteelArmor.GetItem());
                }
                if (itemName == "Mithril Armor")
                {
                    inventory.AddItem(MithrilArmor.GetItem());
                }
            }
            activeWeapon = inventory.GetItemByName(data.activeWeapon);
            activeArmor = inventory.GetItemByName(data.activeArmor);
        }
        healthBar.SetMaxHealth(MaxHealth);
    }

    public float[] getFloatPlayerInfo()
    {
        float[] floatArray = { MaxHealth, Health, Attack, Defense, Speed };
        return floatArray;
    }

    public int[] getIntPlayerInfo()
    {
        int[] intArray = { XP, playerLevel, availableStatPoints };
        return intArray;
    }

    public string[] getStringPlayerInfo()
    {

        List<string> strList = new List<string>();
        strList.Add(activeWeapon.name);
        strList.Add(activeArmor.name);
        foreach (Item item in inventory.GetItems())
        {
            strList.Add(item.name);
        }
        string[] stringArray = strList.ToArray();
        return stringArray;
    }


    public Item GetActiveWeapon()
    {
        return activeWeapon;
    }

    public Item GetActiveArmor()
    {
        return activeArmor;
    }

    public void SetActiveWeaponByName(string name)
    {
        activeWeapon = inventory.GetItemByName(name);
        healthBar.ChangeMaxHealthAccordingItems(getPlayerMaxHealth());
    }

    public void SetActiveArmorByName(string name)
    {
        activeArmor = inventory.GetItemByName(name);
        healthBar.ChangeMaxHealthAccordingItems(getPlayerMaxHealth());
    }

    public void increaseHealth()
    {
        if (availableStatPoints > 0)
        {
            availableStatPoints -= 1;
            MaxHealth += 5;
            Health = MaxHealth;
            healthBar.ChangeMaxHealthAccordingItems(MaxHealth);
            gameObject.GetComponent<CharacterStatsControl>().ReloadStats();
        }
    }

    public void increaseAttack()
    {
        if (availableStatPoints > 0)
        {
            availableStatPoints -= 1;
            Attack += 2;
            gameObject.GetComponent<CharacterStatsControl>().ReloadStats();
        }
    }

    public void increaseDefense()
    {
        if (availableStatPoints > 0)
        {
            availableStatPoints -= 1;
            Defense += 1;
            gameObject.GetComponent<CharacterStatsControl>().ReloadStats();
        }
    }

    public void increaseSpeed()
    {
        if (availableStatPoints > 0)
        {
            availableStatPoints -= 1;
            Speed += 0.1f;
            gameObject.GetComponent<CharacterStatsControl>().ReloadStats();
        }
    }

    public float getPlayerMaxHealth()
    {
        return MaxHealth + activeWeapon.healthModifier + activeArmor.healthModifier;
    }

    public string GetPlayerMaxHealthString()
    {
        float healthModifier = activeWeapon.healthModifier + activeArmor.healthModifier;
        if (healthModifier >= 0)
        {
            return MaxHealth.ToString() + " (+" + healthModifier.ToString() + ")";
        }
        return MaxHealth.ToString() + " (" + healthModifier.ToString() + ")";
    }

    public float getPlayerHealth()
    {
        return Health;
    }

    public float getPlayerAttack()
    {
        return Attack + activeWeapon.attackModifier + activeArmor.attackModifier;
    }

    public string GetPlayerAttackString()
    {
        float attackModifier = activeWeapon.attackModifier + activeArmor.attackModifier;
        if (attackModifier >= 0)
        {
            return Attack.ToString() + " (+" + attackModifier.ToString() + ")";
        }
        return Attack.ToString() + " (" + attackModifier.ToString() + ")";
    }

    public float getPlayerDefense()
    {
        return Defense + activeWeapon.defenseModifier + activeArmor.defenseModifier;
    }

    public string GetPlayerDefenseString()
    {
        float defenseModifier = activeWeapon.defenseModifier + activeArmor.defenseModifier;
        if (defenseModifier >= 0)
        {
            return Defense.ToString() + " (+" + defenseModifier.ToString() + ")";
        }
        return Defense.ToString() + " (" + defenseModifier.ToString() + ")";
    }

    public float getPlayerSpeed()
    {
        return Speed + activeWeapon.speedModifier + activeArmor.speedModifier;
    }

    public string GetPlayerSpeedString()
    {
        float speedModifier = activeWeapon.speedModifier + activeArmor.speedModifier;
        if (speedModifier >= 0)
        {
            return Speed.ToString() + " (+" + speedModifier.ToString() + ")";
        }
        return Speed.ToString() + " (" + speedModifier.ToString() + ")";
    }

    public float getPlayerXP()
    {
        return XP;
    }

    public int getPlayerLevel()
    {
        return playerLevel;
    }

    public int getAvailableStatPoints()
    {
        return availableStatPoints;
    }

    public int getXpNeededForNextLevel()
    {
        if (playerLevel >= xpNeededForLevelUp.Length)
        {
            return -1;
        }
        return (xpNeededForLevelUp[playerLevel] - XP);
    }

    public void increaseXP(int amount)
    {
        XP += amount;
        checkForLevelup();
    }

    void checkForLevelup()
    {
        if (XP >= xpNeededForLevelUp[playerLevel])
        {
            levelUp();
            healthBar.SetMaxHealth(MaxHealth);
            Health = MaxHealth;
            checkForLevelup();
        }
    }

    void levelUp()
    {
        ExpText spawnedExpText = Instantiate(expText);
        spawnedExpText.setText("Level Up");
        RectTransform textTransform = spawnedExpText.GetComponent<RectTransform>();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        textTransform.SetParent(canvas.transform);
        playerLevel += 1;
        availableStatPoints += 2;
        Debug.Log("Level reached: " + playerLevel);
    }

    public void InflictDamage(float damage, Vector2 knockback)
    {
        float damageDealt = damage - getPlayerDefense();
        if (damageDealt > 0)
        {
            animator.SetTrigger("BeingHit");
            Health -= damageDealt;
            healthBar.SetHealth(Health);
            if ((Health <= 0) && (isAlive))
            {
                isAlive = false;
                Defeated();
            }
            rb.AddForce(knockback);
        }
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
        rb.simulated = false;
    }

    public void RemovePlayer()
    {
        gameObject.SetActive(false);
        youDiedControl.YouDied();
        isAlive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "WorldItem")
        {
            WorldItem worldItem = other.GetComponent<WorldItem>();
            if (worldItem != null)
            {
                Item item = worldItem.GetItem();
                if (item.itemType == Item.ItemType.Weapon || item.itemType == Item.ItemType.Armor)
                {
                    inventory.AddItem(item);
                }
                if (item.itemType == Item.ItemType.HealthReplenish)
                {
                    Health = MaxHealth;
                    healthBar.SetMaxHealth(MaxHealth);
                }
                if (item.itemType == Item.ItemType.HealthIncrease)
                {
                    availableStatPoints += 1;
                    increaseHealth();
                }
                if (item.itemType == Item.ItemType.AttackIncrease)
                {
                    availableStatPoints += 1;
                    increaseAttack();
                }
                if (item.itemType == Item.ItemType.DefenseIncrease)
                {
                    availableStatPoints += 1;
                    increaseDefense();
                }
                if (item.itemType == Item.ItemType.SpeedIncrease)
                {
                    availableStatPoints += 1;
                    increaseSpeed();
                }
                if (item.itemType == Item.ItemType.Map)
                {
                    mapLineOfSight.SetActive(true);
                    isMapLineOfSightActive = true;
                    mapTTL = 0f;
                }
                worldItem.Consume();
            }
        }
    }

    void Update()
    {
        if (isMapLineOfSightActive)
        {
            mapTTL += Time.deltaTime;
            if (mapTTL > 1f)
            {
                mapLineOfSight.SetActive(false);
                isMapLineOfSightActive = false;
                Debug.Log("Map Inactive");
            }
        }
    }
}
