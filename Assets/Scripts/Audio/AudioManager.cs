using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        #region Singleton

        if(instance != null) {
            Debug.LogWarning("Multiple instances of AudioManager found!");
            return;
        }
        instance = this;

        #endregion

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        float introLenght = Play("Intro");

        StartCoroutine(PlayNextFile(introLenght, "Loop"));
    }

    public float Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s == null) {
            Debug.LogWarning("Sound " + name + " not found!");
            return 0;
        }

        s.source.Play();
        return s.clip.length;
    }

    private IEnumerator PlayNextFile(float currentAudioClipLength, string nextFileName)
    {
        yield return new WaitForSeconds(currentAudioClipLength - .05f);
        Play(nextFileName);
    }
}
