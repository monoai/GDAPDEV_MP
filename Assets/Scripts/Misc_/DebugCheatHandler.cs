using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCheatHandler : MonoBehaviour
{
    public void currencyCheat()
    {
        DataManager.data.Money += 3;
    }

    public void dmgCheat()
    {
        DataManager.data.maxDamage += 5;
    }

    public void levelCheat()
    {
        DataManager.data.Lvl2unlock = true;
        DataManager.data.Lvl3unlock = true;
        DataManager.data.Lvl4unlock = true;
    }
}
