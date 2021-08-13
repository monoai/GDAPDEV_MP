using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    public void lifeUpg() {
        if(DataManager.data.Money >= 1) {
            DataManager.data.Money -= 1;
            DataManager.data.maxLives += 1;
        } else {
            Debug.Log("Not enough money");
        }
    }

    public void dmgUpg() {
        if(DataManager.data.Money >= 1) {
            DataManager.data.Money -= 1;
            DataManager.data.maxDamage += 5;
        } else {
            Debug.Log("Not enough money");
        }
    }

    public void fireRateUpg() {
        if(DataManager.data.Money >= 1) {
            DataManager.data.Money -= 1;
            DataManager.data.maxFireRate -= 0.05f;
        } else {
            Debug.Log("Not enough money");
        }
    }
}
