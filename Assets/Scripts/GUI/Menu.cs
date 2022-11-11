using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject creditsMenu;

    void Play()
    {
        SceneManager.LoadScene("MainScene");
    }

    void Settings()
    {
        settingsMenu.SetActive(true);
    }

    void Credits()
    {
        creditsMenu.SetActive(true);
    }

    void Exit()
    {
        Application.Quit();
    }
}
