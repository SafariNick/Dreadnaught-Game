using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private int jumpCount = 1; // Track the number of jumps
    [SerializeField] private int maxJumpCount = 2; // Maximum number of jumps allowed
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    
    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckRadius = 0.02f; // Radius for ground check
    [SerializeField] private GroundCheck groundChecker; // Ground checker component
                                                        
    //components
    private LayerMask groundLayer;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D col;
    private Animator anim;
    private GroundCheck groundCheck;
    public Shoot shoot; // Reference to the Shoot component for firing projectiles
    private Coroutine jumpForceChange = null;
    //Colliders
    private Collider2D hit; // Collider for ground detection
    private Collider2D hitArea; // Collider for ground detection area

    //private Vector2 groundCheckPos => new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        shoot = GetComponent<Shoot>();

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
        float vValue = Input.GetAxisRaw("Vertical");
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        SpriteFlip(hValue);

        rb.linearVelocityX = hValue * moveSpeed;
        groundCheck.CheckIsGrounded();
        //anim.SetFloat("Speed", Mathf.Abs(hValue));

        //if ( Input.GetButtonDown("Fire1"))
        //{
        //    anim.SetTrigger("Fire");
        //}
        if (currentState.IsName("Fire"))
        {
            rb.linearVelocity = Vector2.zero;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("BigGun", true);

        }
        if (Input.GetButtonUp("Fire2"))
        {
            anim.SetBool("BigGun", false);
        }
        if (currentState.IsName("Fire"))
        {
            rb.linearVelocity = Vector2.zero;
        }
        if (Input.GetButtonDown("Fire3"))
        {
            anim.SetBool("Missle", true);

        }
        if (Input.GetButtonUp("Fire3"))
        {
            anim.SetBool("Missle", false);
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

        if (!currentState.IsName("jump"))
        {
            if (Input.GetButtonDown("Fire1"))
                anim.SetBool("SmallGun", true);
        }

        if (Input.GetButtonUp("Fire1"))
            anim.SetBool("SmallGun", false);

        anim.SetFloat("hValue", Mathf.Abs(hValue));
        anim.SetBool("isGrounded", groundCheck.IsGrounded);
    }
    public void ActivateJumpForceChange()
    {
        //start a coroutine that changes the jump force for x seconds
        if (jumpForceChange != null)
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce = 6; // Reset to default jump force
        }

        jumpForceChange = StartCoroutine(ChangeJumpForce()); // Change jump force to 12 for 3 seconds
    }
    private IEnumerator ChangeJumpForce()
    {
        jumpForce = 12; // Set new jump force
        Debug.Log($"Jump force change to {jumpForce} at {Time.time}");
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        jumpForce = 6; // Reset to default jump force
        Debug.Log($"Jump force change to {jumpForce} at {Time.time}");
        jumpForceChange = null; // Clear the coroutine reference
    }

    void SpriteFlip(float hValue)
    {
        //if (hValue > 0 && sr.flipX)
        //{
        //    sr.flipX = false;
        //}
        //else if (hValue < 0 && !sr.flipX)
        //{
        //    sr.flipX = true;
        //}
        if (hValue != 0) sr.flipX = (hValue < 0); // Simplified sprite flipping logic
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Squish") && rb.linearVelocityY < 0)
        {
            collision.GetComponentInParent<Enemy>().TakeDamage(0, DamageType.JumpedOn);
            rb.linearVelocity = Vector2.zero; // Stop the player from moving after squishing an enemy
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Add a small upward force to simulate bounce
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }
}

