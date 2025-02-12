using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomelessAttack : MonoBehaviour
{   
    public GameObject Homeless;
    public float damage = 1;
    public Collider2D attackTrigger;

    private Animator animator;
    private float attackCooldownTimer = 0f; // Ajout de la variable de timer de cooldown d'attaque
    private float attackCooldown = 0.5f; // Ajout de la variable de cooldown
    private bool isHurt = false; // Ajout de la variable d'état de blessure
    private bool isDead = false; // Ajout de la variable d'état de mort

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            isHurt = true;
        }
        else
        {
            isHurt = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }
        
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isHurt && !isDead && attackCooldownTimer <= 0)
        {   
            animator.SetBool("isAttacking", true);
            Debug.Log("Homeless is attacking");
            attackCooldownTimer = attackCooldown;
            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * 2, ForceMode2D.Impulse);
            // Ajouter votre logique d'attaque ici
            other.gameObject.GetComponent<playerHurt>().TakeDamage(damage);
            animator.SetBool("isAttacking", false);
        }
    }
}
