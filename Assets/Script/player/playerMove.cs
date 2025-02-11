using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour {

    public ParticleSystem dust;
    public ParticleSystem dashParticle;

    public float speed = 5f;
    public float jumpForce = 10f;
    public float dashForce = 10f;
    public float dashDuration = 0.5f;
    public int extraJumps = 1;
    
    public float attackCooldown = 0.5f; // Ajout de la variable de cooldown d'attaque

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

    private bool isAttacking = false; // Ajout de la variable d'état d'attaque
    private float attackCooldownTimer = 0f; // Ajout de la variable de timer de cooldown d'attaque

    private int dashCount = 0; // Ajout de la variable pour suivre le nombre de dashs effectués
    private int maxDashCount = 2; // Nombre maximum de dashs consécutifs autorisés

    public bool isDoubleJumpUnlocked = true; // Booléen public pour indiquer si le double saut est débloqué$
    public bool hasDash = false;

    private Vector2 defaultPhysics;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponentInChildren<Animator>();
        extraJumpsValue = extraJumps; // Initialiser extraJumpsValue
        defaultPhysics = Physics2D.gravity;
    }

    void Update()
    {
        // Gestion du cooldown d'attaque
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        // Déclenchement de l'attaque
        if (Input.GetButtonDown("Fire1") && attackCooldownTimer <= 0)
        {
            Attack();
        }

        // Réinitialisation de l'animation d'attaque après le cooldown
        if (attackCooldownTimer <= 0 && isAttacking)
        {
            isAttacking = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isJumpAttacking", false);
            if (!isGrounded)
            {
                animator.SetBool("isJumping", true);
            }
        }

        // Mouvement horizontal
        float moveH = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.IsTouchingLayers(groundCol, groundLayer);
        rb.velocity = new Vector2(moveH * speed, rb.velocity.y);

        if(isDashing)
        {
            animator.SetBool("isJumping", false);
        }

        if (isAttacking)
        {
            animator.SetBool("isJumping", false);
        }
        // Gestion du coyote time
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            animator.SetBool("isJumping", false);
            extraJumpsValue = extraJumps; // Réinitialiser extraJumpsValue lorsque le joueur touche le sol
            dashCount = 0; // Réinitialiser le compteur de dashs lorsque le joueur touche le sol
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Gestion du jump buffer
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Conditions de saut
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isDashing && (extraJumpsValue > 0 || (isDoubleJumpUnlocked && extraJumpsValue > 0)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("isJumping", true);
            createDust();

            jumpBufferCounter = 0f;
            extraJumpsValue--; // Décrémenter extraJumpsValue à chaque saut
        }

        // Permet un saut plus long en maintenant la touche de saut
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            animator.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }

        // Dash lorsque Left Shift est pressé
        if (Input.GetKeyDown(KeyCode.LeftShift) && hasDash && !isDashing && dashCount < maxDashCount)
        {
            isDashing = true;
            dashParticle.Play();
            dashTime = dashDuration;
            dashCount++; // Incrémenter le compteur de dashs
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
                rb.velocity = new Vector2(dashDirection * dashForce, 0);
                dashTime -= Time.deltaTime;
            }
            else
            {
                isDashing = false;
                animator.SetBool("isDashingGround", false);
                animator.SetBool("isDashingAir", false);
            }
        }

        // Retourner le joueur si il se déplace dans la direction opposée
        if (moveH > 0 && !facingRight && !isDashing && !isAttacking)
        {
            Flip();
            if (coyoteTimeCounter > 0f)
            {
                createDust();
            }
        }
        else if (moveH < 0 && facingRight && !isDashing && !isAttacking)
        {
            Flip();
            if (coyoteTimeCounter > 0f)
            {
                createDust();
            }
        }

        if (moveH == 0 || isDashing || isAttacking || !isGrounded) {
            animator.SetBool("isWalking", false);
        }else{
            animator.SetBool("isWalking", true);
        }

        // Assurez-vous que l'animation de dash s'arrête lorsque le dash se termine
        if (!isDashing)
        {
            animator.SetBool("isDashingGround", false);
            animator.SetBool("isDashingAir", false);
        }
    }

    // Fonction pour retourner le joueur
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

    void Attack()
    {
        if (isAttacking) return; // Empêche l'exécution multiple de l'attaque

        isAttacking = true;
        attackCooldownTimer = attackCooldown;
        if (isGrounded)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isJumpAttacking", true);
        }
    }
}