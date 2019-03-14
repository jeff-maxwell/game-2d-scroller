using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public bool IsAttack = false;
    public Animator animator;
    public float runSpeed = 10f;


    private float horizontalMove = -0.5f;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        if (IsAttack)
        {
            this.transform.Translate(horizontalMove * runSpeed * Time.fixedDeltaTime, 0, 0);
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }

    }

}
