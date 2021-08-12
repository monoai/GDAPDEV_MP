using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GesturesObjectReceiver : MonoBehaviour, ITap, ISwipe, IDrag
{

    public float speed = 10;
    private Vector3 TargetPos = Vector3.zero;

    public void OnDrag(DragEventArgs args)
    {
        if (args.HitObject == gameObject)
        {
            Ray r = Camera.main.ScreenPointToRay(args.TargetFinger.position);
            Vector3 worldPos = r.GetPoint(10);

            TargetPos = worldPos;
            transform.position = worldPos;
        }
    }

    public void OnSwipe(SwipeEventArgs args)
    {
        Vector3 dir = args.SwipeVector.normalized;

        /*
        Vector3 dir = Vector3.zero;

        switch (args.SwipeDirection)
        {
            case SwipeDirections.RIGHT: dir.x = 1; break;
            case SwipeDirections.LEFT: dir.x = -1; break;
            case SwipeDirections.UP: dir.x = 1; break;
            case SwipeDirections.DOWN: dir.x = -1; break;
        }
        */

        TargetPos += (dir * 5);
    }

    private void OnEnable()
    {
        TargetPos = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, speed * Time.deltaTime);
    }

    public void OnTap()
    {
        Destroy(gameObject);
    }
}
