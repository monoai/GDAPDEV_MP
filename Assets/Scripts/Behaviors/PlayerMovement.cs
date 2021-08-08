using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int touchCount = Input.touchCount;
        if(touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            //Debug.Log($"Finger state: {t.phase}");
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(t.position);
            touchPosition.z = 0f;
            //Vector3 newPosition = touchPosition - transform.position;
            transform.position = touchPosition;
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
}
