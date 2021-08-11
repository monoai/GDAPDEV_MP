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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var stats = PlayerStats.instance;
        if(stats.fireRate < 0.0) {
            Shoot();
            stats.fireRate = stats.maxFireRate;
        }
        //fireRate -= (float)(Time.deltaTime * 2.0);
        stats.fireRate -= Time.deltaTime;
    }

    void Shoot()
    {
        //Debug.Log("Shooting!" + "Bullet Type:" + this.bulletType);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
