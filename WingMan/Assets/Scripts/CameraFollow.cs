using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public float followSpeed;
    public float yOffSet;
    Vector3 tempPos;
    private void Start()
    {
        tempPos = transform.position;
    }
    private void LateUpdate()
    {
        tempPos.y = Mathf.Lerp(transform.position.y, followTarget.position.y-yOffSet, followSpeed *Time.fixedDeltaTime* Time.deltaTime);
        transform.position = tempPos;
    }
}
