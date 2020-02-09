using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteControls : MonoBehaviour
{
    Rigidbody myRb;
    public float parachuteDrag;
    public float canopyHeight;
    public float controlForce;
    public float rotationSpeed;
    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 forcesToAdd;
        forcesToAdd = new Vector3(InputHandler.GetTouchDelta().x, 0, InputHandler.GetTouchDelta().y) * controlForce;
        myRb.AddForce(forcesToAdd,ForceMode.VelocityChange);
        myRb.AddForceAtPosition(Vector3.up * parachuteDrag, transform.TransformPoint(Vector3.up * canopyHeight));
    }
}
