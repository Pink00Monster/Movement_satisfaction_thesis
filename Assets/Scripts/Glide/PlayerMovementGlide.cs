using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementGlide : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator;

    public float speed = 4;
    public float jumpingPower = 8;

    private float horizontal;
    private bool isFacingRight = true;

    [SerializeField]
    private float glidingspeed = 1;

    private float _initialGravityScale;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _initialGravityScale = rb.gravityScale;
    }


    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (IsGrounded())
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }
        else if (!IsGrounded() && rb.velocity.y < 0)
        {
            animator.SetBool("Falling", true);
            animator.SetBool("Jumping", false);
        }
        else if (!IsGrounded() && rb.velocity.y > 0)
        {
            animator.SetBool("Jumping", true);
        }


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
    }

    public void Glide(InputAction.CallbackContext context)
    {
        if (context.performed && !IsGrounded())
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(_rigidbody.velocity.x, y: -glidingspeed);
        }
        if (context.canceled)
        {
            rb.gravityScale = _initialGravityScale;
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
}

