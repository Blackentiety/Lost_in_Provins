using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Elite : MonoBehaviour
{
    public float speed = 2f;
    public float timer = 2f;
    private Rigidbody2D rb;
    private Collider2D col;
    private int direction;
    private Animator anim;
    private int RandomAnim;
    private bool isHurt = false; // Ajout de la variable d'état de blessure
    private bool isDead = false; // Ajout de la variable d'état de mort
    private bool isAttacking = false; // Ajout de la variable d'état d'attaque
    public Collider2D attackTrigger;
    private float attackCooldownTimer = 0f; // Ajout de la variable de timer de cooldown d'attaque
    private float attackCooldown = 0.5f; // Ajout de la variable de cooldown
    private bool isFacingRight = true;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = gameObject.GetComponent<Animator>();
        Droite();
    }

    void Update()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        if (!isHurt && !isDead && !isAttacking)
        {
            rb.velocity = transform.right * speed * direction;
        }
    }
    
    void Gauche()
    {
        anim.SetBool("isRunning", true);
        direction = -1;
        Invoke("Droite", 4.0f);
        Invoke("Stop", 2.0f);
        if (isFacingRight)
        {
            Flip();
            isFacingRight = false;
        }
    }
    void Droite()
    {
        anim.SetBool("isRunning", true);
        direction = 1;
        Invoke("Gauche", 4.0f);
        Invoke("Stop", 2.0f);
        if (!isFacingRight)
        {
            Flip();
            isFacingRight = true;
        }
        
    }

    void Stop()
    {
        direction = 0;
        anim.SetBool("isRunning", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isHurt && !isDead && attackCooldownTimer <= 0)
        {
            isAttacking = true;
            StartCoroutine(Attack());
            Debug.Log("Homeless is attacking");
            attackCooldownTimer = attackCooldown;
            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * 2, ForceMode2D.Impulse);
            // Ajouter votre logique d'attaque ici
            other.gameObject.GetComponent<playerHurt>().TakeDamage(1);
        }
    }
    private IEnumerator Attack()
    {
        Debug.Log("Homeless is attacking animation");
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isAttacking", false);
        isAttacking = false;
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
}

