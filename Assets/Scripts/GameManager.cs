using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform player;
    public HUD hud;

    public PlayerInput playerInput;
    public InputActionAsset actions;

    public InputAction jumpAction;

    public bool gameIsPaused = false;

    public PlayerPowers playerPowers;

    public int currentLevel;
    public string nextLevel;
    public List<LevelData> levelsData;

    private void Awake()
    {
        #region Singleton

        if(instance != null) {
            Debug.LogWarning("Multiple instances of GameManager found!");
            return;
        }
        instance = this;

        #endregion

        currentLevel = SceneManager.GetActiveScene().buildIndex;

        playerInput = player.GetComponent<PlayerInput>();
        jumpAction = playerInput.actions.FindAction("Jump");
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        hud.ResetHUDVariables();
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
        hud.ResetHUDVariables();
    }

    public void ReloadLevel()
    {
        LoadLevel(currentLevel);
    }

    public void LoadNextLevel()
    {
        LoadLevel(nextLevel);
    }

    public void OnLevelComplete()
    {
        foreach(LevelData levelData in levelsData) {
            if(levelData.levelName == SceneManager.GetActiveScene().name) {
                if(hud.actualTime < levelData.bestTime) {
                    levelData.UpdateBestTime(hud.actualTime);
                }

                if(hud.collectiblesCount > levelData.levelScore) {
                    levelData.UpdateLevelScore(hud.collectiblesCount);
                }
            }
        }

        LoadNextLevel();
    }
}
