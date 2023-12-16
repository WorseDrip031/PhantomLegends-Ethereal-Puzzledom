using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryControl : MonoBehaviour
{
    [SerializeField] GameObject inventoryMenu;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] Button selectedButton;

    [SerializeField] Player player;

    [SerializeField] TextMeshProUGUI healthModificationText;
    [SerializeField] TextMeshProUGUI attackModificationText;
    [SerializeField] TextMeshProUGUI defenseModificationText;
    [SerializeField] TextMeshProUGUI speedModificationText;

    [SerializeField] Button knifeButton;
    [SerializeField] Button woodenSwordButton;
    [SerializeField] Button steelSwordButton;
    [SerializeField] Button mithrilSwordButton;

    [SerializeField] Button clothButton;
    [SerializeField] Button leatherArmorButton;
    [SerializeField] Button steelArmorButton;
    [SerializeField] Button mithrilArmorButton;

    [SerializeField] GameObject weaponIcon;
    [SerializeField] GameObject armorIcon;

    private PlayerInput playerInput;
    private bool isInventoryOpen = false;
    private string activeWeapon;
    private string activeArmor;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {

        if (!gameObject.GetComponent<PauseGameController>().isGamePaused() && isInventoryOpen)
        {
            if (eventSystem.currentSelectedGameObject == knifeButton.gameObject)
            {
                Item item = player.inventory.GetItemByName("Knife");
                updateItemDescription(item);
            }
            else if (eventSystem.currentSelectedGameObject == woodenSwordButton.gameObject)
            {
                Item item = player.inventory.GetItemByName("Wooden Sword");
                updateItemDescription(item);
            }
            else if (eventSystem.currentSelectedGameObject == steelSwordButton.gameObject)
            {
                Item item = player.inventory.GetItemByName("Steel Sword");
                updateItemDescription(item);
            }
            else if (eventSystem.currentSelectedGameObject == mithrilSwordButton.gameObject)
            {
                Item item = player.inventory.GetItemByName("Mithril Sword");
                updateItemDescription(item);
            }
            else if (eventSystem.currentSelectedGameObject == clothButton.gameObject)
            {
                Item item = player.inventory.GetItemByName("Cloth");
                updateItemDescription(item);
            }
            else if (eventSystem.currentSelectedGameObject == leatherArmorButton.gameObject)
            {
                Item item = player.inventory.GetItemByName("Leather Armor");
                updateItemDescription(item);
            }
            else if (eventSystem.currentSelectedGameObject == steelArmorButton.gameObject)
            {
                Item item = player.inventory.GetItemByName("Steel Armor");
                updateItemDescription(item);
            }
            else if (eventSystem.currentSelectedGameObject == mithrilArmorButton.gameObject)
            {
                Item item = player.inventory.GetItemByName("Mithril Armor");
                updateItemDescription(item);
            }
        }
    }

    void updateItemDescription(Item item)
    {
        healthModificationText.text = item.healthModifier.ToString();
        attackModificationText.text = item.attackModifier.ToString();
        defenseModificationText.text = item.defenseModifier.ToString();
        speedModificationText.text = item.speedModifier.ToString();
    }

    public bool IsInventoryOpen()
    {
        return isInventoryOpen;
    }

    void OnOpenInventory()
    {
        if (!gameObject.GetComponent<PauseGameController>().isGamePaused())
        {
            isInventoryOpen = true;
            inventoryMenu.SetActive(true);
            selectedButton.Select();
            playerInput.SwitchCurrentActionMap("UI");

            Debug.Log(player.inventory.GetItems().Count);

            // Wooden Sword
            if (player.inventory.GetItemByName("Wooden Sword") == null)
            {
                woodenSwordButton.gameObject.SetActive(false);
            }
            else
            {
                woodenSwordButton.gameObject.SetActive(true);
            }

            // Steel Sword
            if (player.inventory.GetItemByName("Steel Sword") == null)
            {
                steelSwordButton.gameObject.SetActive(false);
            }
            else
            {
                steelSwordButton.gameObject.SetActive(true);
            }

            // Mithril Sword
            if (player.inventory.GetItemByName("Mithril Sword") == null)
            {
                mithrilSwordButton.gameObject.SetActive(false);
            }
            else
            {
                mithrilSwordButton.gameObject.SetActive(true);
            }

            // Leather Armor
            if (player.inventory.GetItemByName("Leather Armor") == null)
            {
                leatherArmorButton.gameObject.SetActive(false);
            }
            else
            {
                leatherArmorButton.gameObject.SetActive(true);
            }

            // Steel Armor
            if (player.inventory.GetItemByName("Steel Armor") == null)
            {
                steelArmorButton.gameObject.SetActive(false);
            }
            else
            {
                steelArmorButton.gameObject.SetActive(true);
            }

            // Mithril Armor
            if (player.inventory.GetItemByName("Mithril Armor") == null)
            {
                mithrilArmorButton.gameObject.SetActive(false);
            }
            else
            {
                mithrilArmorButton.gameObject.SetActive(true);
            }

            activeWeapon = player.GetActiveWeapon().name;
            if (activeWeapon == "Knife")
            {
                SelectKnife();
            }
            else if (activeWeapon == "Wooden Sword")
            {
                SelectWoodenSword();
            }
            else if (activeWeapon == "Steel Sword")
            {
                SelectSteelSword();
            }
            else if (activeWeapon == "Mithril Sword")
            {
                SelectMithrilSword();
            }


            activeArmor = player.GetActiveArmor().name;
            if (activeArmor == "Cloth")
            {
                SelectCloth();
            }
            else if (activeArmor == "Leather Armor")
            {
                SelectLeatherArmor();
            }
            else if (activeArmor == "Steel Armor")
            {
                SelectSteelArmor();
            }
            else if (activeArmor == "Mithril Armor")
            {
                SelectMithrilArmor();
            }
        }
    }

    public void CloseInventory()
    {
        if (!gameObject.GetComponent<PauseGameController>().isGamePaused())
        {
            inventoryMenu.SetActive(false);
            selectedButton.Select();
            playerInput.SwitchCurrentActionMap("Player");
        }
    }

    public void OnBack()
    {
        if (isInventoryOpen)
        {
            CloseInventory();
            isInventoryOpen = false;
        }
    }

    public void ResetSelectedButton()
    {
        selectedButton.Select();
    }

    public void SelectKnife()
    {
        Transform parent = knifeButton.transform;
        weaponIcon.transform.SetParent(parent);
        weaponIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(30, 0);
        string itemName = "Knife";
        if (activeWeapon != itemName)
        {
            activeWeapon = itemName;
            player.SetActiveWeaponByName(itemName);
        }
    }

    public void SelectWoodenSword()
    {
        Transform parent = woodenSwordButton.transform;
        weaponIcon.transform.SetParent(parent);
        weaponIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(30, 0);
        string itemName = "Wooden Sword";
        if (activeWeapon != itemName)
        {
            activeWeapon = itemName;
            player.SetActiveWeaponByName(itemName);
        }
    }

    public void SelectSteelSword()
    {
        Transform parent = steelSwordButton.transform;
        weaponIcon.transform.SetParent(parent);
        weaponIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(30, 0);
        string itemName = "Steel Sword";
        if (activeWeapon != itemName)
        {
            activeWeapon = itemName;
            player.SetActiveWeaponByName(itemName);
        }
    }

    public void SelectMithrilSword()
    {
        Transform parent = mithrilSwordButton.transform;
        weaponIcon.transform.SetParent(parent);
        weaponIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(30, 0);
        string itemName = "Mithril Sword";
        if (activeWeapon != itemName)
        {
            activeWeapon = itemName;
            player.SetActiveWeaponByName(itemName);
        }
    }

    public void SelectCloth()
    {
        Transform parent = clothButton.transform;
        armorIcon.transform.SetParent(parent);
        armorIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-30, 0);
        string itemName = "Cloth";
        if (activeArmor != itemName)
        {
            activeArmor = itemName;
            player.SetActiveArmorByName(itemName);
        }
    }

    public void SelectLeatherArmor()
    {
        Transform parent = leatherArmorButton.transform;
        armorIcon.transform.SetParent(parent);
        armorIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-30, 0);
        string itemName = "Leather Armor";
        if (activeArmor != itemName)
        {
            activeArmor = itemName;
            player.SetActiveArmorByName(itemName);
        }
    }

    public void SelectSteelArmor()
    {
        Transform parent = steelArmorButton.transform;
        armorIcon.transform.SetParent(parent);
        armorIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-30, 0);
        string itemName = "Steel Armor";
        if (activeArmor != itemName)
        {
            activeArmor = itemName;
            player.SetActiveArmorByName(itemName);
        }
    }

    public void SelectMithrilArmor()
    {
        Transform parent = mithrilArmorButton.transform;
        armorIcon.transform.SetParent(parent);
        armorIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-30, 0);
        string itemName = "Mithril Armor";
        if (activeArmor != itemName)
        {
            activeArmor = itemName;
            player.SetActiveArmorByName(itemName);
        }
    }
}
