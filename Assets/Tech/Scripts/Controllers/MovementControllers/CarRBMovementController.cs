using UnityEngine;

public class CarRBMovementController : IMovementController, ISetMovementSpeed, ITurnController
{
    public bool PoliceCarIsInfront { get; private set; }
    private float _currentSpeed;
    private Rigidbody _rb;
    private Transform _transformToMove;
    private Transform[] _rayOrigins;
    private float _rayDist;
    private LayerMask _policeCarLayer;
    private Quaternion _initRot, _defaultTurnRot, _altTurnRot, _turnRot;
    private int _lane;

    private Transform _self;
    private float _maxX;

    private bool _turnChosen;

    public CarRBMovementController(float currentSpeed, Rigidbody rb, Transform transformToMove, Transform[] rayOrigins, float rayDist, LayerMask policeCarLayer, int lane)
    {
        _currentSpeed = currentSpeed;
        _rb = rb;
        _transformToMove = transformToMove;
        _rayOrigins = rayOrigins;
        _rayDist = rayDist;
        _policeCarLayer = policeCarLayer;
        _lane = lane;
        _defaultTurnRot = Quaternion.Euler(0, 45, 0);
        _altTurnRot = Quaternion.Euler(0, -45, 0);
        _initRot = _transformToMove.rotation;
    }

    public void DropSpeedTo(float speed)
    {
        if (_currentSpeed > speed)
        {
            _currentSpeed -= (Time.deltaTime * 100);
        }
        else
        {
            _currentSpeed = speed;
        }
    }

    public void GainSpeedTo(float speed)
    {
        if (_currentSpeed < speed)
        {
            _currentSpeed += (Time.deltaTime * 100);
        }
        else
        {
            _currentSpeed = speed;
        }
    }

    public void Move()
    {        
        _rb.velocity = _transformToMove.forward * _currentSpeed * Time.fixedDeltaTime;        
    }

    public void SetSpeed(float speed)
    {
        _currentSpeed = speed;
    }

    public void Turn()
    {
        PoliceCarIsInfront = false;
        foreach (var origin in _rayOrigins)
        {
            if (Physics.Raycast(origin.position, Vector3.forward, _rayDist, _policeCarLayer))
            {
                PoliceCarIsInfront = true;
                if (!_turnChosen)
                {

                    if (Physics.Raycast(_rayOrigins[0].position, Vector3.left, _rayDist, _policeCarLayer))
                    {
                        _turnRot = _defaultTurnRot;
                    }
                    else
                    {
                        _turnRot = _altTurnRot;
                    }
     
                    _turnChosen = true;
                }
            }
        }
        if (PoliceCarIsInfront)
        {
            _transformToMove.rotation = Quaternion.Lerp(_transformToMove.rotation, _turnRot, 2 * Time.fixedDeltaTime);
        }
        else
        {
            _transformToMove.rotation = Quaternion.Lerp(_transformToMove.rotation, _initRot, 2 * Time.fixedDeltaTime);
            _turnChosen = false;
        }
    }

    public void Center()
    {
        _transformToMove.rotation = Quaternion.Lerp(_transformToMove.rotation, _initRot, 2 * Time.fixedDeltaTime);
    }
}