using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage (int damage, string weaponType)
    {
        //Testing weaponType enum 
        //Note: WORKS but this code overwrites damage for now
        switch (weaponType)
        {
            case "Red": damage = 25; break;
            case "Blue": damage = 50; break;
            case "Yellow": damage = 100; break;
        }
        

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
