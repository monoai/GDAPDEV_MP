using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGestMainReceiver : MonoBehaviour, ITap, ISwipe, IDrag
{
    public float speed = 10;
    private Vector3 TargetPos = Vector3.zero;
    private GameObject player;

    public void OnDrag(DragEventArgs args)
    {
        Ray r = Camera.main.ScreenPointToRay(args.TargetFinger.position);
        Vector3 worldPos = r.GetPoint(10);

        TargetPos = worldPos;
        player.transform.position = worldPos;
    }

    public void OnSwipe(SwipeEventArgs args)
    {
        /*
        Vector3 dir = args.SwipeVector.normalized;

        /
        Vector3 dir = Vector3.zero;

        switch (args.SwipeDirection)
        {
            case SwipeDirections.RIGHT: dir.x = 1; break;
            case SwipeDirections.LEFT: dir.x = -1; break;
            case SwipeDirections.UP: dir.x = 1; break;
            case SwipeDirections.DOWN: dir.x = -1; break;
        }
        /

        TargetPos += (dir * 5);
        */
    }

    private void OnEnable()
    {
        TargetPos = player.transform.position;
    }

    private void Update()
    {
        if(player != null) {
            player.transform.position = Vector3.MoveTowards(player.transform.position, TargetPos, speed * Time.deltaTime);
        } else {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void OnTap()
    {
        //Destroy(gameObject);
    }
}
