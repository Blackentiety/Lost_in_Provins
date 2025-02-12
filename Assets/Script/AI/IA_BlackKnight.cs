using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_BlackKnight : MonoBehaviour
{
    public float speed = 1f;
    public float attackCooldown = 1f;
    public float damage = 1f;
    public Collider2D attackTrigger;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isAttacking = false;
    private float attackCooldownTimer = 0f;
    private bool isIdle = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        InvokeRepeating("ChangeState", 2f, 2f); // Change state every 2 seconds
        animator.SetTrigger("walk");
    }

    void Update()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (!isAttacking && !isIdle)
        {
            rb.velocity = new Vector2(speed * (isFacingRight ? 1 : -1), rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && attackCooldownTimer <= 0)
        {
            isAttacking = true;
            rb.velocity = Vector2.zero;
            animator.SetTrigger("skill_1");
            attackCooldownTimer = attackCooldown;
            other.gameObject.GetComponent<playerHurt>().TakeDamage(damage);
            StartCoroutine(ResetAttack());
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // Durée de l'animation d'attaque
        isAttacking = false;
    }

    void ChangeState()
    {
        if (!isAttacking)
        {
            if (isIdle)
            {
                animator.SetTrigger("walk");
                isIdle = false;
                Flip();
            }
            else
            {
                int idleType = Random.Range(0, 3);
                if (idleType == 0)
                {
                    animator.SetTrigger("idle_1");
                }
                if (idleType == 1)
                {
                    animator.SetTrigger("skill_2");
                }
                else
                {
                    animator.SetTrigger("idle_2");
                }
                isIdle = true;
            }
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }
}
