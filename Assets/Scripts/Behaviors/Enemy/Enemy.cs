using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage (int damage, Weapon.weaponTypeEnum weaponType) {
        //Testing weaponType enum
        //Note: WORKS but this code overwrites damage for now
        float multiplier = 1.0f;

        switch (weaponType) {
            case Weapon.weaponTypeEnum.Red: multiplier = 1.0f;
            Debug.Log("Got hit with Red"); break;
            case Weapon.weaponTypeEnum.Blue: multiplier = 0.8f;
            Debug.Log("Got hit with Blue"); break;
            case Weapon.weaponTypeEnum.Yellow: multiplier = 0.5f;
            Debug.Log("Got hit with Yellow"); break;
        }


        health -= (int)(damage * multiplier);

        if (health <= 0) {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
