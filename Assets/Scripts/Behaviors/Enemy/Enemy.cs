using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enemyTypeEnum enemyType;
    public enum enemyTypeEnum { RedEnemy, BlueEnemy, YellowEnemy, RedBoss, BlueBoss, YellowBoss };

    public int health = 100;
    public int scoreWorth;
    public int damage = 10;
    public HealthBar hpBar;

    void Start()
    {
        if 
        (enemyType == enemyTypeEnum.RedEnemy ||
         enemyType == enemyTypeEnum.BlueEnemy ||
         enemyType == enemyTypeEnum.YellowEnemy) FindObjectOfType<AudioManager>().Play("Game_SFX_BasicSpawn");
        else if 
        (enemyType == enemyTypeEnum.RedBoss ||
         enemyType == enemyTypeEnum.BlueBoss ||
         enemyType == enemyTypeEnum.YellowBoss) FindObjectOfType<AudioManager>().Play("Game_SFX_BossSpawn");

        hpBar.SetHealthBar(health);
    }

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
        //FindObjectOfType<AudioManager>().Play("Game_SFX_EnemyHit");
        AudioManager.instance.Play("Game_SFX_EnemyHit");
        health -= (int)(damage * multiplier);

        hpBar.SetHealth(health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //FindObjectOfType<AudioManager>().Play("Game_SFX_EnemyDeath");
        AudioManager.instance.Play("Game_SFX_EnemyDeath");
        Destroy(gameObject);
        GameMaster.Score += scoreWorth;
    }
}
