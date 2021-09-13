using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponLogic : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body.velocity = -transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Player")
        {
            Debug.Log("Player Got Hit!");
            Destroy(gameObject);
        }

        Player player = hitInfo.GetComponent<Player>();
        if (player != null)
        {
            //function to hurt/kill player
        }
    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
