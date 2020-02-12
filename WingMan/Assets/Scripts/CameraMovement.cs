using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Private Fields
    [SerializeField]
    private GameObject  _target;
    [SerializeField]
    private Vector3     _offset;
    [SerializeField]
    private float       _smoothSpeed;
    #endregion

    #region MonoBehavior Callbacks
    
    void FixedUpdate()
    {
        Vector3 desiredPosition = _target.transform.position + _offset;
        Vector3 destination = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.fixedDeltaTime);
        destination.x = 0;
        destination.z = 0;
        transform.position = destination;
        //transform.LookAt(_target.transform);
    }
    #endregion
}
