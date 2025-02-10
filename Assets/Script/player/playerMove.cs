using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour {

    public ParticleSystem dust;

    public float speed = 5f;
    public float jumpForce = 10f;
    public float dashForce = 10f;
    public float dashDuration = 0.5f;
    public int extraJumps = 1;
    

    public Transform groundCheck;
    public LayerMask groundLayer;

    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D col;
    public Collider2D groundCol;

    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    private int extraJumpsValue;

    private bool isGrounded;
    private bool facingRight = true;

    private float dashTime;
    private bool isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        //horizontal movement
        float moveH = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.IsTouchingLayers(groundCol, groundLayer);
        rb.velocity = new Vector2(moveH * speed, rb.velocity.y);

        if(isDashing)
        {
            animator.SetBool("isJumping", false);
        }
        //handle coyote time
        if (isGrounded || extraJumpsValue < extraJumps)
        {
            coyoteTimeCounter = coyoteTime;
            animator.SetBool("isJumping", false);
            extraJumpsValue = 0;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //handle jump buffer
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        //replaced isGrounded and input jump conditions by buffer and coyote time since they are handled from those conditions
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isDashing && extraJumpsValue < extraJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isJumping", true);
            createDust();

            jumpBufferCounter = 0f;
            extraJumpsValue++;
        }

        //allows for longer jump while holding down the jump key
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        // Dash when Left Shift is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            isDashing = true;
            dashTime = dashDuration;
            if (isGrounded)
            {
                animator.SetBool("isDashingGround", true);
            }
            else
            {
                animator.SetBool("isDashingAir", true);
            }
        }

        if (isDashing)
        {
            if (dashTime > 0)
            {
                float dashDirection = facingRight ? 1 : -1;
                rb.velocity = new Vector2(dashDirection * dashForce, rb.velocity.y);
                dashTime -= Time.deltaTime;
            }
            else
            {
                isDashing = false;
                animator.SetBool("isDashingGround", false);
                animator.SetBool("isDashingAir", false);
            }
        }

        // Flip the player if the player is moving in the opposite direction
        if (moveH > 0 && !facingRight && !isDashing)
        {
            Flip();
            if (coyoteTimeCounter > 0f)
            {
                createDust();
            }
        }
        else if (moveH < 0 && facingRight && !isDashing)
        {
            Flip();
            if (coyoteTimeCounter > 0f)
            {
                createDust();
            }
        }

        if (moveH == 0 || isDashing) {
            animator.SetBool("isWalking", false);
        }else{
            animator.SetBool("isWalking", true);
        }

        // Ensure dash animation stops when dashing ends
        if (!isDashing)
        {
            animator.SetBool("isDashingGround", false);
            animator.SetBool("isDashingAir", false);
        }
    }

    // Function to flip the player
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void createDust()
    {
        dust.Play();
    }
}