using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    bool isGrounded = true;
    int jumps = 0;
    public int maxJumps = 2;
    public float JumpForce;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Nouvelle ligne pour le mouvement horizontal.

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumps < maxJumps))
        {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            jumps++;
        }

        // Nouvelles lignes pour le mouvement horizontal.
        Vector2 movement = new Vector2(moveHorizontal, rb.velocity.y);
        rb.velocity = movement;
    }

    public void JumpAnimationFinished()
    {
        animator.SetBool("IsJumping", false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumps = 0;
        }
    }
}
