using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class YouDiedControl : MonoBehaviour
{
    [SerializeField] GameObject youDiedMenu;
    [SerializeField] Button selectedButton;
    [SerializeField] EndOfLevel endOfLevel;

    public bool isPlayerDead = false;

    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void YouDied()
    {
        isPlayerDead = true;
        youDiedMenu.SetActive(true);
        selectedButton.Select();
        playerInput.SwitchCurrentActionMap("UI");
    }

    public void RestartLevel()
    {
        string currentLevel = "Level " + endOfLevel.GetCurrentLevel().ToString();
        SceneManager.LoadScene(currentLevel);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
