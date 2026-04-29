using UnityEngine;
using System;

public class StreetOfficer : AbstractEntity
{
    public Pedestrian DetainedPedestrian { get; set; }

    [SerializeField] private ScannerController _scannerController;
    [SerializeField] private Transform _model;
    [SerializeField] private PedestrianDescriptionPanel _pedestrianDescriptionPanel;
    [SerializeField] private AbstractInteractionPanel _interactionPanel;
    [SerializeField] private StreetPatrolOrderController _orderController;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _detainCamera;
    [SerializeField] private Transform _followTarget;
    [SerializeField] private Animator _animator;
    [SerializeField] private Swat _swat;
    [SerializeField] private StreetDutyData _streetDutyData;
    [SerializeField] private ParticleSystem _electricParticles;

    private StreetOfficerAnimatorController _animatorController;
    private IState _transitionState;   
    private IMovementController _movementController;

    private void Awake()
    {
        float speed = 150 * 1.3f;
        _movementController = new StreetOfficerRBMovementController(speed, speed, GetComponent<Rigidbody>(), _model, _followTarget);
        _scannerController.Enabled = true;
        InitStreetOfficer();
    }

    private void FixedUpdate()
    {
        _stateMachine?.FixedTick();        
    }

    private void Update()
    {
        _stateMachine?.Tick();        
    }

    public void InitStreetOfficer()
    {
        _stateMachine = new JasonWeimannStateMachine();
        _animatorController = new StreetOfficerAnimatorController(_animator);
        _scannerController.Init(_animatorController);

        var moveAction = new MoveByRBControllerAction(_movementController);
        var actions = new Action[] { moveAction.Do, _scannerController.Scan, _animatorController.SetAnimatorSpeed };
        var moveAndUpdateAnimSpeedAction = new MultipleGenericActionsAction(actions);
        var moveStateEndAction = new GenericAction(() => _animator.SetFloat("speed", 0));

        var moveState = new RBMoveState(moveAndUpdateAnimSpeedAction, null, moveStateEndAction);

        var detainState = new DetainPedestrianState(_model, _scannerController, _pedestrianDescriptionPanel, _interactionPanel, _orderController, _detainCamera, this);

        var letGoState = new LetGoPedestrianState(this, _orderController, _scannerController);
        var arrestState = new ArrestPedestrianState(_scannerController, _animatorController, _orderController, transform, _model, _followTarget, this, _swat);
        var shockState = new ShockPedestrianState(_scannerController, _animatorController, _orderController, transform, _model, _followTarget, this, _electricParticles);

        var transitionState = new TransitionToActionState(this, _detainCamera, _streetDutyData, _model.gameObject);

        var fsm = _stateMachine as JasonWeimannStateMachine;

        fsm.AddTransition(moveState, detainState, () => _scannerController.IsScanCompleted() && _scannerController.CurrentPedestrian != null);

        fsm.AddTransition(letGoState, moveState, () => letGoState.CanTransition);
        fsm.AddTransition(arrestState, moveState, () => arrestState.CanTransition);
        fsm.AddTransition(shockState, moveState, () => shockState.CanTransition);

        fsm.AddTransition(transitionState, letGoState, () => transitionState.CanTransition && transitionState.CrimeType == CrimeType.NotGuilty);
        fsm.AddTransition(transitionState, arrestState, () => transitionState.CanTransition && transitionState.CrimeType == CrimeType.ArrestWorthy);
        fsm.AddTransition(transitionState, shockState, () => transitionState.CanTransition && transitionState.CrimeType == CrimeType.ShockWorthy);

        _transitionState = transitionState;

        fsm.SetState(moveState);
    }

    public void TransitionFromDetainState(CrimeType crimeType)
    {
        var transState = _transitionState as TransitionToActionState;
        transState.CrimeType = crimeType;
        _stateMachine.SetState(_transitionState);
    }
}