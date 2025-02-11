using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHurt : MonoBehaviour
{
    public float health = 3; // Ajout de la variable de santé du joueur
    private bool isHurt = false; // Ajout de la variable d'état de blessure
    private float hurtDuration = 1f; // Durée de l'état de blessure
    private float hurtTimer = 0f; // Timer de l'état de blessure
    private bool isDead = false; // Ajout de la variable d'état de mort

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
            return; // Ne rien faire si le joueur est mort
        }

        if (health <= 0)
        {
            // Ajouter la logique de mort du joueur ici
            Debug.Log("Player is dead");
            animator.SetBool("isDead", true);
            isDead = true;
            StartCoroutine(HandleDeath());
        }

        if (isHurt)
        {
            hurtTimer -= Time.deltaTime;
            animator.SetBool("isHurt", true);

            if (hurtTimer <= 0)
            {
                isHurt = false;
                animator.SetBool("isHurt", false);
            }
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
                // Ajouter la logique de mort du joueur ici
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
