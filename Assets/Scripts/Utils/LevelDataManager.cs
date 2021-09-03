using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataManager : MonoBehaviour
{
    public static LevelDataManager instance;

    public List<LevelData> levelsData;

    private void Awake()
    {
        #region Singleton

        if(instance != null) {
            Debug.LogWarning("Multiple instances of LevelDataManager found!");
            return;
        }
        instance = this;

        #endregion

        DontDestroyOnLoad(gameObject);
    }

    public void OnLevelCompleted(string levelName, float currentTime, int collectibles)
    {
        foreach(LevelData levelData in levelsData) {
            if(levelData.levelName == levelName) {
                if(currentTime < levelData.bestTime) {
                    levelData.UpdateBestTime(currentTime);
                }

                if(collectibles > levelData.levelScore) {
                    levelData.UpdateLevelScore(collectibles);
                }
            }
        }
    }
}
