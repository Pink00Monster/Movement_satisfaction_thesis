using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovementDash : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    //Moving
    private float horizontal;
    private float speed = 4f;
    private float jumpingPower = 8f;
    private bool isFacingRight = true;

    //Dashing
    public float dashDistance = 1f;
    public int dashDelay = 1;
    private bool isDashing = false;
    private float dashTime = 0;

    void Update()
    {
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (isDashing && (Time.time - dashTime >= dashDelay))
        {
            isDashing = false;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (!isDashing) {
            if (isFacingRight)
            {
                rb.position = new Vector2(rb.position.x + dashDistance, rb.position.y);
            }
            else
            {
                rb.position = new Vector2(rb.position.x - dashDistance, rb.position.y);
            }
            dashTime = Time.time;
            isDashing = true;
        }
    }
}
