using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnScreenJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image JoystickParent;
    public Image Stick;

    public Vector2 JoystickAxis = Vector2.zero;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickParent.rectTransform, 
                                                                    eventData.position, 
                                                                    eventData.pressEventCamera, 
                                                                    out localPoint))
        {
            float half_w = JoystickParent.rectTransform.rect.width / 2;
            float half_h = JoystickParent.rectTransform.rect.height / 2;

            float x = localPoint.x / half_w;
            float y = localPoint.y / half_h;

            JoystickAxis.x = x;
            JoystickAxis.y = y;

            if (JoystickAxis.magnitude > 1) JoystickAxis.Normalize();

            Stick.rectTransform.localPosition = new Vector3(JoystickAxis.x * half_w, JoystickAxis.y * half_h);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        JoystickAxis = Vector2.zero;
        Stick.rectTransform.localPosition = Vector2.zero;
    }
}
