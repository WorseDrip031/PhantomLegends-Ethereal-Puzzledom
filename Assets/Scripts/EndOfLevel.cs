using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour
{
    [SerializeField][Range(1,4)] int currentLevel;

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (currentLevel == 4)
            {
                SceneManager.LoadScene("The End");
            }
            else
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    int nextLevel = currentLevel + 1;
                    SaveSystem.SavePlayer(player, nextLevel);
                    SceneManager.LoadScene("Level " + nextLevel.ToString());
                }
            }
        }
    }
}
