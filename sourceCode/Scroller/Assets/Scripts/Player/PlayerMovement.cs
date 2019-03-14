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
    private float hitTime; //This is the time between being hit so hit() isnt called 21 times a millisecond

    private bool hit = false;
    private bool gameOver = false;
    private bool levelComplete = false;
    private bool jump = false;
    private bool crouch = false;

    private Collider2D hitCollider; //Collider that player ran into. Need this to disable collision between player and collider after impact
                                  //Then Re-enable for collision in the update() function.

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
                    Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), hitCollider, false);
                }
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
        if (!hit)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                hit = true;
                uiManager.hit();
                hitCollider = (Collider2D) col.gameObject.GetComponent<PolygonCollider2D>();
                Physics2D.IgnoreCollision(GetComponent<PolygonCollider2D>(), hitCollider, true);
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
    }



}
