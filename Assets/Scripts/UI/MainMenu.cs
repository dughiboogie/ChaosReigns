using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPage;
    public GameObject levelSelectionPage;
    public GameObject controlsPage;
    public GameObject creditsPage;

    public GameObject startButton;
    public GameObject firstLevelButton;
    public GameObject controlsBackButton;
    public GameObject creditsBackButton;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    public void StartPressed()
    {
        mainPage.SetActive(false);
        levelSelectionPage.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstLevelButton);
    }

    public void ControlsPressed()
    {
        mainPage.SetActive(false);
        controlsPage.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsBackButton);
    }

    public void CreditsPressed()
    {
        mainPage.SetActive(false);
        creditsPage.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsBackButton);
    }

    public void QuitPressed()
    {
        Application.Quit();
    }

    public void BackPressed()
    {
        levelSelectionPage.SetActive(false);
        controlsPage.SetActive(false);
        creditsPage.SetActive(false);
        mainPage.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
    }
}
