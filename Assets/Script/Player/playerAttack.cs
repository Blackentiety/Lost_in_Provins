using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{   
    public GameObject player;
    public float damage = 1;
    public Collider2D attackTrigger;

    private Animator animator;
    private bool isAttacking = false; // Ajout de la variable d'état d'attaque

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        attackTrigger.enabled = false; // Désactiver le collider de l'attaque par défaut
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack"))
        {
            isAttacking = true;
            attackTrigger.enabled = true; // Activer le collider de l'attaque pendant l'animation d'attaque
        }
        else
        {
            isAttacking = false;
            attackTrigger.enabled = false; // Désactiver le collider de l'attaque lorsque l'animation d'attaque est terminée
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && isAttacking)
        {   
            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackDirection * 10, ForceMode2D.Impulse);
            // Ajouter votre logique d'attaque ici
            other.gameObject.GetComponent<playerHurt>().TakeDamage(damage);
        }
    }
}
