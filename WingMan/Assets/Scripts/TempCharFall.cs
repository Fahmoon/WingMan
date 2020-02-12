using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCharFall : MonoBehaviour
{
    Rigidbody myRb;
    float stamp;
    void Start()
    {
        stamp = Time.time;
        myRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        myRb.MovePosition(transform.position + Vector3.down);
        if (transform.position.y<1)
        {
            Debug.Log(Time.time - stamp);
        }
    }
}
