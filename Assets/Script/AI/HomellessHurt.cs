using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomelessHurt : MonoBehaviour
{
    public float health = 3; // Ajout de la variable de santé du joueur
    private bool isHurt = false; // Ajout de la variable d'état de blessure
    private float hurtDuration = 1f; // Durée de l'état de blessure
    private float hurtTimer = 0f; // Timer de l'état de blessure
    private bool isDead = false; // Ajout de la variable d'état de mort
    public Collider2D attackTrigger;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return; // Ne rien faire si le Homeless est mort
        }

        if (health <= 0)
        {
            // Ajouter la logique de mort du Homeless ici
            Debug.Log("Player is dead");
            animator.SetBool("isDead", true);
            isDead = true;
            StartCoroutine(HandleDeath());
        }

        if (isHurt)
        {
            hurtTimer -= Time.deltaTime;
            animator.SetBool("isHurt", true);
            attackTrigger.enabled = false; // Désactiver le collider de l'attaque pendant l'état de blessure

            if (hurtTimer <= 0)
            {
                isHurt = false;
                animator.SetBool("isHurt", false);
            }
        }
        else {
            attackTrigger.enabled = true; // Activer le collider de l'attaque lorsque le Homeless n'est pas blessé
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isHurt && !isDead)
        {
            health -= damage;
            isHurt = true;
            hurtTimer = hurtDuration;

            if (health <= 0)
            {
                // logique de mort du Homeless
                Debug.Log("Player is dead");
                animator.SetBool("isDead", true);
                isDead = true;
                StartCoroutine(HandleDeath());
            }
        }
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(1f); // Attendre 1 secondes avant de désactiver le joueur
        gameObject.SetActive(false); // Désactiver le GameObject du joueur
    }
}
