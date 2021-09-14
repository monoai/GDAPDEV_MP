using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class GestureManager : MonoBehaviour
{
    public static GestureManager Instance;

    // Gesture properties
    public DragProperty _dragProperty;
    public SwipeProperty _swipeProperty;
    //public TapProperty _tapProperty;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Primary trackers
    private Touch trackedFinger1;
    private Touch trackedFinger2;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private float gestureTime1;
    private float gestureTime2;

    // Player object for direct manipulation
    private GameObject player;
    private PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = PlayerStats.instance;
    }

    // Update is called once per frame
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(trackedFinger1.position);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        if (player == null)
        {
            player = GameMaster.gm.findPlayer();
        }

        if (Input.touchCount > 0)
        {
            CheckSingleFingerGestures();
        }
        if (Input.touchCount > 1)
        {
            trackedFinger1 = Input.GetTouch(0);
            trackedFinger2 = Input.GetTouch(1);

            //yo this is mad ugly, why
            //Starts gesture time if finger1 either moves or is stationary while finger2 began moving
            if (trackedFinger1.phase == TouchPhase.Moved && trackedFinger2.phase == TouchPhase.Began || trackedFinger1.phase == TouchPhase.Stationary && trackedFinger2.phase == TouchPhase.Began)
            {
                gestureTime2 = 0;
                startPoint = trackedFinger2.position;
            }
            //Ends evaluation if finger2 ends touching
            else if (trackedFinger1.phase == TouchPhase.Moved && trackedFinger2.phase == TouchPhase.Ended)
            {
                endPoint = trackedFinger2.position;

                // Swipe Events
                if (gestureTime2 <= _swipeProperty.swipeTime &&
                    Vector2.Distance(startPoint, endPoint) >= (Screen.dpi * _swipeProperty.minSwipeDistance))
                {
                    FireSwipeEvent(false);
                }
            }
            //if finger2 is still moving, keep adding gesture time
            else
            {
                gestureTime2 += Time.deltaTime;
            }
        }
    }

    private void CheckSingleFingerGestures()
    {
        trackedFinger1 = Input.GetTouch(0);

        if (trackedFinger1.phase == TouchPhase.Began)
        {
            gestureTime1 = 0;
            startPoint = trackedFinger1.position;
        }
        else if (trackedFinger1.phase == TouchPhase.Ended)
        {
            endPoint = trackedFinger1.position;

            // Swipe Events
            if (gestureTime1 <= _swipeProperty.swipeTime &&
                Vector2.Distance(startPoint, endPoint) >= (Screen.dpi * _swipeProperty.minSwipeDistance))
            {
                FireSwipeEvent(true);
            }
        }
        else
        {
            gestureTime1 += Time.deltaTime;
            if (gestureTime1 >= _dragProperty.dragBufferTime)
            {
                FireDragEvent(trackedFinger1);
            }
        }
    }

    private void FireDragEvent(Touch touch)
    {
        //Debug.Log($"Drag: {trackedFinger1.position}");

        player.transform.Translate(touch.deltaPosition.x / Screen.width * (5.0f * stats.moveSpeedPercent), touch.deltaPosition.y / Screen.height * (5.0f * stats.moveSpeedPercent * stats.verticalCompensator), 0.0f);
    }

    private void FireSwipeEvent(bool isSingle)
    {
        Debug.Log("Swiped");
        Vector2 dir = endPoint - startPoint;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) // Horizontal Swipes
        {
            if (isSingle)
            {
                if (dir.x > 0)
                {
                    Debug.Log("Right");
                    player.transform.position = new Vector3(player.transform.position.x + 2.5f, player.transform.position.y, 0.0f);
                }
                else
                {
                    Debug.Log("Left");
                    player.transform.position = new Vector3(player.transform.position.x - 2.5f, player.transform.position.y, 0.0f);
                }
            }
        }
        else // Vertical Swipes
        {
            if (isSingle == false)
            {
                if (dir.y > 0)
                {
                    Debug.Log("Up");
                    GameMaster.gm.changeWeapon(1);
                }
                else
                {
                    Debug.Log("Down");
                    GameMaster.gm.changeWeapon(-1);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        int touches = Input.touchCount;

        if (touches > 0)
        {
            Ray r = Camera.main.ScreenPointToRay(trackedFinger1.position);
            Gizmos.DrawIcon(r.GetPoint(10), "Bell Cranel Chibi");
        }
    }
}
