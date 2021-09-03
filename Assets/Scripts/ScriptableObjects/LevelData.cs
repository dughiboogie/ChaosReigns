using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New level data", menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public float bestTime = 1000f;
    public int levelScore;

    public void UpdateBestTime(float newTime)
    {
        bestTime = newTime;
    }

    public void UpdateLevelScore(int newScore)
    {
        levelScore = newScore;
    }
}
