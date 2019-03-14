using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public Animator animator;
    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    // Update is called once per frame
    void Update()
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

    }

    private void FixedUpdate()
    {
        //for physics
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackTrigger"))
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<ZombieMovement>().IsAttack = true;
        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Take damage this is handled to kill the player through the KillPlayer script

        }
    }



}
