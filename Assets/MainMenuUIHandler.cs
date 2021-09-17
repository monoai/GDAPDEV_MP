using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuUIHandler : MonoBehaviour
{
    //Why do all these besides the desire for elegance?
    //1. Find can't find disabled objects on its own.
    //2. We could find and disable through start but it makes editing on the editor more tedious.
    //3. This is one of the easiest solutions for the 2 problems above.
    //4. Time constraints.
    //5. Himemori Luna is life.
    [Header("Title Menu")]
    public GameObject TitleMenu;
    public GameObject TitleLogoPortrait;
    public GameObject TitleLogoLandscape;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;

        if (Screen.orientation == ScreenOrientation.Landscape ||
            Screen.orientation == ScreenOrientation.LandscapeLeft ||
            Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            if (TitleMenu.activeInHierarchy == true)
            {
                TitleLogoPortrait.SetActive(false);
                TitleLogoLandscape.SetActive(true);
            }
        }
        else if (Screen.orientation == ScreenOrientation.Portrait ||
                Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            if (TitleMenu.activeInHierarchy == true)
            {
                TitleLogoPortrait.SetActive(true);
                TitleLogoLandscape.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Error: Went to else");
        }
    }
}
