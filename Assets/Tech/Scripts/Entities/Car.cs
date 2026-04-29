using UnityEngine;
using System;

public class Car : AbstractEntity
{
    public static event Action<Car> CarScanned;

    public bool IsScanned { get => _isMaxScanProgressReached; }
    public Driver Driver { get; private set; }
    public CarData CarData { get; set; }
    public int Speed { get; private set; }
    public string LicensePlateNumber { get; private set; }
    public int Lane { get; private set; }    
    public Transform TargetLeft { get => _targetLeft;  }
    public Transform MaxZPos { get => _maxZPos; set => _maxZPos = value; }
    public Transform MinZPos { get => _minZPos; set => _minZPos = value; }

    public IState CurrentState
    {
        get => _stateMachine.GetCurrentState();
    }

    public IState StopState
    {
        get => _stopState;
    }

    public IState MoveState
    {
        get => _moveState;
    }

    public IState ArrestState
    { 
        get => _arrestState;         
    }

    public Transform DriverPlace 
    { 
        get => _driverPlace;  
    }

    public DollyTrackCameraController DollyTrackCameraController
    { 
        get => _dollyTrackCameraController; 
    }

    public Transform TrunkStuffPlace 
    { 
        get => _trunkStuffPlace; 
    }
    public TrunkController TrunkController 
    { 
        get => _trunkController;  
    }

    [SerializeField] private Transform _driverPlace;
    [SerializeField] private CarSpeedDisplay _speedDisplay;    
    [SerializeField] private LayerMask _policeCarLayer;
    [SerializeField] private Transform[] _rayOrigins;
    [SerializeField] private Transform _targetLeft;
    [SerializeField] private CarLicenseNumberPlate[] _licensePlateNumbers;
    [SerializeField] private DollyTrackCameraController _dollyTrackCameraController;
    [SerializeField] private Transform _trunkStuffPlace;
    [SerializeField] private TrunkController _trunkController;
    [SerializeField] private ParticleSystem _moneyParticles;
    private Transform _maxZPos;
    private Transform _minZPos;
    private IMovementController _forwardMovementController;
    private Rigidbody _rb;

    private float _scanSpeed = 50;
    private float _scanProgress;
    private float _scanMaxProgress = 40;

    private bool _isMaxScanProgressReached;
    private bool _isDetained;

    private IState _moveFastState, _stopState, _moveState, _arrestState;

    private float _initZ, _zToReturn;

    private GameObject _carMesh;
    private float _rayDist = 5;

    private void Awake()
    {        
              
    }
    private void Update()
    {
        _stateMachine?.Tick();
        if (transform.position.z >= _zToReturn && gameObject.activeInHierarchy && !_isDetained)
        {
            ReturnToPool();
        }
    }

    private void FixedUpdate()
    {
        _stateMachine?.FixedTick();
    }

    public void Scan()
    {
        if (!_isMaxScanProgressReached)
        {
            _scanProgress += (_scanSpeed * Time.fixedDeltaTime);
            float amount = _scanProgress / _scanMaxProgress;
            _speedDisplay.UpdateFill(amount);
            if (amount >= 1)
            {
                _speedDisplay.ShowText(Speed, Speed > 30);
                _isMaxScanProgressReached = true;
                CarScanned?.Invoke(this);
            }
        }
        else
        {
            _speedDisplay.UpdateFill(1);
        }        
    }

    public void Init(CarData carData, Driver driver)
    {
        _speedDisplay.ResetDisplay();

        _scanProgress = 0;
        _isMaxScanProgressReached = false;
        _initZ = transform.position.z;

        CarData = carData;

        Speed = carData.Speed;
        Lane = carData.Lane;

        if (_licensePlateNumbers != null)
        {
            foreach (var plate in _licensePlateNumbers)
            {
                plate.SetLicensePlateNumber(CarData.LicensePlateNumber);
            }
        }
        
        _zToReturn = Lane == 0 ? 3.8f : 4;

        _isDetained = false;
        _rb = GetComponent<Rigidbody>();

        Driver = driver;
        this.Driver.transform.position = _driverPlace.position;
        Driver.transform.SetParent(_driverPlace);
        Driver.transform.localScale = Vector3.one;
        Driver.transform.localPosition = Vector3.zero;

        _forwardMovementController = new CarRBMovementController(100, _rb, transform, _rayOrigins, _rayDist, _policeCarLayer, Lane);
        _stateMachine = new JasonWeimannStateMachine();
        var fsm = _stateMachine as JasonWeimannStateMachine;

        ISetMovementSpeed setSpeed = _forwardMovementController as ISetMovementSpeed;
        ITurnController turnController = _forwardMovementController as ITurnController;
        CarAnimationController carAnimController = GetComponent<CarAnimationController>();
        _moveState = new CarMoveState(_forwardMovementController, setSpeed, 100, turnController, carAnimController);
        _moveFastState = new CarFastMoveState(_forwardMovementController, setSpeed, 200, turnController);
        _stopState = new CarStopState(_forwardMovementController, setSpeed, turnController, carAnimController);
        _arrestState = new CarArrestState(this);

        CarRBMovementController carRBMovementController = _forwardMovementController as CarRBMovementController;

        fsm.AddTransition(_moveFastState, _stopState, () => _isDetained && carRBMovementController.PoliceCarIsInfront);
        fsm.SetState(_moveState);
    }

    public bool TryDetain()
    {
        _stateMachine.SetState(_moveFastState);
        if (transform.position.z < MinZPos.position.z || transform.position.z >= MaxZPos.position.z)
        {
            return false;
        }
        else
        {            
            _isDetained = true;
            return true;
        }
    }

    public void ReturnToPool()
    {
        _speedDisplay.ResetDisplay();
        this.Driver.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void TrySetState(IState stateFrom, IState stateTo)
    {
        var checkStateCommand = new CheckStateCommand(stateFrom, _stateMachine.GetCurrentState());
        var setStateCommand = new SetStateCommand(stateTo, _stateMachine);

        checkStateCommand.SetNext(setStateCommand);
        checkStateCommand.HandleCommand();
    }

    public void CloseTrunk()
    {
        _trunkController?.Close();
    }

    public void Fine()
    {
        _moneyParticles?.Play();
    }
}