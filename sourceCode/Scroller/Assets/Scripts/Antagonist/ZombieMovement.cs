using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public PlayerController controller;
    public Animator animator;
    public float runSpeed = 40f;
    public GameObject player;
    public float pickupDistance;

    private float horizontalMove = 0.1f;

    // Update is called once per frame
    void Update()
    {

            horizontalMove = horizontalMove * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    private void FixedUpdate()
    {
        //for physics
        controller.Move(-1 * horizontalMove * Time.fixedDeltaTime, false, false);
        
    }

    
}
