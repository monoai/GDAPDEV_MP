using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public GameObject Lvl2;
    public GameObject Lvl3;
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.data.levelsUnlocked == 2)
        {
            Lvl2.SetActive(true);
        }
        if (DataManager.data.levelsUnlocked == 3)
        {
            Lvl2.SetActive(true);
            Lvl3.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
