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
    private Rigidbody2D rb; // Référence au Rigidbody2D du joueur
    private Animator animator;
    public GameObject PlayerMove; // Référence au script PlayerMove
    public GameOverManager gameOverManager; // Référence au GameOverManager

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
         // Disable player movement components
        var playerRigidbody = rb;
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.isKinematic = true;
        }

        var playerMovement = PlayerMove.GetComponent<playerMove>(); // Assuming you have a PlayerMovement script
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Wait for the death animation to finish (assuming it takes 3 seconds)
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);

        // Show game over panel
        gameOverManager.ShowGameOverPanel();

        // Pause the game
        Time.timeScale = 0f;
    }
}
