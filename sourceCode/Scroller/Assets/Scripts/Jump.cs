using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float xVelocity = 0.5f;
    public float yVelocity = 10.0f;

    private Rigidbody2D rigidBody;
    private bool facingRight = true;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        float move = Input.GetAxis("Horizontal");

        if (move != 0)
        {
            if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
                Flip();
            }

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            rigidBody.AddForce(new Vector2(xVelocity, yVelocity), ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        xVelocity = xVelocity * -1.0f;
    }
}
