using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GoToSettings()
    {
        
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        // GameObject.Find("SettingsMenu").SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
