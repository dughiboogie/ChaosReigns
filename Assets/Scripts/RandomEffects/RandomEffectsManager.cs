using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEffectsManager : MonoBehaviour
{
    public bool isActive;
    public List<GlobalEffect> globalEffects;

    public float newEffectTimeout = 5f;
    private float effectCounter;

    private int currentEffectIndex = 0;

    public HUD hud;

    private void Awake()
    {
        effectCounter = newEffectTimeout;
    }

    private void Update()
    {
        if(isActive) {
            if(effectCounter > 0) {
                effectCounter -= Time.deltaTime;
            }
            else {
                globalEffects[currentEffectIndex].Stop();

                currentEffectIndex = Random.Range(0, globalEffects.Count);

                bool tooSoon4DeathWall = false;

                if(globalEffects[currentEffectIndex].name == "the void is coming for you" && (int)hud.actualTime < 30) {
                    tooSoon4DeathWall = true;
                }

                if(!tooSoon4DeathWall) {
                    globalEffects[currentEffectIndex].Apply();

                    hud.DisplayChaosEffect(globalEffects[currentEffectIndex].name);

                    effectCounter = newEffectTimeout;
                }

                
            }
        }
    }

}
