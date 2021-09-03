using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject resumeButton;

    public GameObject HUD;

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if(context.performed) {

            Debug.Log("Pause pressed");

            if(GameManager.instance.gameIsPaused) {
                Resume();

                Debug.Log("Unpause game");
            }
            else {

                Debug.Log("Pause game");
                Pause();
            }
        }
    }

    public void Back(InputAction.CallbackContext context)
    {
        Resume();
    }


    private void Pause()
    {
        HUD.SetActive(false);

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameManager.instance.gameIsPaused = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton);

        /*
         * TODO Write on Unity forum for errors on playerInput.SwitchCurrentActionMap() 
         * Interaction index out of range, 
         * UnityEngine.InputSystem.PlayerInput:SwitchCurrentActionMap(String)
         */
        GameManager.instance.playerInput.SwitchCurrentActionMap("Menu");
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.instance.gameIsPaused = false;

        HUD.SetActive(true);

        GameManager.instance.playerInput.SwitchCurrentActionMap("Gameplay");
    }

    public void LoadMenu()
    {
        GameManager.instance.LoadLevel("MainMenu");
    }
    
    public void QuitGame()
    {
        /*
         * TODO Works only in Unity Editor, in official release version call
         * Application.Quit();
         */
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
