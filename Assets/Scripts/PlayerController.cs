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

    private MobileInput mobileInput;
    private ComputerTerminal currentTerminal; // ✅ Track the nearby terminal

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mobileInput = FindFirstObjectByType<MobileInput>();
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        anim.SetBool("isGrounded", isGrounded);

        float move = 0;

        // Desktop input
        move = Input.GetAxisRaw("Horizontal");

        // Mobile input
        if (mobileInput != null)
        {
            if (mobileInput.leftPressed) move = -1;
            else if (mobileInput.rightPressed) move = 1;
        }

        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // Flip character
        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move), 1, 1);

        anim.SetBool("isWalking", move != 0);

        // Jump
        if ((Input.GetKeyDown(KeyCode.Space) || (mobileInput != null && mobileInput.jumpPressed)) && isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        // ✅ Interact (keyboard or mobile)
        if (Input.GetKeyDown(KeyCode.E) || (mobileInput != null && mobileInput.interactPressed))
        {
            Debug.Log("🟢 Interact pressed");
            if (currentTerminal != null)
            {
                currentTerminal.ToggleComputer();
            }
            else
            {
                Debug.Log("⚠️ No nearby terminal to interact with.");
            }
        }
    }

    // ✅ Detect nearby terminal
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Terminal"))
        {
            currentTerminal = other.GetComponent<ComputerTerminal>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Terminal") && currentTerminal == other.GetComponent<ComputerTerminal>())
        {
            currentTerminal = null;
        }
    }
}
