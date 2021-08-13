using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    public float speed = 10f;
    public bool isLaser = false;
    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        if(!isLaser) {
            body.velocity = transform.up * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "Enemy")
        {
            Debug.Log("Hit" + hitInfo.name);
            if(!isLaser) {
                Destroy(gameObject);
            }
        }

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(FindObjectOfType<Weapon>().damage, FindObjectOfType<Weapon>().weaponType);
        }
    }

    void OnBecameInvisible()
    {
        if(!isLaser) {
            enabled = false;
            Destroy(gameObject);
        }
    }
}
