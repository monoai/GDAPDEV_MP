using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class SampleAdHandler : MonoBehaviour
{
    //Ads Manager reference
    public AdsManager adsManager;

    public int currency = 0;
    private Text currencyLabel;
    private void Start()
    {
        adsManager.OnAdDone += OnAdDone;
        currencyLabel = GetComponent<Text>();
    }

    public void OnAdDone(object sender, AdEventArg args)
    {
        if (args.PlacementID == AdsManager.SampleRewarded)
        {
            switch (args.AdResult)
            {
                case ShowResult.Failed: Debug.Log("Ad Fail"); break;
                case ShowResult.Skipped: Debug.Log("Ad Skipped"); break;
                case ShowResult.Finished:
                    Debug.Log("Ad Finished");
                    currency += 1;
                    break;
            }

        }
    }

    private void Update()
    {
        currencyLabel.text = $"Currency: {currency}";
    }
}
