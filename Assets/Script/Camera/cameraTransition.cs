using System.Collections;
using System.Collections.Generic;
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
        Camera.main.transform.Translate(cameraMoveX, cameraMoveY, 0);
        GameObject.FindWithTag("Player").transform.Translate(playerMoveX, playerMoveY, 0);
    }
}