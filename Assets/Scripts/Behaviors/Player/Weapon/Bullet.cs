using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "Enemy")
        {
            Debug.Log("Hit" + hitInfo.name);
            Destroy(gameObject);
        }

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(FindObjectOfType<Weapon>().damage, FindObjectOfType<Weapon>().bulletType.ToString());
        }
    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
