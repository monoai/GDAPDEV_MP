using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 initTouchPos = new Vector3(0,0,0);

    public float MoveSpeed = 0.05f;
    public Joystick joystick;
    public GameObject JoystickObject;

    [HideInInspector] public enum ControlTypes { A, B };
    [HideInInspector] public ControlTypes controlType = ControlTypes.A;

    void Start()
    {
        if (PlayerPrefs.GetInt("ControlType", 0) == 0) controlType = ControlTypes.A; // Set in SettingsManager
        else controlType = ControlTypes.B;
    }

    // Update is called once per frame
    void Update()
    {
        // JOYSTICK INPUTS
        if (controlType == ControlTypes.A)
        {
            JoystickObject.SetActive(true);
            Vector3 movement;
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;
            movement.z = 0.0f;

            transform.position += movement * MoveSpeed;
            Debug.Log("Joystick is being used");
        }

        // TOUCH DRAG
        else if (controlType == ControlTypes.B) // just a temp flag to disable touch movement instead of comment blocks
        {
            JoystickObject.SetActive(false);
            Debug.Log("Touch Drag is being used");
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
