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
}
