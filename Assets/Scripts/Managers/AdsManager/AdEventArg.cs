using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdEventArg : MonoBehaviour
{
    public string PlacementID
    {
        get; private set;
    }

    public UnityEngine.Advertisements.ShowResult AdResult
    {
        get; private set;
    }

    public AdEventArg(string id,
        UnityEngine.Advertisements.ShowResult result)
    {
        PlacementID = id;
        AdResult = result;
    }
}
}
