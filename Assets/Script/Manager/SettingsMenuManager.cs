using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    public void GoBack()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
