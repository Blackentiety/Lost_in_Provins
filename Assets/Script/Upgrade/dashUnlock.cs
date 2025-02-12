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
        gameObject.SetActive(false);
        player.hasDash = true;
        collect.Play();
    }
}