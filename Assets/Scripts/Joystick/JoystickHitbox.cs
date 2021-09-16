using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickHitbox : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image hitbox;
    public OnScreenJoystick joystick;

    public void OnDrag(PointerEventData eventData)
    {
        joystick.OnDrag(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(hitbox.rectTransform,
                                                                    eventData.position,
                                                                    eventData.pressEventCamera,
                                                                    out localPoint))
        {
            joystick.gameObject.SetActive(true);
            RectTransform rTrans = joystick.gameObject.GetComponent<RectTransform>();

            rTrans.localPosition = localPoint;
            joystick.OnPointerDown(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystick.gameObject.SetActive(false);
        joystick.OnPointerUp(eventData);
    }
}
