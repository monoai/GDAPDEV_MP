using UnityEngine.Audio;
using UnityEngine.UI;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private bool BGMflag = true;
    private bool SFXflag = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("BGM");
        BGMflag = true;
        SFXflag = true;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound file '" + name + "' not found!!");
            return;
        }
        s.source.Play();    
    }

    public void SetSliders(Slider slider)
    {
        if (slider.name == "BGM Slider") SetBGMVolume(slider);
        else if (slider.name == "SFX Slider") SetSFXVolume(slider);
        else 
        { 
            Debug.LogWarning("Slider '" + "BGM" + "' not found!!");
            return; 
        }
    }

    public void SetBGMVolume(Slider slider)
    {
        Sound s = Array.Find(sounds, sound => sound.name == "BGM");
        if (s == null)
        {
            Debug.LogWarning("Sound file '" + "BGM" + "' not found!!");
            return;
        }

        if (BGMflag)
        {
            slider.value = s.volume;
            BGMflag = false;
        }

        s.volume = slider.value;
        s.source.volume = s.volume;
    }

    public void SetSFXVolume(Slider slider)
    {
        for (int i = 1; i < sounds.Length; i++) // sounds[0] is the BGM Sound
        {
            Sound s = sounds[i];
            if (s == null)
            {
                Debug.LogWarning("Sound file '" + s.name + "' not found!!");
                return;
            }

            if (SFXflag)
            {
                slider.value = s.volume;
                SFXflag = false;
            }

            s.volume = slider.value;
            s.source.volume = s.volume;
        }
    }
}
