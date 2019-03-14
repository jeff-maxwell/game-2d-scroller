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
    /* public GameObject gameOverCanvas;
    public string restartGameSceneName;
    public bool isRestartGame = false;*/

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.enabled = false;
        levelCompleteCanvas.enabled = false;
        hurtPanel.gameObject.SetActive(false);

        hearts = playerHud.transform.GetComponentsInChildren<RawImage>();
    }

    public void hit()
    {
        lives--;
        hearts[lives].color = new Color(0, 0, 0, .75f);
        StartCoroutine("FlashHurt");
        if (lives == 0)
        {
            setGameOver();
        }
    }

    IEnumerator FlashHurt()
    {
        hurtPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        hurtPanel.gameObject.SetActive(false);
    }

    public int getLives()
    {
        return lives;
    }

    public void setLevelCompleteHud()
    {
        playerHud.enabled = false;
        playerHud.gameObject.SetActive(false);
        levelCompleteCanvas.enabled = true;
        levelCompleteCanvas.gameObject.SetActive(true);
    }

    public void setGameOver()
    {
        playerHud.enabled = false;
        playerHud.gameObject.SetActive(false);
        levelCompleteCanvas.enabled = false;
        levelCompleteCanvas.gameObject.SetActive(false);
        gameOverCanvas.enabled = true;
        gameOverCanvas.gameObject.SetActive(true);
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
