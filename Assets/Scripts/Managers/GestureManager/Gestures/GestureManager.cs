using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class GestureManager : MonoBehaviour
{
    public static GestureManager Instance;
    public EventHandler<TapEventArgs> OnTap;
    public EventHandler<DragEventArgs> OnDrag;
    public EventHandler<SwipeEventArgs> OnSwipe;

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
    private Vector2 startPoint;
    private Vector2 endPoint;
    private float gestureTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            trackedFinger1 = Input.GetTouch(0);
            if(trackedFinger1.phase == TouchPhase.Began)
            {
                gestureTime = 0;
                startPoint = trackedFinger1.position;
            }
            else if (trackedFinger1.phase == TouchPhase.Ended)
            {
                endPoint = trackedFinger1.position;

                // Tap Events
                if (gestureTime <= _tapProperty.tapTime &&
                    Vector2.Distance(startPoint, endPoint) < (Screen.dpi * _tapProperty.tapMaxDistance))
                {
                    FireTapEvent(startPoint);
                }

                // Swipe Events
                if (gestureTime <= _swipeProperty.swipeTime &&
                    Vector2.Distance(startPoint, endPoint) >=  (Screen.dpi * _swipeProperty.minSwipeDistance) )
                {
                    FireSwipeEvent();
                }
            }
            else
            {
                gestureTime += Time.deltaTime;
                if (gestureTime >= _dragProperty.dragBufferTime)
                {
                    FireDragEvent();
                }
            }
        }
    }

    private void FireDragEvent()
    {
        Debug.Log($"Drag: {trackedFinger1.position}");

        Ray r = Camera.main.ScreenPointToRay(trackedFinger1.position);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }

        DragEventArgs args = new DragEventArgs(trackedFinger1, hitObj);
        if (OnDrag != null)
        {
            OnDrag(this, args);
        }

        if (hitObj != null)
        {
            IDrag draginterface = hitObj.GetComponent<IDrag>();
            if (draginterface != null)
            {
                draginterface.OnDrag(args);
            }
        }
    }

    private void FireSwipeEvent()
    {
        Debug.Log("Swiped");
        Vector2 dir = endPoint - startPoint;

        Ray r = Camera.main.ScreenPointToRay(startPoint);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if (Physics.Raycast(r, out hit, Mathf.Infinity))
        {
            hitObj = hit.collider.gameObject;
        }


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

        SwipeEventArgs args = new SwipeEventArgs(startPoint, swipeDir, dir, hitObj);
        if (OnSwipe != null)
        {
            OnSwipe(this, args);
        }

        if (hitObj != null)
        {
            ISwipe swipeInterface = hitObj.GetComponent<ISwipe>();
            if (swipeInterface != null)
            {
                swipeInterface.OnSwipe(args);
            }
        }
    }

    private void FireTapEvent(Vector2 pos)
    {
        Debug.Log("Tap");
        if (OnTap != null)
        {
            GameObject hitObj = null;
            Ray r = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(r, out hit, Mathf.Infinity))
            {
                hitObj = hit.collider.gameObject;
            }

            TapEventArgs args = new TapEventArgs(pos, hitObj);
            OnTap(this, args);

            if (hitObj != null)
            {
                //TapObjectReceiver rec = hitObj.GetComponent<TapObjectReceiver>();
                ITap rec = hitObj.GetComponent<ITap>();
                if (rec != null)
                {
                    rec.OnTap();
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
