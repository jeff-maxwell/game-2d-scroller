using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public Animator animator;
    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private float hitTime; //This is the time between being hit so hit() isnt called 21 times a millisecond
    private bool hit = false;
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

        if (hit)
        {
            hitTime += Time.deltaTime;
            if (hitTime > 1.2)
            {
                hit = false;
                hitTime = 0;
            }
        }

    }

    private void FixedUpdate()
    {
        //for physics
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(hit);
        if(!hit)
        {
            Debug.Log("Fired");
            if (col.gameObject.CompareTag("obstical"))
            {
                hit = true;
                GetComponent<GameInfo>().hit();
                if(GetComponent<GameInfo>().getLives() == 0)
                {
                    animator.Play("Dying", 0);
                    //Need to add script that turns off all controlls
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



}
