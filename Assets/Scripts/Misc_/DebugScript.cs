using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{

    public GameObject Player;

    float nextTimeToSearch = 0;

    public void damagePlayer()
    {
        Player.GetComponent<Player>().DamagePlayer(20);
        Debug.Log("DIE PLAYEERR");
    }

    public void spawnTest()
    {
        GameMaster.gm.testSpawn(new Vector3(0.25f, 0.25f, 0.0f));
        Debug.Log("Space Pressed, Spawning...");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            findPlayer();
            return;
        }
    }

    void findPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
            {
                Player = searchResult;
            }
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
}
