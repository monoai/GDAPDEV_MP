using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = PlayerStats.instance;

        stats.curHealth = stats.maxHealth;
        stats.fireRate = stats.maxFireRate;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamagePlayer(int damage) {
        stats.curHealth -= damage;
        if(stats.curHealth <= 0) {
            GameMaster.KillPlayer(this);
        }
    }
}
