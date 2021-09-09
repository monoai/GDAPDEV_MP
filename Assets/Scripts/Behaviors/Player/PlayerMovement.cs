using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 initTouchPos = new Vector3(0, 0, 0);

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameMaster.gm.testSpawn(new Vector3(0.5f, 0.5f, 0.0f));
            Debug.Log("Space Pressed, Spawning...");
        }
    }
}
