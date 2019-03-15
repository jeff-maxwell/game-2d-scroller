using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public Animator animator;
    public UIManager uiManager;
    public AudioSource audioSource;
    
    public Vector2 winPosition;

    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    // This is the time between being hit so hit() isnt called 21 times a millisecond
    private float hitTime; 

    private bool gameOver = false;
    private bool levelComplete = false;
    private bool jump = false;
    private bool crouch = false;

    // If we get hit mutilple times by different enemies, each enemy will have 
    // his own cooldown time before he can be hit agian
    private List<float> hitCoolDowns = new List<float>();
    //This is the list of corresonding enemies that will be toggled off after cool down = 0;
    private List<Collider2D> hitColliders = new List<Collider2D>(); 

    void Update()
    {
        if (!gameOver && !levelComplete)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            // Detector for current hits on the players.
            hitDetector(); 

            // Space is the Jump key
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }

            // If the down arrow is pressed the crouch is true
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                crouch = true;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                crouch = false;
            }


            if(transform.position.x > winPosition.x && transform.position.y < winPosition.y)
            {
                uiManager.setLevelCompleteHud();
                animator.speed = 0;
                levelComplete = true;
            }

            // If the character goes below a certain position on the map kill the player
            if(transform.position.y < -15)
            {
                animator.Play("Dying", 0);
                gameOver = true;
                uiManager.setGameOver();
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }

    private void FixedUpdate()
    {
        // As long as the game and level are not over/complete allow the character to move
        if (!gameOver && !levelComplete)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Make enemy run at you
        if (col.gameObject.CompareTag("AttackTrigger"))
        {
            col.gameObject.transform.parent.gameObject.GetComponent<ZombieMovement>().IsAttack = true;
        }
    }

    public void OnLanding()
    {
        // Finish the jump animation
        animator.SetBool("IsJumping", false);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // If the player impacts an Enemy take damage and update HUD
        if (col.gameObject.CompareTag("Enemy"))
        {
            uiManager.hit();
            hitCoolDowns.Add(1.2f);
            hitColliders.Add(col.gameObject.GetComponent<PolygonCollider2D>());
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), col.gameObject.GetComponent<PolygonCollider2D>(), true);
            // If lives = 0 the player is dead
            if (uiManager.getLives() == 0)
            {
                audioSource.Play();
                animator.Play("Dying", 0);
                gameOver = true;
            }
            else
            {
                audioSource.Play();
                animator.Play("Hurt", 0);
            }
        }

        // Big Enemy does twice as much damage
        if (col.gameObject.CompareTag("Big-Enemy"))
        {
            uiManager.hit();
            uiManager.hit();
            hitCoolDowns.Add(1.2f);
            hitColliders.Add(col.gameObject.GetComponent<PolygonCollider2D>());
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), col.gameObject.GetComponent<PolygonCollider2D>(), true);
            if (uiManager.getLives() == 0)
            {
                audioSource.Play();
                animator.Play("Dying", 0);
                gameOver = true;
            }
            else
            {
                audioSource.Play();
                animator.Play("Hurt", 0);
            }
        }
    }

    private void hitDetector()
    {
        for(int i = 0; i < hitCoolDowns.Count; i++)
        {
            hitCoolDowns[i] -= Time.deltaTime;
            if(hitCoolDowns[i] < 0)
            {
                Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), hitColliders[i], false);
                hitCoolDowns.RemoveAt(0);
                hitColliders.RemoveAt(0);
            }
        }
    }

}
