using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public Canvas playerHud;

    private RawImage[] hearts;

    private int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(playerHud.transform.childCount);
        hearts = playerHud.transform.GetComponentsInChildren<RawImage>();
        Debug.Log(hearts.Length);
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
        if(lives == 0)
        {
            //Call gameover
        }
    }

    public int getLives()
    {
        return lives;
    }
}
