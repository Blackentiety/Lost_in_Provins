using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKnightHurt : MonoBehaviour
{
    public GameObject enemy;
    public float health = 5; // Ajout de la variable de santé du joueur
    private bool isHurt = false; // Ajout de la variable d'état de blessure
    private float hurtDuration = 1f; // Durée de l'état de blessure
    private float hurtTimer = 0f; // Timer de l'état de blessure
    private bool isDead = false; // Ajout de la variable d'état de mort
    public Collider2D attackTrigger;
    public Collider2D bodyCollider;
    private Animator enemyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHurt)
        {
            hurtTimer -= Time.deltaTime;
            attackTrigger.enabled = false; // Désactiver le collider de l'attaque pendant l'état de blessure

            if (hurtTimer <= 0)
            {
                isHurt = false;
            }
        }
        else {
            attackTrigger.enabled = true; // Activer le collider de l'attaque lorsque le Homeless n'est pas blessé
        }
        if (health <= 0 && !isDead)
        {
            isDead = true;
            attackTrigger.enabled = false; // Désactiver le collider de l'attaque lorsque le Homeless est mort
        }
        if (isDead)
        {
            StartCoroutine(Die());
        }
    }


	public void AttackStart () {
		Debug.Log ("Attack Start");

		//Just for demonstration, you can replace it with your own code logic.
		if (enemy && health > 0) {
			if (health%2 == 1) {
				enemyAnimator.SetTrigger ("hit_1");
			} else if (health%2 == 0) {
				enemyAnimator.SetTrigger ("hit_2");
			} 
			} 
        else {
            enemyAnimator.SetTrigger ("death");
            
        }
	}
    public void TakeDamage(float damage)
    {
        if (!isHurt && !isDead)
        {
            health -= damage;
            isHurt = true;
            hurtTimer = hurtDuration;
            AttackStart();
        }
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(0.8f);
        enemy.SetActive(false);
    }

}
