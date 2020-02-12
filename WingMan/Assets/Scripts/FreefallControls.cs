using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FreefallControls : MonoBehaviour
{
    #region Private Fields
    [SerializeField]
    private Camera      _mainCamera;
    [SerializeField]
    private Rigidbody   _rigidbody;
    [SerializeField]
    private float       _speed;
    [SerializeField]
    private float       _maxSpeed;
    [SerializeField]
    private float       _downSpeed;
    [SerializeField]
    private float       _decellerationRate;
    [SerializeField]
    private Animator    _animator;

    private Vector3     _forcesToAdd     = Vector3.zero;
    private ClipPoints  _clipPoints;
    private float       _clampedX, _clampedZ;
    #endregion

    #region MonoBehavior Callbacks
    private void OnEnable()
    {
        if (_rigidbody == null) _rigidbody.GetComponent<Rigidbody>();

        float playerHeight = Mathf.Abs(transform.position.y - _mainCamera.transform.position.y);

        _clipPoints = GameManager.CalculatePlayGround(playerHeight, _mainCamera);

        _rigidbody.velocity = Vector3.down * _downSpeed;

    }
    private void FixedUpdate()
    {

        if (InputHandler.GetTouch().phase == TouchPhase.Ended || InputHandler.GetTouch().phase == TouchPhase.Stationary)
        {
            _rigidbody.velocity = Vector3.MoveTowards(_rigidbody.velocity, new Vector3(0, _rigidbody.velocity.y, 0), _decellerationRate);
        }
        else
        {
            _clampedX = Mathf.Clamp(InputHandler.GetTouchDelta().x * _speed, _maxSpeed * -1, _maxSpeed);
            _clampedZ = Mathf.Clamp(InputHandler.GetTouchDelta().y * _speed, _maxSpeed * -1, _maxSpeed);

            _forcesToAdd = new Vector3(_clampedX, 0, _clampedZ);
         
            _rigidbody.velocity += _forcesToAdd;
        }

        _animator.SetFloat("VelocityX", _rigidbody.velocity.x / _maxSpeed);
        _animator.SetFloat("VelocityZ", _rigidbody.velocity.z / _maxSpeed);

        _clampedX = Mathf.Clamp(transform.position.x, _clipPoints.upperLeft.x, _clipPoints.upperRight.x);
        _clampedZ = Mathf.Clamp(transform.position.z, _clipPoints.downLeft.z, _clipPoints.upperRight.z);

        Vector3 clampedPos = new Vector3(_clampedX, transform.position.y, _clampedZ);

        transform.position = clampedPos;
    }
    #endregion

    #region Public Methods
    public void ChangeState(PlayerStates _state)
    {
        this.enabled = (_state != PlayerStates.FreeFalling);
    }
    #endregion

}
