using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 initTouchPos = new Vector3(0,0,0);

    public float MoveSpeed = 0.05f;
    public Joystick joystick;

    [HideInInspector] public enum ControlType { A, B };
    [HideInInspector] public ControlType _controlType = ControlType.A;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // JOYSTICK INPUTS
        if (_controlType == ControlType.A)
        {
            Vector3 movement;
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;
            movement.z = 0.0f;

            transform.position += movement * MoveSpeed;
        }
        else if (_controlType == ControlType.B) // just a temp flag to disable touch movement instead of comment blocks
        {
            int touchCount = Input.touchCount;
            if (touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                //Debug.Log($"Finger state: {t.phase}");
                if (t.phase == TouchPhase.Began)
                {
                    initTouchPos = Camera.main.ScreenToWorldPoint(t.position);
                }
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(t.position);
                //Debug.Log("TouchPos" + (touchPos - initTouchPos));
                touchPos.z = 0f;
                Vector3 newPosition = (touchPos - initTouchPos);
                //transform.position += newPosition; //New Movement Formula (Needs Refinement)
                transform.position = touchPos;
                /*
                if(t.phase != TouchPhase.Ended)
                {
                    Ray r = Camera.main.ScreenPointToRay(t.position);
                    RaycastHit hit = new RaycastHit();

                    if(Physics.Raycast(r, out hit, Mathf.Infinity)){
                        Debug.Log($"Hit: {hit.collider.name}");
                    }
                    else {
                        //nothing
                    }
                }
                else
                {
                    //nothing
                }
                */
            }
        }

        /*
        if(Input.GetKeyDown("space")) {
            this.DamagePlayer(20);
            Debug.Log("DIE PLAYEERR");
        }
        */
    }
}
