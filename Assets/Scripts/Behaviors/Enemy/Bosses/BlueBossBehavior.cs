using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBossBehavior : MonoBehaviour
{
    [Header("Behavior Stats")]
    //To compensate overflowing since the lasers will continuously detect colliders in frame-time instead of time-based
    public float maxFireRate = 0.1f;

    [Header("Weapon Object")]
    public Transform firePosUp;
    public Transform firePosDown;
    public Transform firePosRight;
    public Transform firePosLeft;
    public LineRenderer laserUp;
    public LineRenderer laserDown;
    public LineRenderer laserLeft;
    public LineRenderer laserRight;

    [Header("Boss Settings")]
    public float rotSpeed = 1.0f;

    //Hidden Calculating Variables
    private float fireRate;
    private Vector3 ogFirePos;
    private Quaternion ogFireRot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        firePosUp.transform.RotateAround(this.transform.position, new Vector3(0.0f, 0.0f, 1.0f), Time.deltaTime * rotSpeed);
        firePosDown.transform.RotateAround(this.transform.position, new Vector3(0.0f, 0.0f, 1.0f), Time.deltaTime * rotSpeed);
        firePosLeft.transform.RotateAround(this.transform.position, new Vector3(0.0f, 0.0f, 1.0f), Time.deltaTime * rotSpeed);
        firePosRight.transform.RotateAround(this.transform.position, new Vector3(0.0f, 0.0f, 1.0f), Time.deltaTime * rotSpeed);
        //Debug.Log("Rot Speed: " + (Time.deltaTime * rotSpeed));

        RaycastHit2D hitInfoUp = Physics2D.Raycast(firePosUp.position, firePosUp.up);
        RaycastHit2D hitInfoDown = Physics2D.Raycast(firePosDown.position, -firePosDown.up);
        RaycastHit2D hitInfoLeft = Physics2D.Raycast(firePosLeft.position, -firePosLeft.right);
        RaycastHit2D hitInfoRight = Physics2D.Raycast(firePosRight.position, firePosRight.right);

        //Debug.DrawRay(firePoint.position, hitInfo.point * 10, Color.red);
        if (fireRate < 0.0f)
        {
            if (hitInfoUp.collider != null && hitInfoUp.collider.tag == "Player")
            {
                //Debug.DrawRay(firePoint.position, hitInfo.point * 10, Color.blue);
                Debug.Log("Up: " + hitInfoUp.collider.name);
                gotHit(hitInfoUp);
            }
            else if (hitInfoDown.collider != null && hitInfoDown.collider.tag == "Player")
            {
                Debug.Log("Down: " + hitInfoDown.collider.name);
                gotHit(hitInfoDown);
            }
            else if (hitInfoLeft.collider != null && hitInfoLeft.collider.tag == "Player")
            {
                Debug.Log("Left: " + hitInfoLeft.collider.name);
                gotHit(hitInfoLeft);
            }
            else if (hitInfoRight.collider != null && hitInfoRight.collider.tag == "Player")
            {
                Debug.Log("Right: " + hitInfoRight.collider.name);
                gotHit(hitInfoRight);
            }
            fireRate = maxFireRate;
        }
        fireRate -= Time.deltaTime;

        //Initial Positions
        laserUp.SetPosition(0, firePosUp.position);
        laserDown.SetPosition(0, firePosDown.position);
        laserLeft.SetPosition(0, firePosLeft.position);
        laserRight.SetPosition(0, firePosRight.position);
        //End Positions
        laserUp.SetPosition(1, firePosUp.position + firePosUp.transform.up * 10);
        laserDown.SetPosition(1, firePosDown.position + -firePosDown.transform.up * 10);
        laserLeft.SetPosition(1, firePosLeft.position + -firePosLeft.transform.right * 10);
        laserRight.SetPosition(1, firePosRight.position + firePosRight.transform.right * 10);
    }

    void gotHit(RaycastHit2D hitInfo)
    {
        Player player = hitInfo.transform.GetComponent<Player>();
        if (player != null)
        {
            //function to hurt/kill player
            player.DamagePlayer(10);
            Debug.Log("Player Got Laser Hit!");
        }
    }

    void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
