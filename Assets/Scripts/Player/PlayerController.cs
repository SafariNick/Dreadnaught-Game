using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private int jumpCount = 1; // Track the number of jumps
    [SerializeField] private int maxJumpCount = 2; // Maximum number of jumps allowed
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckRadius = 0.02f; // Radius for ground check
    [SerializeField] private GroundCheck groundChecker; // Ground checker component
                                                        
    //components
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D col;
    private Animator anim;
    private GroundCheck groundCheck;

    //Colliders
    private Collider2D hit; // Collider for ground detection
    private Collider2D hitArea; // Collider for ground detection area

    private LayerMask groundLayer; // Layer for ground detection
    private Vector2 groundCheckPos => new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        groundLayer = LayerMask.GetMask("Ground");

        if (groundLayer == 0)
        {
            Debug.LogError("Ground layer not set. Please assign the Ground layer in the inspector.");
            return;
        }
        groundCheck = new GroundCheck(col, groundLayer, groundCheckRadius);
    }

    void Update()
    {
        float hValue = Input.GetAxis("Horizontal");
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        SpriteFlip(hValue);

        rb.linearVelocityX = hValue * moveSpeed;
        groundCheck.CheckIsGrounded();
        //anim.SetFloat("Speed", Mathf.Abs(hValue));

        if (!Input.GetButtonDown("Fire1") && Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Fire");
        }
        if (currentState.IsName("Fire"))
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Apply jump force
            jumpCount++;
            anim.SetBool("isJumping", true);
        }
        if (groundCheck.IsGrounded)
        {
            jumpCount = 1; // Reset jump count when grounded
            anim.SetBool("isJumping", false);
        }
       //when player is jumping and left mouse button is clicked, set the animation to fistSlam.
        if (Input.GetButtonDown("Fire1") && currentState.IsName("jump"))
        {
            anim.SetTrigger("FistSlam");
        }


        anim.SetFloat("hValue", Mathf.Abs(hValue));
        anim.SetBool("isGrounded", groundCheck.IsGrounded);
    }

    void SpriteFlip(float hValue)
    {
        if (hValue > 0 && sr.flipX)
        {
            sr.flipX = false;
        }
        else if (hValue < 0 && !sr.flipX)
        {
            sr.flipX = true;
        }
    }
}
