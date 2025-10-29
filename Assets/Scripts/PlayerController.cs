using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // --- Ground Check FIRST ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        anim.SetBool("isGrounded", isGrounded);

        // --- Horizontal Movement ---
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // Flip character
        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);

        anim.SetBool("isWalking", move != 0);

        // --- Jump ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        // --- Pass vertical speed to Animator ---
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }
}
