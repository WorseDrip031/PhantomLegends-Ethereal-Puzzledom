using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject loadGameScene;
    [SerializeField] Button loadGameBackButton;
    [SerializeField] GameObject noSaveDataText;
    [SerializeField] GameObject loadLevel1Button;
    [SerializeField] GameObject loadLevel2Button;
    [SerializeField] GameObject loadLevel3Button;
    [SerializeField] GameObject loadLevel4Button;
    [SerializeField] GameObject askForDeleteSaveDataScene;
    [SerializeField] Button noButton;
    [SerializeField] Button yesButton;

    [SerializeField] GameObject showControlsScene;
    [SerializeField] Button controlsBackButton;

    [SerializeField] Button newGameButton;

    private bool isLoadGameSceneOpen = false;

    public void NewGame()
    {
        string path = Application.persistentDataPath + "/level1.plep";
        if (File.Exists(path))
        {
            AskForDeleteSaveData();
        }
        else
        {
            SceneManager.LoadScene("Level 1");
        }
    }

    public void LoadGame()
    {
        Debug.Log(Application.persistentDataPath);
        isLoadGameSceneOpen = true;
        string path = Application.persistentDataPath + "/level1.plep";
        if (!File.Exists(path))
        {
            noSaveDataText.SetActive(true);
            loadLevel1Button.SetActive(false);
            loadLevel2Button.SetActive(false);
            loadLevel3Button.SetActive(false);
            loadLevel4Button.SetActive(false);
        }
        else
        {
            noSaveDataText.SetActive(false);
            loadLevel1Button.SetActive(true);
        }
        path = Application.persistentDataPath + "/level2.plep";
        if (File.Exists(path))
        {
            loadLevel2Button.SetActive(true);
        }
        else
        {
            loadLevel2Button.SetActive(false);
        }
        path = Application.persistentDataPath + "/level3.plep";
        if (File.Exists(path))
        {
            loadLevel3Button.SetActive(true);
        }
        else
        {
            loadLevel3Button.SetActive(false);
        }
        path = Application.persistentDataPath + "/level4.plep";
        if (File.Exists(path))
        {
            loadLevel4Button.SetActive(true);
        }
        else
        {
            loadLevel4Button.SetActive(false);
        }
        gameObject.SetActive(false);
        loadGameScene.SetActive(true);
        loadGameBackButton.Select();
    }

    public void MainMenuFromLoadGame()
    {
        isLoadGameSceneOpen = false;
        loadGameScene.SetActive(false);
        gameObject.SetActive(true);
        newGameButton.Select();
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level 4");
    }

    public void DeleteSaveData()
    {
        for (int i = 1; i <= 4; i++)
        {
            string path = Application.persistentDataPath + "/level" + i + ".plep";
            if (File.Exists (path))
            {
                File.Delete(path);
            }
        }
    }

    public void RefreshLoadGame()
    {
        noSaveDataText.SetActive(true);
        loadLevel1Button.SetActive(false);
        loadLevel2Button.SetActive(false);
        loadLevel3Button.SetActive(false);
        loadLevel4Button.SetActive(false);
    }

    public void AskForDeleteSaveData()
    {
        if (isLoadGameSceneOpen)
        {
            string path = Application.persistentDataPath + "/level1.plep";
            if (File.Exists(path))
            {
                askForDeleteSaveDataScene.SetActive(true);
                noButton.Select();
            }
        }
        else
        {
            askForDeleteSaveDataScene.SetActive(true);
            noButton.Select();
        }
    }

    public void No()
    {
        if (isLoadGameSceneOpen)
        {
            askForDeleteSaveDataScene.SetActive(false);
            loadGameBackButton.Select();
        }
        else
        {
            askForDeleteSaveDataScene.SetActive(false);
            newGameButton.Select();
        }
    }

    public void Yes()
    {
        if (isLoadGameSceneOpen)
        {
            askForDeleteSaveDataScene.SetActive(false);
            loadGameBackButton.Select();
            DeleteSaveData();
            RefreshLoadGame();
        }
        else
        {
            DeleteSaveData();
            SceneManager.LoadScene("Level 1");
        }
    }

    public void ShowControls()
    {
        gameObject.SetActive(false);
        showControlsScene.SetActive(true);
        controlsBackButton.Select();
    }

    public void MainMenuFromShowControls()
    {
        showControlsScene.SetActive(false);
        gameObject.SetActive(true);
        newGameButton.Select();
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
