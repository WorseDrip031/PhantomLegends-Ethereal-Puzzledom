using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGameController : MonoBehaviour
{
    [SerializeField] GameObject pauseGameMenu;
    [SerializeField] Button selectedButton;

    [SerializeField] YouDiedControl youDiedControl;

    private PlayerInput playerInput;
    private bool _isGamePaused = false;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public bool isGamePaused()
    {
        return _isGamePaused;
    }

    void OnPauseGame()
    {
        if (!_isGamePaused)
        {
            if (!youDiedControl.isPlayerDead)
            {
                _isGamePaused = true;
                pauseGameMenu.SetActive(true);
                Time.timeScale = 0;
                selectedButton.Select();
                playerInput.SwitchCurrentActionMap("UI");
            }
        }
    }

    public void ResumeGame()
    {
        if (_isGamePaused)
        {
            _isGamePaused = false;
            pauseGameMenu.SetActive(false);
            Time.timeScale = 1;
            selectedButton.Select();

            if (gameObject.GetComponent<CharacterStatsControl>().isCharacterStatsOpen())
            {
                gameObject.GetComponent<CharacterStatsControl>().ResetSelectedButton();
            }
            else if (gameObject.GetComponent<InventoryControl>().IsInventoryOpen())
            {
                gameObject.GetComponent<InventoryControl>().ResetSelectedButton();
            }
            else
            {
                playerInput.SwitchCurrentActionMap("Player");
            }
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
