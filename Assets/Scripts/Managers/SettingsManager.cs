using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Managers to Handle")]
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

    public GameObject ControlTypeA;
    public GameObject ControlTypeB;
    public GameObject BGMslider;
    public GameObject SFXslider;
    public GameObject MoveSpeed;
    public GameObject MuteToggle;
    public GameObject AdsToggle;

    public GameObject image;

    //It's okay to store references to objects that we prefer to hide on the editor.
    [Header("UI to Handle")]
    public GameObject OptionsMenu;


    void Start()
    {
        savedControlType = PlayerPrefs.GetInt("ControlType", 0);

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
    }

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

        int CheckNet = PlayerPrefs.GetInt("InternetCheck", 0);

        if (AdsFlag)
        {
            if (AdsOn == 0 && CheckNet == 1)
            {
                color.a = 0.0f;
                image.GetComponent<Image>().color = color;
                toggle.GetComponent<Toggle>().isOn = true;
                AdsIsEnabled = true;
            }
            else
            {
                color.a = 1.0f;
                image.GetComponent<Image>().color = color;
                toggle.GetComponent<Toggle>().isOn = false;
                AdsIsEnabled = false;
            }

            AdsFlag = false;
        }
        else
        {
            if (toggle.GetComponent<Toggle>().isOn && CheckNet == 1)
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

        if (AdsIsEnabled) PlayerPrefs.SetInt("AdsIsEnabled", 1);
        else PlayerPrefs.SetInt("AdsIsEnabled", 0);
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
