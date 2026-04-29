using UnityEngine;
using CnControls;

public class StreetOfficerRBMovementController : IMovementController
{
    public float CurrentSpeed => _currentSpeed;
    private float _playerSpeed;
    private float _defaultSpeed;
    private float _currentSpeed;
    private float _rotSpeed = 3f;
    private Rigidbody _rb;
    private Transform _modelTransform;
    private SimpleJoystick _joy;
    private Vector3 _lastForward, _lastRight;
    private Transform _followTarget;    

    public StreetOfficerRBMovementController(float playerSpeed, float defaultSpeed, Rigidbody rb, Transform modelTransform, Transform followTarget)
    {
        _playerSpeed = playerSpeed;
        _defaultSpeed = defaultSpeed;
        _rb = rb;
        _modelTransform = modelTransform;
        _lastForward = _modelTransform.forward;
        _lastRight = _modelTransform.right;

        _followTarget = followTarget;
    }

    public void Move()
    {
        //if (_joy == null) return;

        Vector3 moveDirection = new Vector3(CnInputManager.GetAxis("Horizontal"), 0, CnInputManager.GetAxis("Vertical"));

        //moveDirection = _lastForward * moveDirection.z + _lastRight * moveDirection.x;
        moveDirection = Camera.main.transform.forward * moveDirection.z + Camera.main.transform.right * moveDirection.x;
        moveDirection.y = 0f;        

        moveDirection.Normalize();

        _currentSpeed = moveDirection.sqrMagnitude;
        
        if (_currentSpeed > 0)
        {
            Quaternion r = Quaternion.LookRotation(moveDirection, Vector3.up);

            _modelTransform.rotation = Quaternion.Slerp(_modelTransform.rotation, r, _rotSpeed * Time.deltaTime);
            float t = Mathf.InverseLerp(-1, 0, CnInputManager.GetAxis("Vertical"));
            float speed = Mathf.Lerp(0, _rotSpeed, t);
            _followTarget.rotation = Quaternion.Lerp(_followTarget.rotation, _modelTransform.rotation,  _rotSpeed * 0.3f * Time.deltaTime);
        }
        else
        {
            _lastForward = _modelTransform.forward;
            _lastRight = _modelTransform.right;
        }        

        _rb.velocity = moveDirection * _playerSpeed * Time.deltaTime;
        
        //_playerAnimatorController.SetSpeed(speed);
    }
}