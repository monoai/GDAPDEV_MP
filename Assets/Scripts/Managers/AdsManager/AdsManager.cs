using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using System;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public EventHandler<AdEventArg> OnAdDone;

    public static AdsManager instance;

    public string GameID
    {
        get
        {
            //GameID for aNDROID
#if UNITY_ANDROID
            return "4287573";
            //GameID for IOS
#elif UNITY_IOS
            return "4287572";
#endif
        }
    }

    public const string SampleRewarded = "sampleRewarded";
    public const string SampleInterstitial = "sampleInterstitial";
    public const string SampleBanner = "sampleBanner";

    private bool flag = true;

    private void Awake()
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

        //when testing the game, set the param to true
        //This is to avoid being flagged / banned while testing
        Advertisement.Initialize(GameID, true);
        //when testing the game, set the param to true
        //Advertisement.Initialize(GameID, false);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (DataManager.data.isAdsEnabled)
            {
                if (flag)
                {
                    flag = false;
                    ShowBanner();
                }
            }
            else HideBanner();
        }
    }

    //call this function when the button is clicked; place this on a button event
    public void ShowInterstitialAd()
    {
        //Check if the ad is ready
        if (Advertisement.IsReady(SampleInterstitial))
        {
            //Show the Ad
            Advertisement.Show(SampleInterstitial);
        }
        else
        {
            //If the user has no internet or the ad doesn't exist
            Debug.Log("No Ads");
        }
    }

    //call this function when the button is cliked; place this on a button event
    public void ShowBanner()
    {
        //Start courotine here
        isBannerActive = true;
        StartCoroutine(ShowBannerRoutine());
    }
    private bool isBannerActive = true;
    //call this function when the button is cliked; place this on a button event; close button
    public void HideBanner()
    {
        //Check if the banner ad is showing
        if (Advertisement.Banner.isLoaded)
        {
            Advertisement.Banner.Hide();
            isBannerActive = false;
        }
    }

    public bool isBannerAvailable()
    {
        if (isBannerActive)
        {
            return true;
        }
        return false;
    }

    IEnumerator ShowBannerRoutine()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_LEFT);
        //Show the banner ad
        Advertisement.Banner.Show(SampleBanner);
    }

    public void ShowRewardedAd()
    {
        //Check if the ad is ready
        if (Advertisement.IsReady(SampleRewarded))
        {
            //Show the Ad
            Advertisement.Show(SampleRewarded);
        }
        else
        {
            //If the user has no internet or the ad doesn't exist
            //Debug.Log("No Ads: " + SampleInterstitial);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Add this behavior as a listener to Ad events
        Advertisement.AddListener(this);
        flag = true;
    }

    //Called when Ad is ready to play
    public void OnUnityAdsReady(string placementId)
    {
        //Debug.Log($"Done loading {placementId}");
    }
    //Called when an Ad had an error
    public void OnUnityAdsDidError(string message)
    {
        //Debug.Log($"Ads error {message}");
    }
    //Called when an Ad gets played
    public void OnUnityAdsDidStart(string placementId)
    {
        //Debug.Log($"Ads start {placementId}");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (OnAdDone != null)
        {
            AdEventArg args = new AdEventArg(placementId, showResult);
            OnAdDone(this, args);
        }
    }

}
