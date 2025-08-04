using UnityEngine;

public class GroundCheck
{
    [SerializeField]private bool isGrounded;

    public bool IsGrounded => isGrounded;
    private LayerMask groundLayer;
    private Collider2D col;
    private Rigidbody2D rb;
    private float groundCheckRadius;

    private Vector2 groundCheckPos => new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);

    public GroundCheck(Collider2D collider, LayerMask LayerMask, float checkRadius)
    {
        col = collider;
        groundLayer = LayerMask;
        groundCheckRadius = checkRadius;
        rb = col.GetComponent<Rigidbody2D>();
    }
    public void CheckIsGrounded()
    {
        if (!isGrounded && rb.linearVelocityY < 0)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);
        }
        else if (isGrounded) isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);
    }
    public void UpdateGroundCheckRadius(float newRadius)
    {
        groundCheckRadius = newRadius;
        Debug.Log($"Ground check radius updated to: {groundCheckRadius}");
    }
}
