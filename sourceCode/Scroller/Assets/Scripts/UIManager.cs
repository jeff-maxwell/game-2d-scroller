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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        gameOverCanvas.enabled = false;
        levelCompleteCanvas.enabled = false;
        hearts = playerHud.transform.GetComponentsInChildren<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void GameOver()
    {

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
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
