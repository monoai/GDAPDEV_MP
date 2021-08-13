using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapObjectReceiver2 : MonoBehaviour, ITap
{
    public void OnTap()
    {
        Destroy(gameObject);
    }
}