using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public float followSpeed;
    public float yOffSet;
    Vector3 tempPos;
    Vector3 offSet;
    private void Start()
    {
        offSet = new Vector3(0, yOffSet, 0);
        tempPos = transform.position;
    }
    private void LateUpdate()
    {
        tempPos = Vector3.Lerp(transform.position, followTarget.position-offSet, followSpeed *Time.fixedDeltaTime* Time.deltaTime);
        transform.position = tempPos;
    }
}
