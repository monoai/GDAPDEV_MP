using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HeaderAttribute("Bullet Object")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    [Header("Bullet Information")]
    public bulletTypeEnum bulletType;
    public enum bulletTypeEnum {Red, Blue, Yellow};
    public int damage;
    public float fireRate;

    private float maxFireRate;

    void Start()
    {
        maxFireRate = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(fireRate < 0.0) {
            Shoot();
            fireRate = maxFireRate;
        }
        //fireRate -= (float)(Time.deltaTime * 2.0);
        fireRate -= Time.deltaTime;
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        Debug.Log("Hit" + hitInfo.name);
        //Enemy enemy = hitInfo.GetComponent<Enemy>();
        //if (enemy != null)
        //{
        //    enemy.TakeDamage(damage, weaponType);
        //}
    }

    void Shoot()
    {
        Debug.Log("Shooting!" + "Bullet Type:" + this.bulletType);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
