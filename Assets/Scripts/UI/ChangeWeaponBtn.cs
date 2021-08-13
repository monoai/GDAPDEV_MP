using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponBtn : MonoBehaviour
{
    public Weapon weapon;

    float nextTimeToSearch = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        weapon = player.GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        //If another function uses this, it's time to move it to GM.
        if(weapon == null) {
            findPlayer();
            return;
        }
    }

    void findPlayer() {
        if(nextTimeToSearch <= Time.time) {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if(searchResult != null) {
                weapon = searchResult.GetComponent<Weapon>();
            }
            nextTimeToSearch = Time.time + 0.5f;
        }
    }

    public void changeWeapon() {
        switch(weapon.weaponType) {
            case Weapon.weaponTypeEnum.Red:
                weapon.currWeapon = null;
                weapon.weaponType = Weapon.weaponTypeEnum.Blue;
                Debug.Log("Weapon is now: " + weapon.weaponType);
                break;
            case Weapon.weaponTypeEnum.Blue:
                weapon.currWeapon = weapon.wavePrefab;
                weapon.weaponType = Weapon.weaponTypeEnum.Yellow;
                Debug.Log("Weapon is now: " + weapon.weaponType);
                break;
            default:
                weapon.currWeapon = weapon.bulletPrefab;
                weapon.weaponType = Weapon.weaponTypeEnum.Red;
                Debug.Log("Weapon is now: " + weapon.weaponType);
                break;
        }
    }
}
