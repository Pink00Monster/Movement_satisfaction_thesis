using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class BasicMovement: MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float maxSpeed;
    public float acceleration;
    public float jumpingPower;

    //Moving
    private float speed;
    private float horizontal;
    private bool isFacingRight = true;
    private float update = 0.0f;

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

        update += Time.deltaTime;
        if (update > 1.0f)
        {
            update = 0.0f;

            if (speed > 0) {
                speed = speed--;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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
        if (maxSpeed > (speed + acceleration)) {
            speed = acceleration + speed;
        } else {
            speed = maxSpeed;
        }

        horizontal = context.ReadValue<Vector2>().x;
    }
}
