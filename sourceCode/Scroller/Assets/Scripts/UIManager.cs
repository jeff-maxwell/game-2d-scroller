using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Canvas gameOverCanvas;
    public Canvas playerHud;
    public Canvas levelCompleteCanvas;

    private RawImage[] hearts;

    private int lives = 3;
    /* public GameObject gameOverCanvas;
    public string restartGameSceneName;
    public bool isRestartGame = false;*/

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.enabled = false;
        levelCompleteCanvas.enabled = false;
        hearts = playerHud.transform.GetComponentsInChildren<RawImage>();
    }

    public void hit()
    {
        lives--;
        hearts[lives].color = new Color(0, 0, 0, .75f);
        Debug.Log("Hit");
        if (lives == 0)
        {
            gameOverCanvas.enabled = true;
        }
    }

    public int getLives()
    {
        return lives;
    }

    public void setLevelCompleteHud()
    {
        levelCompleteCanvas.enabled = true;
        playerHud.enabled = false;
    }

    public void RestartLevel()
    {
        //if (isRestartGame)
        //{
            //SceneManager.LoadScene(restartGameSceneName);
            //Time.timeScale = 1;
        //}
        //else
        //{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        //}
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
