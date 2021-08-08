using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTester : MonoBehaviour
{
    //Began
    public Sprite Idle;
    public Sprite Stationary;
    //Moved
    public Sprite Pressed;
    public Sprite Ended;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        int touches = Input.touchCount;
        if (touches > 0)
        {
            Touch t = Input.GetTouch(0);
            Debug.Log($"Touch: {t.phase}");

            
            if (t.phase != TouchPhase.Ended)
            {
                Ray r = Camera.main.ScreenPointToRay(t.position);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(r, out hit, Mathf.Infinity))
                {
                    _spriteRenderer.sprite = Pressed;
                }
                else
                {
                    _spriteRenderer.sprite = Idle;
                }
            }
            else
            {
                _spriteRenderer.sprite = Idle;
            }

            /*
            switch (t.phase)
            {
                case TouchPhase.Began:
                    _spriteRenderer.sprite = Idle; break;

                case TouchPhase.Stationary:
                    _spriteRenderer.sprite = Stationary; break;

                case TouchPhase.Moved:
                    _spriteRenderer.sprite = Pressed; break;

                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                    _spriteRenderer.sprite = Ended; break;
            }
            */ 

        }
    }

    private void OnDrawGizmos()
    {
        int touches = Input.touchCount;

        if (touches > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(0);
                Ray r = Camera.main.ScreenPointToRay(t.position);

                switch (t.fingerId)
                {
                    case 0:
                        Gizmos.DrawIcon(r.GetPoint(10), "Ainz"); break;
                    case 1:
                        Gizmos.DrawIcon(r.GetPoint(10), "Bete"); break;

                    default:
                        Gizmos.DrawIcon(r.GetPoint(10), "Bell"); break;
                }
            }
        }
    }
}
