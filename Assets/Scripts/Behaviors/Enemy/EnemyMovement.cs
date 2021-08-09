using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float timer = 3f;
    public float speed = 2;
    private int switchMovement = 1;

    private float maxTimer;
    void Start()
    {
        maxTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
        {
            switchMovement *= -1;
            timer = maxTimer * 2;
        }
        
        if (switchMovement == 1)
        {
            transform.position += Vector3.right * Time.deltaTime;
        }
        else
            transform.position += Vector3.left * Time.deltaTime;

        timer -= Time.deltaTime;
    }
}