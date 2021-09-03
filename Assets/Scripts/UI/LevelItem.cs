using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;

public class LevelItem : MonoBehaviour
{
    public TextMeshProUGUI levelName;
    public TextMeshProUGUI levelTime;
    public List<GameObject> stars;

    public LevelData levelData;

    private void Awake()
    {
        levelName.text = levelData.levelName;
    }

    public void LevelSelected()
    {
        SceneManager.LoadScene(levelName.text);
    }

    private void Update()
    {
        if(levelData.bestTime == 1000f) {
            levelTime.text = "-";
        }
        else {
            int seconds = (int)levelData.bestTime;
            int minutes = seconds / 60;
            seconds = seconds % 60;

            levelTime.text = minutes.ToString().PadLeft(2, '0') + ':' + seconds.ToString().PadLeft(2, '0');
        }

        switch(levelData.levelScore) {
            case 1:
                stars[0].SetActive(true);
                stars[1].SetActive(false);
                stars[2].SetActive(false);
                break;
            case 2:
                stars[0].SetActive(true);
                stars[1].SetActive(true);
                stars[2].SetActive(false);
                break;
            case 3:
                stars[0].SetActive(true);
                stars[1].SetActive(true);
                stars[2].SetActive(true);
                break;
            default:
                stars[0].SetActive(false);
                stars[1].SetActive(false);
                stars[2].SetActive(false);
                break;
        }
    }
}
