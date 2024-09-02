using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    public Rigidbody2D rb; // Reference to the Rigidbody2D component

    private Vector2 movement; // Stores the player's movement direction

    void Update()
    {
        // Get input from the player
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        movement.y = Input.GetAxisRaw("Vertical"); // W/S or Up/Down
    }

    void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}

