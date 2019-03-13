using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float xVelocity = 20.0f;
    public float yVelocity = 40.0f;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private bool facingRight = true;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
        float move = Input.GetAxis("Horizontal");

        if (move != 0)
        {
            //Vertical upward player movement.
            //Get bounds of player's collision box.
            //Look for overlapping colliders in area of same width below player.
            Vector3 max = boxCollider.bounds.max;
            Vector3 min = boxCollider.bounds.min;
            Vector2 corner1 = new Vector2(max.x, min.y - .1f);
            Vector2 corner2 = new Vector2(min.x, min.y - .2f);
            Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

            bool grounded = false;

            if (hit != null) //If a collider was detected under the player...
            {
                grounded = true;
            }

            rigidBody.gravityScale = grounded && move == 0 ? 0 : 1; //Check both on ground and not moving.

            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }

            if (grounded && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))) //Only add force when arrows pressed.
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
                rigidBody.AddForce(new Vector2(xVelocity, yVelocity), ForceMode2D.Impulse);

            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        xVelocity = xVelocity * -1.0f;
    }
}
