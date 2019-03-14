using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public Animator animator;
    public UIManager uiManager;


    public Vector2 winPosition;

    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private float hitTime; //This is the time between being hit so hit() isnt called 21 times a millisecond

    //private bool hit = false;
    private bool gameOver = false;
    private bool levelComplete = false;
    private bool jump = false;
    private bool crouch = false;

    private List<float> hitCoolDowns = new List<float>();      //If we get hit mutilple times by different enemies, each enemy will have his own cooldown time before he can be hit agian
    private List<Collider2D> hitColliders = new List<Collider2D>(); //This is the list of corresonding enemies that will be toggled off after cool down = 0;

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && !levelComplete)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            hitDetector(); //Detector for current hits on the players.

            if (Input.GetKeyDown(KeyCode.Space)) //maybe add key for controller.
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow)) //maybe add key for controller.
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
        //for physics
        if (!gameOver && !levelComplete)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //make enemy run at you
        if (col.gameObject.CompareTag("AttackTrigger"))
        {
            col.gameObject.transform.parent.gameObject.GetComponent<ZombieMovement>().IsAttack = true;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            uiManager.hit();
            hitCoolDowns.Add(1.2f);
            hitColliders.Add(col.gameObject.GetComponent<PolygonCollider2D>());
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), col.gameObject.GetComponent<PolygonCollider2D>(), true);
            if (uiManager.getLives() == 0)
            {
                animator.Play("Dying", 0);
                gameOver = true;
            }
            else
            {
                animator.Play("Hurt", 0);
            }
        }

        if (col.gameObject.CompareTag("Big-Enemy"))
        {
            uiManager.hit();
            uiManager.hit();
            hitCoolDowns.Add(1.2f);
            hitColliders.Add(col.gameObject.GetComponent<PolygonCollider2D>());
            Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), col.gameObject.GetComponent<PolygonCollider2D>(), true);
            if (uiManager.getLives() == 0)
            {
                animator.Play("Dying", 0);
                gameOver = true;
            }
            else
            {
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
