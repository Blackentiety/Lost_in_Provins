using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour
{
    public string sceneName;
    private Collider2D col;
    public Sprite closedDoor;
    public Sprite openDoor;
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = closedDoor;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("supposed to open the door");
        gameObject.GetComponent<SpriteRenderer>().sprite = openDoor;
        Invoke("warp", 1);

    }

    void warp()
    {
        SceneManager.LoadScene(sceneName);
    }
}