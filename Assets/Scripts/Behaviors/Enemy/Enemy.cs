using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enemyTypeEnum enemyType;
    public enum enemyTypeEnum { RedEnemy, BlueEnemy, YellowEnemy, RedBoss, BlueBoss, YellowBoss };

    public int health = 100;
    public int scoreWorth;

    [Range(0.4f, 2.0f)]
    public float RedWeaponMultiplier = 1.0f;
    [Range(0.4f, 2.0f)]
    public float BlueWeaponMultiplier = 0.8f;
    [Range(0.4f, 2.0f)]
    public float YellowWeaponMultiplier = 0.5f;

    public void TakeDamage(int damage, Weapon.weaponTypeEnum weaponType)
    {
        //Testing weaponType enum
        //Note: WORKS but this code overwrites damage for now
        float multiplier = 1.0f;

        switch (weaponType)
        {
            case Weapon.weaponTypeEnum.Red:
                multiplier = RedWeaponMultiplier;
                //Debug.Log("Got hit with Red");
                break;
            case Weapon.weaponTypeEnum.Blue:
                multiplier = BlueWeaponMultiplier;
                //Debug.Log("Got hit with Blue");
                break;
            case Weapon.weaponTypeEnum.Yellow:
                multiplier = YellowWeaponMultiplier;
                //Debug.Log("Got hit with Yellow");
                break;
        }


        health -= (int)(damage * multiplier);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        GameMaster.Score += scoreWorth;
    }
}
