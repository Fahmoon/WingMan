using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FreefallControls : MonoBehaviour
{
    #region Private Fields

    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _downSpeed;
    [SerializeField]
    private float _decellerationRate;
    [SerializeField]
    private Animator _animator;

    private Vector3 _forcesToAdd = Vector3.zero;
    private float _clampedX, _clampedZ;
    #endregion

    #region MonoBehavior Callbacks
    private void OnEnable()
    {
        if (_rigidbody == null) _rigidbody.GetComponent<Rigidbody>();


        _rigidbody.velocity = Vector3.down * _downSpeed;
    }
    private void FixedUpdate()
    {
        if (InputHandler.GetTouchDelta().sqrMagnitude < 0.0001f)
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
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, -_downSpeed, _rigidbody.velocity.z);

        _animator.SetFloat("VelocityX", _rigidbody.velocity.x / _maxSpeed);
        _animator.SetFloat("VelocityZ", _rigidbody.velocity.z / _maxSpeed);


    }
    #endregion

    #region Public Methods
    public void ChangeState(PlayerStates _state)
    {
        this.enabled = (_state == PlayerStates.FreeFalling);
    }
    #endregion

}
