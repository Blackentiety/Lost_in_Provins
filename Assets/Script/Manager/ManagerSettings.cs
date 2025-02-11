using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSettings : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;

    public void returnToMainMenu()
    {
        Debug.Log("Returning to Main Menu");
        SettingsMenu.SetActive(false);
        MainMenu.SetActive(true);

    }
}
