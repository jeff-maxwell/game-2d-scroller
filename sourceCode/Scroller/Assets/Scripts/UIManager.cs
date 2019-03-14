using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Canvas gameOverCanvas = GameObject.FindGameObjectWithTag("GameOverCanvas")
                                              .GetComponent<Canvas>();

        gameOverCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
