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
    public TapProperty _tapProperty;

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

    private Touch trackedFinger1;
    private Touch trackedFinger2;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private float gestureTime1;
    private float gestureTime2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(trackedFinger1.position);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        if (Input.touchCount > 0)
        {
            CheckSingleFingerGestures();
        }
        if (Input.touchCount > 1)
        {
            trackedFinger1 = Input.GetTouch(0);
            trackedFinger2 = Input.GetTouch(1);

            //yo this is mad ugly, why
            if (trackedFinger1.phase == TouchPhase.Moved && trackedFinger2.phase == TouchPhase.Began || trackedFinger1.phase == TouchPhase.Stationary && trackedFinger2.phase == TouchPhase.Began)
            {
                gestureTime2 = 0;
                startPoint = trackedFinger2.position;
            }
            else if (trackedFinger1.phase == TouchPhase.Moved && trackedFinger2.phase == TouchPhase.Ended)
            {
                endPoint = trackedFinger2.position;

                // Swipe Events
                if (gestureTime2 <= _swipeProperty.swipeTime &&
                    Vector2.Distance(startPoint, endPoint) >= (Screen.dpi * _swipeProperty.minSwipeDistance))
                {
                    FireSwipeEvent();
                }
            }
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
                FireSwipeEvent();
            }
        }
        else
        {
            gestureTime1 += Time.deltaTime;
            if (gestureTime1 >= _dragProperty.dragBufferTime)
            {
                FireDragEvent();
            }
        }
    }

    private void FireDragEvent()
    {
        Debug.Log($"Drag: {trackedFinger1.position}");

    }

    private void FireSwipeEvent()
    {
        Debug.Log("Swiped");
        Vector2 dir = endPoint - startPoint;

        SwipeDirections swipeDir = SwipeDirections.RIGHT;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) // Horizontal Swipes
        {
            if (dir.x > 0)
            {
                Debug.Log("Right");
                swipeDir = SwipeDirections.RIGHT;
            }
            else
            {
                Debug.Log("Left");
                swipeDir = SwipeDirections.LEFT;
            }
        }
        else // Vertical Swipes
        {
            if (dir.y > 0)
            {
                Debug.Log("Up");
                swipeDir = SwipeDirections.UP;
            }
            else
            {
                Debug.Log("Down");
                swipeDir = SwipeDirections.DOWN;
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
