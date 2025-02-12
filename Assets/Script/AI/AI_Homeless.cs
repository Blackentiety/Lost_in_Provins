using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Homeless : MonoBehaviour
{
    public float speed = 2f;
    public float timer = 2f;
    private Rigidbody2D rb;
    private Collider2D col;
    private int direction;
    private Animator anim;
    private SpriteRenderer sprite;
    private int RandomAnim;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = gameObject.GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Droite();
    }

    void Update()
    {
        rb.velocity = transform.right * speed * direction;
    }

    void Gauche()
    {
        anim.SetBool("IsDrinking", false);
        anim.SetBool("IsWalking", true);
        direction = -1;
        Invoke("Droite", 4.0f);
        Invoke("Stop", 2.0f);
        sprite.flipX = true;
    }
    void Droite()
    {
        anim.SetBool("IsDrinking", false);
        anim.SetBool("IsWalking", true);
        direction = 1;
        Invoke("Gauche", 4.0f);
        Invoke("Stop", 2.0f);
        sprite.flipX = false;
    }

    void Stop()
    {
        direction = 0;
        RandomAnim = Random.Range(1,3);
        Debug.Log(RandomAnim);
        
        if (RandomAnim == 1)
        {
            anim.SetBool("IsDrinking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }
}

