using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private PlayerStats stats;

    [HeaderAttribute("Weapon Object")]
    public Transform firePoint;
    public GameObject currWeapon;
    public GameObject bulletPrefab;
    //public GameObject laserPrefab;
    public LineRenderer laserLine;
    public GameObject wavePrefab;

    [Header("Weapon Information")]
    public weaponTypeEnum weaponType;
    public enum weaponTypeEnum { Red, Blue, Yellow };
    public int damage;

    void Start()
    {
        stats = PlayerStats.instance;
        damage = stats.maxDamage;

        //Just makes sure your default weapon is the red type.
        currWeapon = bulletPrefab;
        weaponType = weaponTypeEnum.Red;
    }

    // Update is called once per frame
    void Update()
    {
        //First checks if your weapon is currently the laser which will enable
        //the lineRenderer, else dont.
        if (weaponType == weaponTypeEnum.Blue)
        {
            laserLine.enabled = true;
        }
        else
        {
            laserLine.enabled = false;
        }

        if (stats.fireRate < 0.0 && weaponType != weaponTypeEnum.Blue)
        {
            Shoot();
            stats.fireRate = stats.maxFireRate;
        }
        else if (stats.fireRate < 0.0 && weaponType == weaponTypeEnum.Blue)
        {
            //something special for the laser One
            laserShoot();
            stats.fireRate = stats.maxFireRate;
        }

        //fireRate -= (float)(Time.deltaTime * 2.0);
        stats.fireRate -= Time.deltaTime;
    }

    void Shoot()
    {
        //Debug.Log("Shooting!" + "Bullet Type:" + this.bulletType);
        FindObjectOfType<AudioManager>().Play("Game_SFX_PlayerShoot");
        Instantiate(currWeapon, firePoint.position, firePoint.rotation);
    }

    void laserShoot()
    {
        FindObjectOfType<AudioManager>().Play("Game_SFX_PlayerShoot");
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up);
        if (hitInfo)
        {
            Debug.Log("Laser Hit: " + hitInfo.transform.name);
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage, weaponType);
            }

            //Something to add for effects
            laserLine.SetPosition(0, firePoint.localPosition);
            Vector3 hitPos = firePoint.InverseTransformPoint(hitInfo.point);
            laserLine.SetPosition(1, hitPos);
        }
        else
        {
            laserLine.SetPosition(0, firePoint.localPosition);
            laserLine.SetPosition(1, firePoint.localPosition + firePoint.up * 10);
        }
    }
}
