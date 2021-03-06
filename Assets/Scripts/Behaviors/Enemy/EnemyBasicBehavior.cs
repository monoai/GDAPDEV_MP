using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicBehavior : MonoBehaviour
{
    [Header("Behavior Stats")]
    public float maxFireRate = 1.5f;

    [Header("Weapon Object")]
    public Transform firePoint;
    public GameObject currWeapon;
    public LineRenderer laserLine;

    private float fireRate;
    private Vector3 firePos;
    private GameObject player;

    private Enemy.enemyTypeEnum enemyType;

    void Start()
    {
        enemyType = FindObjectOfType<Enemy>().enemyType;

        //We set any enemy stats by themselves instead of storing it into some large enemy script
        fireRate = maxFireRate;
        //We could also technically just pass a transform but I like typing things out on code instead.
        firePos = this.transform.position - new Vector3(0.0f, 0.5f, 0.0f);
        if (laserLine != null && enemyType == Enemy.enemyTypeEnum.BlueEnemy)
        {
            laserLine.enabled = true;
        }
    }

    void Update()
    {
        if (fireRate < 0.0)
        {
            //In case the player dies.
            player = GameObject.FindGameObjectWithTag("Player");
            switch (enemyType)
            {
                case Enemy.enemyTypeEnum.RedEnemy:
                    redBehavior();
                    break;
                case Enemy.enemyTypeEnum.BlueEnemy:
                    blueBehavior();
                    break;
                case Enemy.enemyTypeEnum.YellowEnemy:
                    yellowBehavior();
                    break;
            }
            //FindObjectOfType<AudioManager>().Play("Game_SFX_EnemyShoot");
            AudioManager.instance.Play("Game_SFX_EnemyShoot");
            fireRate = maxFireRate;
        }

        fireRate -= Time.deltaTime;
    }

    void redBehavior()
    {
        Vector3 targetDir = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.FromToRotation(-transform.up, targetDir);
        var bullet = Instantiate(currWeapon, firePos, rotation);
        var bulletValues = bullet.GetComponent<EnemyWeaponLogic>();
        bulletValues.damage = gameObject.GetComponent<Enemy>().damage;
    }

    void blueBehavior()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, -firePoint.up);
        Debug.DrawRay(firePoint.position, hitInfo.point * 10, Color.red);
        if (hitInfo.collider != null && hitInfo.collider.tag == "Player")
        {
            Debug.DrawRay(firePoint.position, hitInfo.point * 10, Color.blue);
            Debug.Log(hitInfo.collider.name);
            Player player = hitInfo.transform.GetComponent<Player>();
            if (player != null)
            {
                //function to hurt/kill player
                player.DamagePlayer(gameObject.GetComponent<Enemy>().damage);

                Debug.Log("Player Got Laser Hit!");
            }

            //Something to add for effects
            laserLine.SetPosition(0, firePoint.position);
            //Vector3 hitPos = transform.InverseTransformPoint(hitInfo.point);
            laserLine.SetPosition(1, hitInfo.point);
        }
        else
        {
            laserLine.SetPosition(0, firePoint.position);
            laserLine.SetPosition(1, firePoint.position + -transform.up * 10);
        }
    }

    void yellowBehavior()
    {
        var bullet = Instantiate(currWeapon, firePos, Quaternion.identity);
        var bulletValues = bullet.GetComponent<EnemyWeaponLogic>();
        bulletValues.damage = gameObject.GetComponent<Enemy>().damage;
    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
