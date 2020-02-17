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
        if (GameManager.Instance.CurrentPlayerState!=PlayerStates.Parachuting)
        {
        myRb.MovePosition(transform.position + Vector3.down*0.5f);
        }
       
    }
}
