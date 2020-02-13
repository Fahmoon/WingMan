using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionClamping : MonoBehaviour
{

    private Camera _mainCamera;
    public static ClipPoints _clipPoints;
    private Rigidbody _rigidbody;
    private float _clampedX, _clampedZ;

    private void Start()
    {
        _mainCamera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        float playerHeight = Mathf.Abs(transform.position.y - _mainCamera.transform.position.y);
        _clipPoints = GameManager.CalculatePlayGround(playerHeight, _mainCamera);
        ObstacleGenerator.Instance.GenerateNewLevel(_clipPoints);
    }


    private void FixedUpdate()
    {
        _clampedX = Mathf.Clamp(transform.position.x, _clipPoints.upperLeft.x, _clipPoints.upperRight.x);
        _clampedZ = Mathf.Clamp(transform.position.z, _clipPoints.downLeft.z, _clipPoints.upperRight.z);

        Vector3 clampedPos = new Vector3(_clampedX, transform.position.y, _clampedZ);

        _rigidbody.MovePosition(clampedPos);
    }
}
