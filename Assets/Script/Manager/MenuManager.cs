using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void settings(){
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
}
