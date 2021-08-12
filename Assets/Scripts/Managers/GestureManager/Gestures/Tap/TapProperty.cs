using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TapProperty
{
    [Tooltip("Time considered for a tap")]
    public float tapTime = 0.7f;
    [Tooltip("Max Distance considered for a tap")]
    public float tapMaxDistance = 0.1f;
}
