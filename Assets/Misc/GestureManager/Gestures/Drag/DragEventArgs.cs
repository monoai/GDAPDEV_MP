using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class DragEventArgs : EventArgs
{
    public Touch TargetFinger
    {
        get; private set;
    }

    public GameObject HitObject
    {
        get; private set;
    }

    public DragEventArgs(Touch targetFinger, GameObject hitObj)
    {
        TargetFinger = targetFinger;
        HitObject = hitObj;
    }
}
