using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioManager audioManager;

    [HideInInspector] public int movementControlType = 0;
    private int savedControlType;

    private float moveSpeed;

    private bool CTflag = true; // Control Type flag
    private bool BGMflag = true;
    private bool SFXflag = true;
    private bool MSflag = true; // MovementSpeed flag
    private bool Muteflag = true;
    private int MuteOn;
    private bool AdsFlag = true;
    private int AdsOn;

    [HideInInspector] public bool AdsIsEnabled;

    GameObject image;

    void Start()
    {
        GameObject OptionsMenu = GameObject.Find("Options Menu Canvas");
        CloseOptionsMenu(OptionsMenu);

        savedControlType = PlayerPrefs.GetInt("ControlType", 0);
        Debug.Log(savedControlType);

        GameObject ControlTypeA = GameObject.Find("ControlType A");
        GameObject ControlTypeB = GameObject.Find("ControlType B");
        GameObject BGMslider = GameObject.Find("BGM Slider");
        GameObject SFXslider = GameObject.Find("SFX Slider");
        GameObject MoveSpeed = GameObject.Find("MoveSpeed Slider");
        GameObject MuteToggle = GameObject.Find("Mute Toggle");
        GameObject AdsToggle = GameObject.Find("Ads Toggle");
        image = GameObject.Find("Red X");


        CTflag = true;
        BGMflag = true;
        SFXflag = true;
        MSflag = true;
        Muteflag = true;
        MuteOn = PlayerPrefs.GetInt("MuteOn", 0);
        AdsFlag = true;
        AdsOn = PlayerPrefs.GetInt("AdsOn", 0);

        SetControlType(ControlTypeA);
        SetControlType(ControlTypeB);
        SetBGMVolume(BGMslider.GetComponent<Slider>());
        SetSFXVolume(SFXslider.GetComponent<Slider>());
        SetMovementSpeed(MoveSpeed.GetComponent<Slider>());
        MuteAudio(MuteToggle);
        SetAds(AdsToggle);

        Debug.Log("Start End");
    }

    public void OpenOptionsMenu(GameObject OptionsMenu)
    {
        OptionsMenu.GetComponent<Canvas>().enabled = true;
    }

    public void CloseOptionsMenu(GameObject OptionsMenu)
    {
        OptionsMenu.GetComponent<Canvas>().enabled = false;
    }

    /*
    public void InitializeSavedSettings() // Set saved control type
    {
        GameObject ControlTypeA = GameObject.Find("ControlType A");
        GameObject ControlTypeB = GameObject.Find("ControlType B");
        GameObject BGMslider = GameObject.Find("BGM Slider");
        GameObject SFXslider = GameObject.Find("SFX Slider");
        GameObject MuteToggle = GameObject.Find("Mute Toggle");

        SetControlType(ControlTypeA);
        SetControlType(ControlTypeB);
        SetBGMVolume(BGMslider.GetComponent<Slider>());
        SetSFXVolume(SFXslider.GetComponent<Slider>());
        MuteAudio(MuteToggle);
    }
    */

    void Update()
    {
        GameObject canvas = GameObject.Find("Canvas");

        if (canvas.transform.Find("Debug Menu").gameObject.activeInHierarchy)
        {
            GameObject AdsButton = GameObject.Find("Ads Button");
            if (AdsButton == null) return;

            AdsButton.GetComponent<Button>().interactable = AdsIsEnabled;
        }
    }

    public void SetAds(GameObject toggle)
    {
        Color color = image.GetComponent<Image>().color;
        int save;

        if (AdsFlag)
        {
            if (AdsOn == 1)
            {
                color.a = 1.0f;
                image.GetComponent<Image>().color = color;
                toggle.GetComponent<Toggle>().isOn = false;
                AdsIsEnabled = false;
            }
            else
            {
                color.a = 0.0f;
                image.GetComponent<Image>().color = color;
                toggle.GetComponent<Toggle>().isOn = true;
                AdsIsEnabled = true;
            }

            AdsFlag = false;
        }
        else
        {
            if (toggle.GetComponent<Toggle>().isOn)
            {
                color.a = 0.0f;
                image.GetComponent<Image>().color = color;
                AdsIsEnabled = true;
                save = 0;
            }
            else
            {
                color.a = 1.0f;
                image.GetComponent<Image>().color = color;
                AdsIsEnabled = false;
                save = 1;
            }
            PlayerPrefs.SetInt("AdsOn", save);
        }
    }

    public void SetControlType(GameObject toggle)
    {
        Color color = toggle.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().color;

        if (CTflag)
        {
            if (savedControlType == 0 && toggle.name == "ControlType A")
            {
                color.r = 0.7f;
                color.g = 1.0f;
                color.b = 0.4f;
                toggle.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().color = color;
                toggle.GetComponent<Toggle>().isOn = true;
                CTflag = false;
            }
            else if (savedControlType == 1 && toggle.name == "ControlType B")
            {
                color.r = 0.7f;
                color.g = 1.0f;
                color.b = 0.4f;
                toggle.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().color = color;
                toggle.GetComponent<Toggle>().isOn = true;
                CTflag = false;
            }
            return;
        }
        else
        {
            if (toggle.GetComponent<Toggle>().isOn)
            {
                color.r = 0.7f;
                color.g = 1.0f;
                color.b = 0.4f;

                toggle.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().color = color;

                if (toggle.name == "ControlType A")
                {
                    movementControlType = 0;
                }
                else if (toggle.name == "ControlType B")
                {
                    movementControlType = 1;
                }
                PlayerPrefs.SetInt("ControlType", movementControlType); // Used in PlayerMovement
            }
            else
            {
                color.r = 1.0f;
                color.g = 1.0f;
                color.b = 1.0f;

                toggle.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().color = color;
            }
        }
    }

    public void SetBGMVolume(Slider slider)
    {
        Sound s = Array.Find(audioManager.sounds, sound => sound.name == "BGM");
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

    public void SetSFXVolume(Slider slider)
    {
        for (int i = 1; i < audioManager.sounds.Length; i++) // sounds[0] SHOULD BE the BGM Sound
        {
            Sound s = audioManager.sounds[i];
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

    public void SetMovementSpeed(Slider slider)
    {
        float MovementSpeed = PlayerPrefs.GetFloat("MovementSpeed", 1.0f);
        Debug.Log(MovementSpeed);

        if (MSflag)
        {
            slider.value = MovementSpeed;
            MSflag = false;
        }

        MovementSpeed = slider.value;
        moveSpeed = MovementSpeed;

        PlayerPrefs.SetFloat("MovementSpeed", MovementSpeed);
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void MuteAudio(GameObject toggle)
    {
        Material material = toggle.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().material;
        int save;

        if (Muteflag)
        {
            if (MuteOn == 1)
            {
                material.SetFloat("_Threshold", 1.0f);
                toggle.GetComponent<Toggle>().isOn = false;
                SetMute(true);
            }
            else
            {
                material.SetFloat("_Threshold", 0.0f);
                toggle.GetComponent<Toggle>().isOn = true;
                SetMute(false);
            }

            Muteflag = false;
        }
        else
        {
            if (toggle.GetComponent<Toggle>().isOn)
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

    private void SetMute(bool isMute)
    {
        for (int i = 0; i < audioManager.sounds.Length; i++) // sounds[0] SHOULD BE the BGM Sound
        {
            Sound s = audioManager.sounds[i];
            if (s == null)
            {
                Debug.LogWarning("Sound file '" + s.name + "' not found!!");
                return;
            }

            s.source.mute = isMute;
        }
    }
}
