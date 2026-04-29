using UnityEngine;

public class PoliceCar : AbstractEntity
{
    public SwapCamerasState TransitionToDetainCameraState { get; private set; }
    public IState ScanState { get; private set; }
    public SwapCamerasState TransitionToInspectCameraState { get; private set; }
    public IState FailedState { get; private set; }

    public IState CurrentState
    {
        get => _stateMachine.GetCurrentState();
    }

    [SerializeField] private RoadDutyOrderController _orderController;
    [SerializeField] private PoliceCarSkannerController _scanner;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _defaultCamera, _detainCamera, _inspectCamera;
    private Cinemachine.CinemachineTrackedDolly _dollyCart;
    [SerializeField] private GameObject _goodCallCanvas, _sliderCanvas, _failedCanvas, _joystickCanvas;
    [SerializeField] private GoodCallCanvasController _goodCallController;
    [SerializeField] private DollyTrackCameraController _dollyTrackCameraController;
    [SerializeField] private DriversLicensePanelController _driversLicensePanelController;
    [SerializeField] private ButtonPanelController _buttonPanelController;
    [SerializeField] private DayCounter _dayCounter;

    private IState _inspectState, _letGoState, _issueFineState, _arrestState;
    private Rigidbody _rb;
    private bool _bumpedIntoCar;

    private void Awake()
    {
        InitCar();
    }

    private void Update()
    {
        _stateMachine?.Tick();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Car car = collision.transform.GetComponent<Car>();
        if (car != null)
        {
            _bumpedIntoCar = true;
        }
    }

    public void InitCar()
    {
        _stateMachine = new JasonWeimannStateMachine();
        _rb = GetComponent<Rigidbody>();

        ScanState = new GenericState(null, null, null, new GenericAction(() => _scanner.gameObject.SetActive(false)));
        TransitionToDetainCameraState = new SwapCamerasState(_defaultCamera, _detainCamera, 0.5f);
        TransitionToInspectCameraState = new SwapCamerasState(_detainCamera, _inspectCamera);

        var carAnimController = GetComponent<CarAnimationController>();
        var detainController = GetComponent<PoliceCarDetainController>();
        var detainState = new PoliceCarDetainState(detainController, _scanner, carAnimController);
        var goodCallCanvasState = new PoliceCarGoodCallState(_goodCallCanvas);
        var failedCanvasState = new PoliceCarFailedState(_failedCanvas);
        
        var bumpState = new PoliceCarBumpState(_rb);

        _dollyCart = _inspectCamera.GetCinemachineComponent<Cinemachine.CinemachineTrackedDolly>();        

        _inspectState = new PoliceCarInspectState(_sliderCanvas, _dollyTrackCameraController, detainController, _goodCallController, _joystickCanvas, _driversLicensePanelController, _buttonPanelController, _dayCounter);

        _letGoState = new PoliceCarLetGoState(detainController, _orderController, _sliderCanvas);
        _issueFineState = new PoliceCarIssueFineState(detainController, _orderController, _sliderCanvas);
        _arrestState = new PoliceCarArrestState(detainController, _orderController, _sliderCanvas);

        var letGoState = _letGoState as PoliceCarInspectActionState;
        var issueFine = _issueFineState as PoliceCarInspectActionState;
        var arrestState = _arrestState as PoliceCarInspectActionState;

        var finishInspectState = new PoliceCarFinishInspectState();

        var fsm = _stateMachine as JasonWeimannStateMachine;

        fsm.AddTransition(TransitionToDetainCameraState, detainState, () => TransitionToDetainCameraState.TransitionIsOver && CurrentState == TransitionToDetainCameraState);
        fsm.AddTransition(detainState, goodCallCanvasState, () => detainController.IsFinishedMoving && detainController.SuccessfullyDetained);
        fsm.AddTransition(detainState, failedCanvasState, () => detainController.IsFinishedMoving && !detainController.SuccessfullyDetained);

        fsm.AddTransition(TransitionToInspectCameraState, _inspectState, () => TransitionToInspectCameraState.TransitionIsOver && CurrentState == TransitionToInspectCameraState);
        fsm.AddTransition(detainState, bumpState, () => _bumpedIntoCar);
        fsm.AddTransition(bumpState, failedCanvasState, () => bumpState.IsTimeOver);

        fsm.AddTransition(_letGoState, finishInspectState, () => letGoState.CanTransition);
        fsm.AddTransition(_issueFineState, finishInspectState, () => issueFine.CanTransition);
        fsm.AddTransition(_arrestState, finishInspectState, () => arrestState.CanTransition);

        _stateMachine.SetState(ScanState);
    }

    public void TrySetState(IState stateFrom, IState stateTo)
    {
        var checkStateCommand = new CheckStateCommand(stateFrom, _stateMachine.GetCurrentState());
        var setStateCommand = new SetStateCommand(stateTo, _stateMachine);

        checkStateCommand.SetNext(setStateCommand);
        checkStateCommand.HandleCommand();
    }

    public void LetGo()
    {
        TrySetState(_inspectState, _letGoState);
    }

    public void IssueFine()
    {
        TrySetState(_inspectState, _issueFineState);
    }

    public void Arrest()
    {
        TrySetState(_inspectState, _arrestState);
    }
}