using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionClamping : MonoBehaviour
{

    private Camera _mainCamera;
    // public static ClipPoints _clipPoints;
    private Rigidbody _rigidbody;
    private float _clampedX, _clampedZ;
    [SerializeField]
    Transform maxXZ,minXZ;
    private void Start()
    {
        _mainCamera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        float playerHeight = Mathf.Abs(transform.position.y - _mainCamera.transform.position.y);
        //  _clipPoints = GameManager.CalculatePlayGround(playerHeight, _mainCamera);
        ObstacleGenerator.Instance.GenerateNewLevel();
    }


    private void FixedUpdate()
    {
        //_clampedX = Mathf.Clamp(transform.position.x, _clipPoints.upperLeft.x , _clipPoints.upperRight.x);
        //_clampedZ = Mathf.Clamp(transform.position.z, _clipPoints.downLeft.z, _clipPoints.upperRight.z);
        _clampedX = Mathf.Clamp(transform.position.x, minXZ.position.x, maxXZ.position.x);
        _clampedZ = Mathf.Clamp(transform.position.z, minXZ.position.z, maxXZ.position.z);

        Vector3 clampedPos = new Vector3(_clampedX, transform.position.y, _clampedZ);

        _rigidbody.MovePosition(clampedPos);
    }
}
