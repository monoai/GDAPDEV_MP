using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBossBehavior : MonoBehaviour
{
    public enum AttackType { A, B, C };

    [Header("Behavior Stats")]
    public float maxFireRate = 1.5f;
    public int maxAttackType = 5;

    [Header("Weapon Object")]
    public Transform firePos1;
    public Transform firePos2;
    public Transform firePos3;
    public GameObject currWeapon;

    [Header("Boss Settings")]
    public float waveSpeed = 1.0f;

    [Header("Bullet Properties")]
    public float bulletSpeed;
    //public float damage;

    //Hidden Calculating Variables
    private float fireRate;
    private int typeChange = 0;
    private AttackType attackState = AttackType.A;

    // Start is called before the first frame update
    void Start()
    {
        //We set any enemy stats by themselves instead of storing it into some large enemy script
        fireRate = maxFireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate < 0.0)
        {
            if (attackState == AttackType.A)
            {
                StartCoroutine(fireBehavior(firePos1, 0));
                StartCoroutine(fireBehavior(firePos2, 0.5f));
                StartCoroutine(fireBehavior(firePos3, 0.5f));
            }
            else if (attackState == AttackType.B)
            {
                StartCoroutine(fireBehavior(firePos1, 0.35f));
                StartCoroutine(fireBehavior(firePos2, 0.5f));
                StartCoroutine(fireBehavior(firePos3, 0.0f));
            }
            else if (attackState == AttackType.C)
            {
                StartCoroutine(fireBehavior(firePos1, 0.35f));
                StartCoroutine(fireBehavior(firePos2, 0.0f));
                StartCoroutine(fireBehavior(firePos3, 0.5f));
            }

            fireRate = maxFireRate;
            typeChange++;
            if (typeChange > maxAttackType)
            {
                attackState = (AttackType)Random.Range((int)0, (int)3);
                typeChange = 0;
            }
        }

        fireRate -= Time.deltaTime;
    }

    IEnumerator fireBehavior(Transform firePos, float duration)
    {
        yield return new WaitForSeconds(duration);
        var bullet = Instantiate(currWeapon, firePos.position, firePos.rotation);
        var bulletValues = bullet.GetComponent<EnemyWeaponLogic>();
        bulletValues.speed = bulletSpeed;
        bulletValues.damage = gameObject.GetComponent<Enemy>().damage;
    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
