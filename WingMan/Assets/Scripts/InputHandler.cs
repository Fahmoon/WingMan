using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    static Vector2 touchDelta;
    static Touch myTouch;


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
    }
}
