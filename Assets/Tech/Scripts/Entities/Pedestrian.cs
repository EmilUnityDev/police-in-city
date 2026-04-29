using System.Collections.Generic;
using UnityEngine;
using System;

public class Pedestrian : AbstractEntity
{
    public bool HasBeenArrestedOrShocked { get; set; }

    public bool HasBeenDetained
    { 
        get => _hasBeenDetained; 
        set => _hasBeenDetained = value; 
    }

    public Transform OfficerTransform => _officerTransform;
    public Gender Gender => _gender;
    public PedestrianData PedestrianData => _pedestrianData;
    public Transform DetainCamTransform => _detainCamTransform;
    public Transform DetainCamLookAt => _detainCamLookAt;

    private Transform _officerTransform;
    [SerializeField] private Gender _gender;
    [SerializeField] private Transform _detainCamTransform;
    [SerializeField] private Transform _detainCamLookAt;
    [SerializeField] private SkinnedMeshRendererController _skinnedMeshRendererController;
    [SerializeField] private PedestrianAnimatorController _pedestrianAnimatorController;
    [SerializeField] private PedestrianScanProgressBar _scanProgressBar;
    [SerializeField] private PedestrianBehaviour _pedestrianBehaviour;
    private string _name;
    private string _description;
    private CrimeType _crimeType;
    private PedestrianData _pedestrianData;
    private bool _hasBeenDetained;
    private IState _detainState, _moveState, _scanState;
    private Dictionary<CrimeType, Action> _emotionsByCrimeType;

    public void InitPedestrian(string name, string description, CrimeType crimeType, Gender gender = Gender.None)
    {
        _name = name;
        _description = description;
        _crimeType = crimeType;
        _gender = gender;      

        _pedestrianData = new PedestrianData(_name, _description, _crimeType);

        _pedestrianAnimatorController.SetIdle();

        _stateMachine = new JasonWeimannStateMachine();
        
        _detainState = new PedestrianDetainState(this);

        switch (_pedestrianBehaviour)
        {
            case PedestrianBehaviour.MovingByWayPoints:
                IMovementController movementController = GetComponent<INPCMoveControllerGenerator>().GetMoveController();
                if (movementController == null)
                {
                    _moveState = new RBMoveState(null, new GenericAction(_pedestrianAnimatorController.SetIdle));
                    _scanState = new RBMoveState(null, new GenericAction(_pedestrianAnimatorController.SetIdle));
                    break;
                }
                var moveAction = new MoveByRBControllerAction(movementController);
                _moveState = new RBMoveState(moveAction, new GenericAction(_pedestrianAnimatorController.SetWalk));
                _scanState = new RBMoveState(moveAction);
                break;
            case PedestrianBehaviour.Sitting:
                _moveState = new RBMoveState(null, new GenericAction(_pedestrianAnimatorController.SetSitting));
                _scanState = new RBMoveState(null, new GenericAction(_pedestrianAnimatorController.SetIdle));                
                break;
            default:
                _moveState = new RBMoveState(null, new GenericAction(_pedestrianAnimatorController.SetIdle));
                _scanState = new RBMoveState(null, new GenericAction(_pedestrianAnimatorController.SetIdle));
                break;
        }  
        
        _stateMachine.SetState(_moveState);
    }

    private void FixedUpdate()
    {
        _stateMachine?.FixedTick();
    }

    private void Update()
    {
        _stateMachine?.Tick();
    }

    public void Detain(Transform policeOfficer)
    {
        if (_detainState == null) return;
        _officerTransform = policeOfficer;
        _stateMachine.SetState(_detainState);
        _pedestrianAnimatorController.SetIdle();
        _scanProgressBar.DisableProgressBar();
    }

    public void LetGo()
    {
        _stateMachine.SetState(_moveState);
        _skinnedMeshRendererController.SetRegular();   
        _scanProgressBar.DisableProgressBar();
    }

    public void Scan(float progress)
    {
        _stateMachine.SetState(_scanState);
        _scanProgressBar.DisplayProgress(progress);
    }

    public void StopScan()
    {
        if (_stateMachine.GetCurrentState() != _detainState)
            _stateMachine.SetState(_moveState);

        _scanProgressBar.DisableProgressBar();
    }

    public void Shock()
    {
        _scanProgressBar.DisableProgressBar();
        _pedestrianAnimatorController.SetElectrocuted();
    }

    public void Arrest()
    {
        _scanProgressBar.DisableProgressBar();        
    }

    public void TakeOut()
    {
        _pedestrianAnimatorController.SetArrest();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void ReactToAction(CrimeType crimeType)
    {
        _scanProgressBar.DisableProgressBar();

        switch (crimeType)
        {
            case CrimeType.ArrestWorthy:
                _skinnedMeshRendererController.SetAngry();
                _pedestrianAnimatorController.SetAngry();
                break;
            case CrimeType.ShockWorthy:
                _skinnedMeshRendererController.SetScared();
                _pedestrianAnimatorController.SetFear();
                break;
            case CrimeType.NotGuilty:
                _skinnedMeshRendererController.SetSmile();
                _pedestrianAnimatorController.SetFunny();
                break;
            default:
                _skinnedMeshRendererController.SetRegular();
                break;
        }
    }
}