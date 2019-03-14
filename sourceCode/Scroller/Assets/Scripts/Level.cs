using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public string sceneName;
    public bool showMenu = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("Player"))
        {
            if (showMenu)
            {
                Canvas gameOverCanvas = GameObject.FindGameObjectWithTag("GameOverCanvas")
                                  .GetComponent<Canvas>();

                UIManager ui = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
                ui.isRestartGame = true;
                ui.restartGameSceneName = sceneName;

                gameOverCanvas.enabled = true;

                Destroy(collision.gameObject);
            }
            else {
                SceneManager.LoadScene(sceneName);
            }
        }*/
            
    }
}
