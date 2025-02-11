using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class doorTrigger : MonoBehaviour
{
    public float cameraMoveX;
    public float cameraMoveY;
    public float playerMoveX;
    public float playerMoveY;
    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
            Time.timeScale = 0;
            Camera.main.transform.position = new Vector3(cameraMoveX, cameraMoveY, -10);
            GameObject.FindWithTag("Player").transform.Translate(playerMoveX, playerMoveY, 0);
            Time.timeScale = 1;
    }
}