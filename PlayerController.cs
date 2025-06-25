using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jumping")]
    public float jumpForce = 12f;
    public int maxJumps = 2;

    [Header("Fall Death")]
    [Tooltip("Y position below which the player will die (fall into pit).")]
    public float fallDeathY = -10f;

    private static readonly int SpeedHash         = Animator.StringToHash("Speed");
    private static readonly int GroundedHash      = Animator.StringToHash("Grounded");
    private static readonly int VerticalSpeedHash = Animator.StringToHash("VerticalSpeed");

    private Rigidbody2D rb;
    private Animator    anim;
    private int         jumpCount  = 0;
    private bool        isGrounded = false;

    void Awake()
    {
        rb   = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimation();

        if (transform.position.y < fallDeathY)
            Die();
    }

    void HandleMovement()
    {
        float vx = 0f;
        if (Keyboard.current.leftArrowKey.isPressed)  vx = -moveSpeed;
        if (Keyboard.current.rightArrowKey.isPressed) vx =  moveSpeed;

        rb.linearVelocity = new Vector2(vx, rb.linearVelocity.y);

        if (vx != 0f)
            transform.localScale = new Vector3(Mathf.Sign(vx), 1f, 1f);
    }

    void HandleJump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
            isGrounded = false;
        }
    }

    void UpdateAnimation()
    {
        anim.SetFloat(SpeedHash,         Mathf.Abs(rb.linearVelocity.x));
        anim.SetBool (GroundedHash,      isGrounded);
        anim.SetFloat(VerticalSpeedHash, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount  = 0;
            return;
        }

        if (col.collider.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            isGrounded = false;
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
