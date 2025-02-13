using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class cameraTrigger : MonoBehaviour


{
    public float cameraPosX;
    public float cameraPosY;
    public float playerMoveX;   
    public float playerMoveY;
    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            Camera.main.transform.position = new Vector3(cameraPosX, cameraPosY, -10);
            GameObject.FindWithTag("Player").transform.Translate(playerMoveX, playerMoveY, 0);
            Time.timeScale = 1;
        }

    }
}