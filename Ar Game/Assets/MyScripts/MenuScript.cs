using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    public GameObject PauseButton;
    public GameObject PausePanel;

    private void Start()
    {
        PauseButton.SetActive(false);
        PausePanel.SetActive(false);

    }


    public void OpenPausePanel()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public GameObject WelcomePanel;
    public GameObject welcomepanelOn;

    public void Gotit()
    {
        WelcomePanel.SetActive(false);
        welcomepanelOn.SetActive(true);
    }

    public void welcomeOn()
    {
        WelcomePanel.SetActive(true);
        welcomepanelOn.SetActive(false);
    }
}
