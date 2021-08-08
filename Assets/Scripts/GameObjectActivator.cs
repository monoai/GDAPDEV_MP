using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectActivator : MonoBehaviour
{
    public GameObject objectToActivate;
    public GameObject objectToDeactivate;
    public GameObject landscape;
    public GameObject portrait;

    public void Activator()
    {
        objectToActivate.SetActive(true);
        objectToDeactivate.SetActive(false);
    }

    void Update()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;

        if (Screen.orientation == ScreenOrientation.Landscape ||
            Screen.orientation == ScreenOrientation.LandscapeLeft ||
            Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            landscape.SetActive(true);
            portrait.SetActive(false);
        }
        else if (Screen.orientation == ScreenOrientation.Portrait ||
                Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            landscape.SetActive(false);
            portrait.SetActive(true);
        }
        else
        {
            Debug.Log("Error: Went to else");
        }
    }
}
