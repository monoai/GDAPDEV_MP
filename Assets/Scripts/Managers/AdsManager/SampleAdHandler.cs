using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class SampleAdHandler : MonoBehaviour
{
    //Ads Manager reference
    public AdsManager adsManager;

    public int currency = 0; //TESTER
    private Text currencyLabel; //TESTER
    private void Start()
    {
        adsManager.OnAdDone += OnAdDone;
        currencyLabel = GetComponent<Text>(); //TESTER
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
                    DataManager.data.Money += 3;
                    currency += 1; //TESTER
                    break;
            }

        }
    }

    private void Update()
    {
        currencyLabel.text = $"Currency: {DataManager.data.Money}"; //TESTER
    }
}
