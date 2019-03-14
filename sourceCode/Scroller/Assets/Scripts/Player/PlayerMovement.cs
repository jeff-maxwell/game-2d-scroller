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

    private bool hit = false;
    private bool gameOver = false;
    private bool levelComplete = false;
    private bool jump = false;
    private bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && !levelComplete)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

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

            if (hit) //Used to pause the collide with the character and an item/enemy
            {
                hitTime += Time.deltaTime;
                if (hitTime > 1.2)
                {
                    hit = false;
                    hitTime = 0;
                }
            }

            if(transform.position.x > winPosition.x && transform.position.y < winPosition.y)
            {
                uiManager.setLevelCompleteHud();
                animator.speed = 0;
                levelComplete = true;
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



        Debug.Log(hit);
        if(!hit)
        {
            if (col.gameObject.CompareTag("obstical"))
            {
                hit = true;
                uiManager.hit();
                if(uiManager.getLives() == 0)
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

    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

 



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Take damage this is handled to kill the player through the KillPlayer script

        }
    }



}
