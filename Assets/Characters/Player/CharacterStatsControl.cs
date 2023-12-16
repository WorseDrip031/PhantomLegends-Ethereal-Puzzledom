using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterStatsControl : MonoBehaviour
{
    [SerializeField] GameObject characterStatsMenu;
    [SerializeField] Button selectedButton;

    [SerializeField] TextMeshProUGUI playerLevelText;
    [SerializeField] TextMeshProUGUI availableStatPointsText;
    [SerializeField] TextMeshProUGUI xpNeededForNextLevelText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI defenseText;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI xpText;

    [SerializeField] Player player;

    private PlayerInput playerInput;
    private bool _isCharacterStatsOpen = false;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>(); 
    }

    public bool isCharacterStatsOpen()
    {
        return _isCharacterStatsOpen;
    }

    void OnOpenCharacterStats()
    {
        if (!gameObject.GetComponent<PauseGameController>().isGamePaused())
        {
            _isCharacterStatsOpen = true;
            characterStatsMenu.SetActive(true);
            selectedButton.Select();
            playerInput.SwitchCurrentActionMap("UI");
            ReloadStats();
        }
    }

    public void CloseCharacterStats()
    {
        if (!gameObject.GetComponent<PauseGameController>().isGamePaused())
        {
            characterStatsMenu.SetActive(false);
            selectedButton.Select();
            playerInput.SwitchCurrentActionMap("Player");
        }
    }

    public void OnBack()
    {
        if (_isCharacterStatsOpen)
        {
            CloseCharacterStats();
            _isCharacterStatsOpen = false;
        }
    }

    public void ReloadStats()
    {
        playerLevelText.text = player.getPlayerLevel().ToString();
        availableStatPointsText.text = player.getAvailableStatPoints().ToString();
        xpNeededForNextLevelText.text = player.getXpNeededForNextLevel().ToString();

        healthText.text = player.GetPlayerMaxHealthString();
        attackText.text = player.GetPlayerAttackString();
        defenseText.text = player.GetPlayerDefenseString();
        speedText.text = player.GetPlayerSpeedString();
        xpText.text = player.getPlayerXP().ToString();
    }

    public void ResetSelectedButton()
    {
        selectedButton.Select();
    }
}
