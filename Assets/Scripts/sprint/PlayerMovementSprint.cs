using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementSprint : MonoBehaviour
{
    public float speed = 4;
    public float sprintSpeed = 8;
    public float jumpingPower = 8;
    public Rigidbody2D rb;
    public LayerMask groundLayer;

    public Transform groundCheck;

    private float horizontal;
    private bool isFacingRight = true;
    private bool isSprinting = false;

    void Start(){
         rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

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
        if (Input.GetButton("Sprint")) {
            rb.velocity = new Vector2(horizontal * sprintSpeed, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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

    public void Sprint(InputAction.CallbackContext context)
    {
        isSprinting = !isSprinting;
        if(isSprinting)
        {

        }
    }
}
