using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private int jumpCount = 1; // Track the number of jumps
    [SerializeField] private int maxJumpCount = 2; // Maximum number of jumps allowed
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int maxLives = 5; // Maximum lives allowed
    private int _score = 0; // Player's score
    private int _lives = 3;  // Current lives
    public Shoot shoot; // Reference to the Shoot component for firing projectiles
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


    //public void ActivateJumpForceChange()
    //{
    //    // This method can be called to change the jump force dynamically
    //    jumpForce = 15f; // Example of changing the jump force
    //    yield return new WaitForSeconds(5f); // Wait for 5 seconds before changing back
    //    Debug.Log($"Jump force changed to: {jumpForce}");
    //}

    public int GetLives() => _lives; // Getter for lives
    public void SetLives(int newLives)
    {
        if (newLives < 0)
        {
            Debug.LogWarning("Lives cannot be set to a negative value.");
            return;
        }
        _lives = Mathf.Min(newLives, maxLives); // Ensure lives do not exceed maxLives
        Debug.Log($"Lives set to: {_lives}");
    }
    public int score
    {
        get => score;
        set
        {
            if (value < 0)
                _score = 0; // Prevent negative score

            else
                _score = value; // Set score to the new value
        }
    }



    private Vector2 groundCheckPos => new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);

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
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        SpriteFlip(hValue);

        rb.linearVelocityX = hValue * moveSpeed;
        groundCheck.CheckIsGrounded();
        //anim.SetFloat("Speed", Mathf.Abs(hValue));

        if ( Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Fire");
        }
        if (currentState.IsName("Fire"))
        {
            rb.linearVelocity = Vector2.zero;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("BigGun", true );
            
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
