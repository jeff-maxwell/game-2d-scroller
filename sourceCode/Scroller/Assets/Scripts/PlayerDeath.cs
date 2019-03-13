using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private bool isAlive = true;
    public GameObject gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == true && transform.position.y < -25.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}
