using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Camera _camera;

    public void SetBackgroundColor(GameObject toggle)
    {
        Color color = toggle.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().color;

        if (toggle.GetComponent<Toggle>().isOn)
        {
            color.r = 0.7f;
            color.g = 1.0f;
            color.b = 0.4f;

            toggle.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().color = color;
        }
        else
        {
            color.r = 1.0f;
            color.g = 1.0f;
            color.b = 1.0f;

            toggle.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().color = color;
        }
    }

    public void MuteAudio(GameObject toggle) //AudioSource has mute feature, might change this entire function
    {
        Material material = toggle.GetComponent<Toggle>().targetGraphic.GetComponent<Image>().material;


        if (toggle.GetComponent<Toggle>().isOn)
        {
            material.SetFloat("_Threshold", 0.0f);
            _camera.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            material.SetFloat("_Threshold", 1.0f);
            _camera.GetComponent<AudioListener>().enabled = false;
        }
    }
}
