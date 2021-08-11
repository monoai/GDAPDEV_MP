using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 initTouchPos = new Vector3(0,0,0);

    void Start()
    {

    }

    // Update is called once per frame

    [System.Serializable]
    public class PlayerStats2 {
        public int Health = 100;
    }

    public PlayerStats2 playerStats = new PlayerStats2();

    void Update()
    {
        if(Input.GetKeyDown("space")) {
            this.DamagePlayer(20);
            Debug.Log("DIE PLAYEERR");
        }

        int touchCount = Input.touchCount;
        if(touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            //Debug.Log($"Finger state: {t.phase}");
            if(t.phase == TouchPhase.Began){
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

    public void DamagePlayer(int damage) {
        playerStats.Health -= damage;
        if(playerStats.Health <= 0) {
            GameMaster.KillPlayer(this);
        }
    }
}
