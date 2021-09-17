using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStats stats;
    private GameObject hpBar;

    //Only for the sake of easier debugging values
    //Can be removed if no longer needed
    [Header("[DEBUG] Clamp Values")]
    public float xMinClamp;
    public float xMaxClamp;
    public float yMinClamp;
    public float yMaxClamp;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Game_SFX_PlayerSpawn");
        stats = PlayerStats.instance;

        stats.curHealth = stats.maxHealth;
        stats.fireRate = stats.maxFireRate;

        hpBar = GameObject.Find("PlayerHP Bar");
        hpBar.GetComponent<HealthBar>().SetHealthBar(stats.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //Can possibly be improved so that player will guaranteed be within the bounds (it has this sort of bounciness/jitter? at times), but otherwise it's pretty much a bit perfect already eh?
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, xMinClamp, xMaxClamp);
        pos.y = Mathf.Clamp(pos.y, yMinClamp, yMaxClamp);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    public void DamagePlayer(int damage)
    {
        FindObjectOfType<AudioManager>().Play("Game_SFX_PlayerHit");
        stats.curHealth -= damage;
        hpBar.GetComponent<HealthBar>().SetHealth(stats.curHealth);
        if (stats.curHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }
    }
}
