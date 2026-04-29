using UnityEngine;
using Cinemachine;

public class TransitionToActionState : IState
{   
    public CrimeType CrimeType { get; set; }
    public bool CanTransition
    {
        get => _startTime + _duration < Time.time;
    }

    private Pedestrian _pedestrian;
    private StreetOfficer _officer;
    private CinemachineVirtualCamera _detainCamera;
    private StreetDutyData _streetDutyData;
    private GameObject _officerModel;

    private float _startTime;
    private float _duration = 1.8f;

    public TransitionToActionState(StreetOfficer officer, CinemachineVirtualCamera detainCamera, StreetDutyData streetDutyData, GameObject officerModel)
    {
        _officer = officer;
        _detainCamera = detainCamera;
        _streetDutyData = streetDutyData;
        _officerModel = officerModel;
    }

    public void Enter()
    {
        _startTime = Time.time;
        _pedestrian = _officer.DetainedPedestrian;        
        
        _pedestrian.ReactToAction(CrimeType);
        _streetDutyData.AddScannedPedestrian(_pedestrian.name, CrimeType == CrimeType.ArrestWorthy || CrimeType == CrimeType.ShockWorthy);
    }

    public void Exit()
    {
        _detainCamera.gameObject.SetActive(false);
        _officerModel.SetActive(true);
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        
    }
}