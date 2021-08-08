using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public enum SwipeDirections
{
    RIGHT, LEFT, UP, DOWN
}

public class SwipeEventArgs : EventArgs
{
    public Vector2 SwipePos
    {
        get; private set;
    } = Vector2.zero;

    public SwipeDirections SwipeDirection
    {
        get; private set;
    } = SwipeDirections.RIGHT;

    public Vector2 SwipeVector
    {
        get; private set;
    } = Vector2.zero;

    public GameObject HitObject
    {
        get; private set;
    } = null;

    public SwipeEventArgs (Vector2 swipePos, SwipeDirections swipeDir, Vector2 swipeVector, GameObject hitObj)
    {
        SwipePos = swipePos;
        SwipeDirection = swipeDir;
        SwipeVector = swipeVector;
        HitObject = hitObj;
    }
}
