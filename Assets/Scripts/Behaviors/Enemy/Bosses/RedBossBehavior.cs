using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBossBehavior : MonoBehaviour
{
    [Header("Behavior Stats")]
    public float maxFireRate = 1.5f;

    [Header("Weapon Object")]
    public Transform firePos;
    public GameObject currWeapon;

    [Header("Boss Settings")]
    public float fireAngle = 30.0f;
    public float waveSpeed = 1.0f;

    [Header("Bullet Properties")]
    public float bulletSpeed;
    //public float damage;

    //Hidden Calculating Variables
    private float fireRate;
    private Vector3 ogFirePos;
    private Quaternion ogFireRot;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //We set any enemy stats by themselves instead of storing it into some large enemy script
        fireRate = maxFireRate;
        //We could also technically just pass a transform but I like typing things out on code instead.
        //firePos = this.transform.localPosition;// - new Vector3(0.0f, 0.5f, 0.0f);
        ogFirePos = firePos.transform.position;
        ogFireRot = firePos.transform.rotation;
    }

    public void RotatePoint(Transform transform, Vector3 pivotPoint, Vector3 axis, float angle)
    {
        Quaternion rot = Quaternion.AngleAxis(angle, axis);
        transform.position = rot * (transform.position - pivotPoint) + pivotPoint;
        transform.rotation = rot * transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        firePos.SetPositionAndRotation(ogFirePos, ogFireRot);
        float angle = fireAngle * Mathf.Sin((Time.time) * waveSpeed);
        Debug.Log("Angle: " + (angle));
        //firePos.transform.RotateAround(this.transform.position, new Vector3(0.0f, 0.0f, 1.0f), angle);
        RotatePoint(firePos.transform, this.transform.position, new Vector3(0.0f, 0.0f, 1.0f), angle);

        if (fireRate < 0.0)
        {
            var bullet = Instantiate(currWeapon, firePos.position, firePos.rotation);
            var bulletValues = bullet.GetComponent<EnemyWeaponLogic>();
            bulletValues.speed = bulletSpeed;
            bulletValues.damage = gameObject.GetComponent<Enemy>().damage;

            fireRate = maxFireRate;
        }

        fireRate -= Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
