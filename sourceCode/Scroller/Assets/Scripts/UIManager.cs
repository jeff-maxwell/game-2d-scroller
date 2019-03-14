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
    public GameObject hurtPanel; 

    private RawImage[] hearts;

    private int lives = 3;

    void Start()
    {
        gameOverCanvas.enabled = false;
        levelCompleteCanvas.enabled = false;
        hurtPanel.gameObject.SetActive(false);

        hearts = playerHud.transform.GetComponentsInChildren<RawImage>();
    }

    public void hit()
    {
        // If Player still alive reduce lives by one when hit
        if (lives > 0)
        {
            lives--;
        }
        // Change the heart color in the HUD
        hearts[lives].color = new Color(0, 0, 0, .75f);
        // Flash the Screen
        StartCoroutine("FlashHurt");
        // If lives = 0 Game Over
        if (lives == 0)
        {
            setGameOver();
        }
    }

    IEnumerator FlashHurt()
    {
        // Show the Hurt Panel for 1/3 of a second
        hurtPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(.3f);
        hurtPanel.gameObject.SetActive(false);
    }

    public int getLives()
    {
        return lives;
    }

    // Set visibiltiy on HUD objects when level is completed
    public void setLevelCompleteHud()
    {
        playerHud.enabled = false;
        playerHud.gameObject.SetActive(false);
        levelCompleteCanvas.enabled = true;
        levelCompleteCanvas.gameObject.SetActive(true);
    }

    // Set visibiltiy on HUD objects when game is over and show Game Over Canvas
    public void setGameOver()
    {
        playerHud.enabled = false;
        playerHud.gameObject.SetActive(false);
        levelCompleteCanvas.enabled = false;
        levelCompleteCanvas.gameObject.SetActive(false);
        gameOverCanvas.enabled = true;
        gameOverCanvas.gameObject.SetActive(true);
    }

    // Restart the level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    // Load the next level
    public void NextLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    // Quit the Game
    public void Quit()
    {
        Application.Quit();
    }
}
