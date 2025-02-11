using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashUnlock : MonoBehaviour
{
    public playerMove player;
    private Collider2D col;
    public ParticleSystem collect;
    public GameObject glint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        Invoke("kill", 1.0f);
        player.hasDash = true;
        glint.SetActive(false);
        collect.Play();
    }

    void kill()
    {
        gameObject.SetActive(false);
    }
}