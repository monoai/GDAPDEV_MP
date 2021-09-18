using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public GameObject Lvl2;
    public GameObject Lvl3;
    public GameObject Lvl4;
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.data.Lvl2unlock == true)
        {
            Lvl2.SetActive(true);
        }
        if (DataManager.data.Lvl3unlock == true)
        {
            Lvl3.SetActive(true);
        }
        if (DataManager.data.Lvl4unlock == true)
        {
            Lvl4.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
