using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI levelName;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI collectiblesNumber;
    public TextMeshProUGUI chaosEffectInfo;
    public TextMeshProUGUI powerUpInfo;
    private float promptInfoTimeout = 3f;
    private float chaosEffectInfoTimer = 0f;
    private float powerUpInfoTimer = 0f;

    public float actualTime = 0f;
    public int collectiblesCount = 0;

    private void Awake()
    {
        ResetHUDVariables();
    }

    private void Update()
    {
        actualTime += Time.deltaTime;

        int seconds = (int)actualTime;
        int minutes = seconds / 60;
        seconds = seconds % 60;

        timer.text = minutes.ToString().PadLeft(2, '0') + ':' + seconds.ToString().PadLeft(2, '0');

        if(chaosEffectInfoTimer > 0f) {
            chaosEffectInfoTimer -= Time.deltaTime;
        }
        else if(chaosEffectInfo.gameObject.activeInHierarchy) {
            chaosEffectInfo.gameObject.SetActive(false);
        }

        if(powerUpInfoTimer > 0f) {
            powerUpInfoTimer -= Time.deltaTime;
        }
        else if(powerUpInfo.gameObject.activeInHierarchy) {
            powerUpInfo.gameObject.SetActive(false);
        }
    }

    public void ResetHUDVariables()
    {
        levelName.text = SceneManager.GetActiveScene().name;
        timer.text = "00:00";
        collectiblesNumber.text = "0";
    }

    public void AddCollectible()
    {
        collectiblesCount++;
        collectiblesNumber.text = collectiblesCount.ToString();
    }

    public void DisplayChaosEffect(string effectName)
    {
        chaosEffectInfo.gameObject.SetActive(true);
        chaosEffectInfo.text = effectName;

        chaosEffectInfoTimer = promptInfoTimeout;
    }

    public void DisplayPowerupInfo(Power power)
    {
        powerUpInfo.gameObject.SetActive(true);
        powerUpInfo.text = "now you can " + power.name + '!';

        powerUpInfoTimer = promptInfoTimeout;
    }

}
