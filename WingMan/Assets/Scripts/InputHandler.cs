﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    static Vector2 touchDelta;
    static Touch myTouch;
    Vector3 tempMousePos;
    Vector3 mousePos;

    public static Touch GetTouch()
    {
        return myTouch;
    }
    public static Vector2 GetTouchDelta()
    {
        return touchDelta;
    }
    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            myTouch = Input.GetTouch(0);
            touchDelta = myTouch.deltaPosition;
        }
        else
        {
            touchDelta = Vector2.zero;
        }
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && !Application.isMobilePlatform)
        {


            touchDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        }
#endif

    }
}
