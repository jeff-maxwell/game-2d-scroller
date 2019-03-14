using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private bool isAlive = true;
    public GameObject gameOverCanvas;

    void Update()
    {
        // If the Player is alive and they fall off the map kill the player
        if (isAlive == true && transform.position.y < -25.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Set the Alive Flag to false
        isAlive = false;
        // Show the Game Over Canvas
        gameOverCanvas.SetActive(true);
        // Reset the Time Scale
        Time.timeScale = 0;
    }
}
