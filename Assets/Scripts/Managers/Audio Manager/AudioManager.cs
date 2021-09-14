using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    /*
    private bool BGMflag = true;
    private bool SFXflag = true;
    private bool first = true;
    private bool MuteOn;

    private GameObject BGMslider;
    private GameObject SFXslider;
    private GameObject MuteToggle;
    */

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
        /*
        BGMflag = true;
        SFXflag = true;
        first = true;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            BGMslider = GameObject.Find("BGM Slider");
            SFXslider = GameObject.Find("SFX Slider");
            MuteToggle = GameObject.Find("Mute Toggle");

            SetBGMVolume(BGMslider.GetComponent<Slider>());
            SetSFXVolume(SFXslider.GetComponent<Slider>());
            SetBGMVolume(BGMslider.GetComponent<Slider>());

            PlayerPrefs.GetInt("MuteOn", 0);
        }
        */
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
   

    /*
    public void SetUpAudio(Slider slider) //Used for the Settings Menu
    {
        if (slider.name == "BGM Slider") SetBGMVolume(slider);
        else if (slider.name == "SFX Slider") SetSFXVolume(slider);
        else 
        { 
            Debug.LogWarning("Slider '" + "BGM" + "' not found!!");
            return; 
        }
    }
    */

    /*
    public void SetBGMVolume(Slider slider) //Used for the Settings Menu
    {
        Sound s = Array.Find(sounds, sound => sound.name == "BGM");
        if (s == null)
        {
            Debug.LogWarning("Sound file '" + "BGM" + "' not found!!");
            return;
        }

        s.volume = PlayerPrefs.GetFloat("BGM", 0.5f);

        if (BGMflag)
        {
            slider.value = s.volume;
            BGMflag = false;
        }

        s.volume = slider.value;
        s.source.volume = s.volume;

        PlayerPrefs.SetFloat("BGM", slider.value);
    }

    public void SetSFXVolume(Slider slider) //Used for the Settings Menu
    {
        for (int i = 1; i < sounds.Length; i++) // sounds[0] is the BGM Sound
        {
            Sound s = sounds[i];
            if (s == null)
            {
                Debug.LogWarning("Sound file '" + s.name + "' not found!!");
                return;
            }

            s.volume = PlayerPrefs.GetFloat("SFX", 0.5f);

            if (SFXflag)
            {
                slider.value = s.volume;
                SFXflag = false;
            }

            s.volume = slider.value;
            s.source.volume = s.volume;
        }
        PlayerPrefs.SetFloat("SFX", slider.value);
    }

    public void MuteAudio() //Used for the Settings Menu
    {
        Material material = MuteToggle.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().material;
        int save;

        if (first)
        {
            if (MuteOn)
            {
                material.SetFloat("_Threshold", 1.0f);
                SetMute(true);
            }
            else
            {
                material.SetFloat("_Threshold", 0.0f);
                SetMute(false);
            }

            first = false;
        }
        else
        {
            if (MuteToggle.GetComponent<Toggle>().isOn)
            {
                material.SetFloat("_Threshold", 0.0f);
                SetMute(false);
                save = 0;
            }
            else
            {
                material.SetFloat("_Threshold", 1.0f);
                SetMute(true);
                save = 1;
            }
            PlayerPrefs.SetInt("MuteOn", save);
        }
    }

    private void SetMute(bool isMute) //Used for the Settings Menu
    {
        for (int i = 0; i < sounds.Length; i++) // sounds[0] is the BGM Sound
        {
            Sound s = sounds[i];
            if (s == null)
            {
                Debug.LogWarning("Sound file '" + s.name + "' not found!!");
                return;
            }

            s.source.mute = isMute;
        }
    }
    */
}
