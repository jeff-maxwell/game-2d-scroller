using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillPlayer : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Canvas gameOverCanvas = GameObject.FindGameObjectWithTag("GameOverCanvas")
                                              .GetComponent<Canvas>();

            gameOverCanvas.enabled = true;

            Destroy(collision.gameObject);
        }
    }
}
