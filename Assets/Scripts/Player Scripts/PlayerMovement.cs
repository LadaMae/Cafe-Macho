using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    public Rigidbody2D rb; // Reference to the Rigidbody2D component

    private Vector2 movement;
    private Vector2 lastMovementDir; // Stores the player's movement direction

    public bool canMove = true;
    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(!canMove) return;

        // Get input from the player
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        movement.y = Input.GetAxisRaw("Vertical"); // W/S or Up/Down
        bool isIdle = movement.x == 0 && movement.y == 0;
        if (isIdle)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("IsMoving", false);
        }
        else
        {
            lastMovementDir = movement;
            animator.SetFloat("HorizontalMovement", movement.x);
            animator.SetFloat("VerticalMovement", movement.y);
            animator.SetBool("IsMoving", true);
        }
    }

    void FixedUpdate()
    {
        // Move the player 
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void LockPFreeroam()
    {
        canMove = false;
    }

    public void UnlockPFreeroam()
    {
        canMove = true;
    }
}

